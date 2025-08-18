// pipeline {
//     agent any

//         triggers {
//         githubPush()
//     }

//     environment {
//         APP_NAME = 'emailserviceapi'
//         BUILD_NUMBER = "${env.BUILD_NUMBER}"
//     }
    
//     stages {
//         stage('Checkout') {
//             steps {
//                 checkout scm
//             }
//         }

//         stage('Unit Tests') {
//             parallel {
//                 stage('Backend Tests') {
//                     steps {
//                         dir('backend') {
//                             sh '''
//                                 docker build -f Dockerfile.test -t ${APP_NAME}:test-${BUILD_NUMBER} .
//                                 mkdir -p test-results
//                                 docker run --rm \
//                                     -v ${PWD}/test-results:/app/test-results \
//                                     ${APP_NAME}:test-${BUILD_NUMBER}
//                             '''
//                         }
//                     }
//                     post {
//                         always {
//                             publishTestResults testResultsPattern: 'backend/test-results/*.trx'
//                             archiveArtifacts artifacts: 'backend/test-results/**/*', allowEmptyArchive: true
//                         }
//                     }
//                 }
                
//                 stage('Frontend Tests') {
//                     steps {
//                         dir('frontend') {
//                             sh '''
//                                 npm ci
//                                 npm test -- --coverage --watchAll=false || true
//                             '''
//                         }
//                     }
//                     post {
//                         always {
//                             archiveArtifacts artifacts: 'frontend/coverage/**/*', allowEmptyArchive: true
//                         }
//                     }
//                 }
//             }
//         }
        
//         stage('Build Images') {
//             when {
//                 expression { currentBuild.currentResult == 'SUCCESS' }
//             }
//             parallel {
//                 stage('Backend') {
//                     steps {
//                         dir('backend') {
//                             sh '''
//                                 docker build -t ${APP_NAME}:backend-${BUILD_NUMBER} .
//                                 docker tag ${APP_NAME}:backend-${BUILD_NUMBER} ${APP_NAME}:backend-latest
//                             '''
//                         }
//                     }
//                 }
                
//                 stage('Frontend') {
//                     steps {
//                         dir('frontend') {
//                             sh '''
//                                 docker build -t ${APP_NAME}:frontend-${BUILD_NUMBER} .
//                                 docker tag ${APP_NAME}:frontend-${BUILD_NUMBER} ${APP_NAME}:frontend-latest
//                             '''
//                         }
//                     }
//                 }
//             }
//         }
        
//         stage('Integration Tests') {
//             when {
//                 expression { currentBuild.currentResult == 'SUCCESS' }
//             }
//             steps {
//                 sh '''
//                     docker-compose up -d
//                     timeout 120 bash -c 'until curl -f http://localhost/api/health; do sleep 5; done'
//                     curl -X POST http://localhost/api/auth/login \
//                          -H "Content-Type: application/json" \
//                          -d '{"email":"test@example.com"}' \
//                          --fail
//                 '''
//             }
//             post {
//                 always {
//                     sh 'docker-compose down -v || true'
//                 }
//             }
//         }
//     }
    
//     post {
//         always {
//             sh '''
//                 docker rmi ${APP_NAME}:test-${BUILD_NUMBER} || true
//                 docker image prune -f || true
//             '''
//             cleanWs()
//         }
//     }
// }


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
                                docker build -f Dockerfile.test -t ${APP_NAME}:backend-test-${BUILD_NUMBER} .
                                mkdir -p test-results
                                docker run --rm \
                                    -v ${PWD}/test-results:/app/test-results \
                                    ${APP_NAME}:backend-test-${BUILD_NUMBER}
                            '''
                        }
                    }
                    post {
                        always {
                            publishTestResults testResultsPattern: 'backend/test-results/*.trx'
                            archiveArtifacts artifacts: 'backend/test-results/**/*', allowEmptyArchive: true
                        }
                    }
                }
                
                stage('Frontend Tests') {
                    steps {
                        dir('frontend') {
                            sh '''
                                docker build -f Dockerfile.test -t ${APP_NAME}:frontend-test-${BUILD_NUMBER} .
                                mkdir -p test-results coverage
                                docker run --rm \
                                    -v ${PWD}/test-results:/app/test-results \
                                    -v ${PWD}/coverage:/app/coverage \
                                    ${APP_NAME}:frontend-test-${BUILD_NUMBER}
                            '''
                        }
                    }
                    post {
                        always {
                            archiveArtifacts artifacts: 'frontend/coverage/**/*', allowEmptyArchive: true
                            // Uncomment if you have test results XML from frontend:
                            // publishTestResults testResultsPattern: 'frontend/test-results/*.xml'
                        }
                    }
                }
            }
        }
        
        stage('Build Images') {
            when {
                expression { currentBuild.currentResult == 'SUCCESS' }
            }
            parallel {
                stage('Backend') {
                    steps {
                        dir('backend') {
                            sh '''
                                docker build -t ${APP_NAME}:backend-${BUILD_NUMBER} .
                                docker tag ${APP_NAME}:backend-${BUILD_NUMBER} ${APP_NAME}:backend-latest
                            '''
                        }
                    }
                }
                
                stage('Frontend') {
                    steps {
                        dir('frontend') {
                            sh '''
                                docker build -t ${APP_NAME}:frontend-${BUILD_NUMBER} .
                                docker tag ${APP_NAME}:frontend-${BUILD_NUMBER} ${APP_NAME}:frontend-latest
                            '''
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
                    docker-compose up -d
                    timeout 120 bash -c 'until curl -f http://localhost/api/health; do sleep 5; done'
                    curl -X POST http://localhost/api/auth/login \
                         -H "Content-Type: application/json" \
                         -d '{"email":"test@example.com"}' \
                         --fail
                '''
            }
            post {
                always {
                    sh 'docker-compose down -v || true'
                }
            }
        }
    }
    
    post {
        always {
            sh '''
                docker rmi ${APP_NAME}:backend-test-${BUILD_NUMBER} || true
                docker rmi ${APP_NAME}:frontend-test-${BUILD_NUMBER} || true
                docker image prune -f || true
            '''
            cleanWs()
        }
    }
}