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
                    # Start the application
                    docker compose up -d --build                    
                    # Run integration tests
                    cd tests/integration
                    docker run --rm -v $(pwd)/tests/integration:/app python:3.9-slim bash -c "cd /app && pip install -r requirements.txt && python -m pytest"
                    # pip3 install -r requirements.txt
                    # python3 integration_test.py
                '''
            }
            post {
                always {
                    sh '''
                        docker-compose logs || true
                        docker-compose down -v || true
                    '''
                    archiveArtifacts artifacts: 'tests/integration/test-results/**/*', allowEmptyArchive: true
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