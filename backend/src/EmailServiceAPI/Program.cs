using EmailServiceAPI.Models;
using EmailServiceAPI.Services;
using EmailServiceAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000") // React default ports
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add validation
builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();

// Add custom services
builder.Services.AddScoped<IEmailService, EmailService>();

// Add logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

// Minimal API endpoints
app.MapPost("/api/auth/login", async (
    [FromBody] LoginRequest request,
    IValidator<LoginRequest> validator,
    IEmailService emailService,
    ILogger<Program> logger) =>
{
    try
    {
        // Validate request
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Login attempt with invalid email: {Email}", request.Email);
            return Results.BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "Invalid email format",
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            });
        }

        // Process login (currently just logging, will integrate with AWS SES later)
        var result = await emailService.SendLoginEmailAsync(request.Email);
        
        if (result.Success)
        {
            logger.LogInformation("Login email sent successfully to: {Email}", request.Email);
            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Login email sent successfully"
            });
        }
        else
        {
            logger.LogError("Failed to send login email to: {Email}. Error: {Error}", 
                request.Email, result.ErrorMessage);
            return Results.Problem(
                detail: result.ErrorMessage,
                statusCode: 500,
                title: "Email sending failed"
            );
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Unexpected error during login process for email: {Email}", request.Email);
        return Results.Problem(
            detail: "An unexpected error occurred",
            statusCode: 500,
            title: "Internal server error"
        );
    }
})
.WithName("Login")
.Produces<ApiResponse<object>>(200)
.Produces<ApiResponse<object>>(400)
.Produces(500);

// Health check endpoint
app.MapGet("/api/health", () =>
{
    return Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
})
.WithName("HealthCheck");
app.Run();