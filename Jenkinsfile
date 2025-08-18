pipeline {
    agent any
    
    triggers {
        githubPush()
    }
    
    environment {
        APP_NAME = 'automarkly'
        BUILD_NUMBER = "${env.BUILD_NUMBER}"
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Unit Tests') {
            parallel {
                stage('Backend Tests') {
                    steps {
                        dir('backend') {
                            sh '''
                                docker build -f Dockerfile.test -t ${APP_NAME}:test-${BUILD_NUMBER} .
                                mkdir -p test-results
                                docker run --rm \
                                    -v ${PWD}/test-results:/app/test-results \
                                    ${APP_NAME}:test-${BUILD_NUMBER}
                            '''
                        }
                    }
                    post {
                        always {
                            archiveArtifacts artifacts: 'backend/test-results/**/*', allowEmptyArchive: true
                        }
                    }
                }
                
                stage('Frontend Tests') {
                    steps {
                        dir('frontend') {
                            sh '''
                                docker build -f Dockerfile.test -t ${APP_NAME}:frontend-test-${BUILD_NUMBER} .
                                mkdir -p coverage
                                docker run --rm \
                                    -v ${PWD}/coverage:/app/coverage \
                                    ${APP_NAME}:frontend-test-${BUILD_NUMBER}
                            '''
                        }
                    }
                    post {
                        always {
                            archiveArtifacts artifacts: 'frontend/coverage/**/*', allowEmptyArchive: true
                        }
                    }
                }
            }
        }
        
        stage('Integration Tests') {
            when {
                expression { currentBuild.currentResult == 'SUCCESS' }
            }
            steps {
                sh '''
                    # Build integration test image
                    docker build -t ${APP_NAME}:integration-${BUILD_NUMBER} tests/integration/
                    
                    # Start the application
                    docker compose up -d --build
                    
                    # Wait for services to be ready
                    echo "Waiting for services to start..."
                    sleep 30
                    
                    # Run integration tests
                    echo "Running integration tests..."
                    docker run --rm \
                        --network automarkly_main_app-network \
                        -e BASE_URL=http://emailservice-frontend:80 \
                        ${APP_NAME}:integration-${BUILD_NUMBER}
                '''
            }
            post {
                always {
                    sh '''
                        echo "Collecting logs..."
                        docker compose logs > integration-logs.txt || true
                        # docker compose down -v || true
                    '''
                    archiveArtifacts artifacts: 'integration-logs.txt', allowEmptyArchive: true
                }
            }
        }
    }
    
    post {
        always {
            sh '''
                docker rmi ${APP_NAME}:test-${BUILD_NUMBER} || true
                docker rmi ${APP_NAME}:frontend-test-${BUILD_NUMBER} || true
                docker image prune -f || true
            '''
            cleanWs()
        }
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}