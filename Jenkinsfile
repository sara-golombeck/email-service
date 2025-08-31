// // // pipeline {
// // //     agent any
    
// // //     triggers {
// // //         githubPush()
// // //     }
    
// // //     environment {
// // //         APP_NAME = 'automarkly'
// // //         BUILD_NUMBER = "${env.BUILD_NUMBER}"
// // //     }
    
// // //     stages {
// // //         stage('Checkout') {
// // //             steps {
// // //                 checkout scm
// // //             }
// // //         }
        
// // //         // stage('Unit Tests') {
// // //         //     parallel {
// // //         //         stage('Backend Tests') {
// // //         //             steps {
// // //         //                 dir('backend') {
// // //         //                     sh '''
// // //         //                         docker build -f Dockerfile.test -t ${APP_NAME}:test-${BUILD_NUMBER} .
// // //         //                         mkdir -p test-results
// // //         //                         docker run --rm \
// // //         //                             -v ${PWD}/test-results:/app/test-results \
// // //         //                             ${APP_NAME}:test-${BUILD_NUMBER}
// // //         //                     '''
// // //         //                 }
// // //         //             }
// // //         //             post {
// // //         //                 always {
// // //         //                     archiveArtifacts artifacts: 'backend/test-results/**/*', allowEmptyArchive: true
// // //         //                 }
// // //         //             }
// // //         //         }
                
// // //         //         stage('Frontend Tests') {
// // //         //             steps {
// // //         //                 dir('frontend') {
// // //         //                     sh '''
// // //         //                         docker build -f Dockerfile.test -t ${APP_NAME}:frontend-test-${BUILD_NUMBER} .
// // //         //                         mkdir -p coverage
// // //         //                         docker run --rm \
// // //         //                             -v ${PWD}/coverage:/app/coverage \
// // //         //                             ${APP_NAME}:frontend-test-${BUILD_NUMBER}
// // //         //                     '''
// // //         //                 }
// // //         //             }
// // //         //             post {
// // //         //                 always {
// // //         //                     archiveArtifacts artifacts: 'frontend/coverage/**/*', allowEmptyArchive: true
// // //         //                 }
// // //         //             }
// // //         //         }
// // //         //     }
// // //         // }
        
// // //         stage('Integration Tests') {
// // //             when {
// // //                 expression { currentBuild.currentResult == 'SUCCESS' }
// // //             }
// // //             steps {
// // //                 sh '''
// // //                     # Build integration test image
// // //                     docker build -t ${APP_NAME}:integration-${BUILD_NUMBER} tests/integration/
                    
// // //                     # Start the application
// // //                     docker compose up -d --build                    
// // //                     # Run integration tests
// // //                     echo "Running integration tests..."
// // //                     docker run --rm \
// // //                         --network automarkly_main_app-network \
// // //                         -e BASE_URL=http://emailservice-frontend:80 \
// // //                         ${APP_NAME}:integration-${BUILD_NUMBER}
// // //                 '''
// // //             }
// // //             post {
// // //                 always {
// // //                     sh '''
// // //                         echo "Collecting logs..."
// // //                         docker compose logs > integration-logs.txt || true
// // //                         # docker compose down -v || true
// // //                     '''
// // //                     archiveArtifacts artifacts: 'integration-logs.txt', allowEmptyArchive: true
// // //                 }
// // //             }
// // //         }
// // //     }
    
// // //     post {
// // //         always {
// // //             sh '''
// // //                 docker rmi ${APP_NAME}:test-${BUILD_NUMBER} || true
// // //                 docker rmi ${APP_NAME}:frontend-test-${BUILD_NUMBER} || true
// // //                 docker image prune -f || true
// // //             '''
// // //             cleanWs()
// // //         }
// // //         success {
// // //             echo 'Pipeline completed successfully!'
// // //         }
// // //         failure {
// // //             echo 'Pipeline failed!'
// // //         }
// // //     }
// // // }



// // pipeline {
// //     agent any
    
// //     environment {
// //         APP_NAME = 'automarkly'
// //         BUILD_NUMBER = "${env.BUILD_NUMBER}"
// //         IMAGE_NAME = 'automarkly/emailservice'
// //         AWS_ACCOUNT_ID = credentials('aws-account-id')
// //         AWS_REGION = credentials('aws_region')
// //         ECR_URL = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com"
// //         ECR_REPO_BACKEND = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/automarkly/emailservice-backend"
// //         ECR_REPO_FRONTEND = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/automarkly/emailservice-frontend"
// //         GITOPS_REPO = 'git@github.com:sara-golombeck/gitops-email-service.git'
// //         GITOPS_BRANCH = 'main'
// //         HELM_VALUES_PATH = 'charts/automarkly/values.yaml'
// //         MAIN_TAG = ''
// //     }
    
// //     triggers {
// //         githubPush()
// //     }
    
// //     stages {
// //         stage('Checkout') {
// //             steps {
// //                 checkout scm
// //             }
// //         }
        
// //         // stage('Unit Tests') {
// //         //     parallel {
// //         //         stage('Backend Tests') {
// //         //             steps {
// //         //                 dir('backend') {
// //         //                     sh '''
// //         //                         docker build -f Dockerfile.test -t ${APP_NAME}:backend-test-${BUILD_NUMBER} .
// //         //                         mkdir -p test-results
// //         //                         docker run --rm \
// //         //                             -v ${PWD}/test-results:/app/test-results \
// //         //                             ${APP_NAME}:backend-test-${BUILD_NUMBER}
// //         //                     '''
// //         //                 }
// //         //             }
// //         //             post {
// //         //                 always {
// //         //                     archiveArtifacts artifacts: 'backend/test-results/**/*', allowEmptyArchive: true
// //         //                 }
// //         //             }
// //         //         }
                
// //         //         stage('Frontend Tests') {
// //         //             steps {
// //         //                 dir('frontend') {
// //         //                     sh '''
// //         //                         docker build -f Dockerfile.test -t ${APP_NAME}:frontend-test-${BUILD_NUMBER} .
// //         //                         mkdir -p coverage
// //         //                         docker run --rm \
// //         //                             -v ${PWD}/coverage:/app/coverage \
// //         //                             ${APP_NAME}:frontend-test-${BUILD_NUMBER}
// //         //                     '''
// //         //                 }
// //         //             }
// //         //             post {
// //         //                 always {
// //         //                     archiveArtifacts artifacts: 'frontend/coverage/**/*', allowEmptyArchive: true
// //         //                 }
// //         //             }
// //         //         }
// //         //     }
// //         // }
        
// //         stage('Build Application Images') {
// //             parallel {
// //                 stage('Build Backend') {
// //                     steps {
// //                         dir('backend') {
// //                             script {
// //                                 def backendImage = docker.build("${IMAGE_NAME}-backend:${BUILD_NUMBER}")
// //                             }
// //                         }
// //                     }
// //                 }
                
// //                 stage('Build Frontend') {
// //                     steps {
// //                         dir('frontend') {
// //                             script {
// //                                 def frontendImage = docker.build("${IMAGE_NAME}-frontend:${BUILD_NUMBER}")
// //                             }
// //                         }
// //                     }
// //                 }
// //             }
// //         }
        
// //         // stage('Integration Tests') {
// //         //     when {
// //         //         expression { currentBuild.currentResult == 'SUCCESS' }
// //         //     }
// //         //     steps {
// //         //         sh '''
// //         //             docker build -t ${APP_NAME}:integration-${BUILD_NUMBER} tests/integration/
                    
// //         //             docker compose up -d --build
                    
// //         //             echo "Waiting for services to start..."
// //         //             sleep 30
                    
// //         //             echo "Running integration tests..."
// //         //             docker run --rm \
// //         //                 --network automarkly_main_app-network \
// //         //                 -e BASE_URL=http://emailservice-frontend:80 \
// //         //                 ${APP_NAME}:integration-${BUILD_NUMBER}
// //         //         '''
// //         //     }
// //         //     post {
// //         //         always {
// //         //             sh '''
// //         //                 docker compose logs > integration-logs.txt || true
// //         //                 docker compose down -v || true
// //         //             '''
// //         //             archiveArtifacts artifacts: 'integration-logs.txt', allowEmptyArchive: true
// //         //         }
// //         //     }
// //         // }
        
// //         stage('Create Version Tag') {
// //             when { 
// //                 branch 'main' 
// //             }
// //             steps {
// //                 script {
// //                     echo "Creating version tag..."
                    
// //                     sshagent(credentials: ['github']) {
// //                         sh "git fetch --tags"
                        
// //                         try {
// //                             def lastTag = sh(script: "git tag --sort=-version:refname | head -1", returnStdout: true).trim()
// //                             echo "Found existing tag: ${lastTag}"
                            
// //                             def v = lastTag.tokenize('.')
// //                             def newPatch = v[2].toInteger() + 1
// //                             MAIN_TAG = v[0] + "." + v[1] + "." + newPatch
                            
// //                         } catch (Exception e) {
// //                             echo "No existing tags found, starting from 1.0.0"
// //                             MAIN_TAG = "1.0.0"
// //                         }
                        
// //                         echo "Generated new tag: ${MAIN_TAG}"
// //                     }
// //                 }
// //             }
// //         }
        
// // stage('Push to ECR') {
// //     when { 
// //         branch 'main'
// //     }
// //     steps {
// //         script {
// //             if (!MAIN_TAG || MAIN_TAG == '') {
// //                 echo "WARNING: MAIN_TAG not set, skipping ECR push"
// //                 return
// //             }
            
// //             echo "Pushing ${MAIN_TAG} to ECR..."
            
// //             sh '''
// //                 # Login to ECR using IAM role
// //                 aws ecr get-login-password --region "${AWS_REGION}" | \
// //                     docker login --username AWS --password-stdin "${ECR_URL}"
                
// //                 # Push Backend
// //                 docker tag "${IMAGE_NAME}-backend:${BUILD_NUMBER}" "${ECR_REPO_BACKEND}:${MAIN_TAG}"
// //                 docker push "${ECR_REPO_BACKEND}:${MAIN_TAG}"
// //                 docker tag "${IMAGE_NAME}-backend:${BUILD_NUMBER}" "${ECR_REPO_BACKEND}:latest"
// //                 docker push "${ECR_REPO_BACKEND}:latest"
                
// //                 # Push Frontend
// //                 docker tag "${IMAGE_NAME}-frontend:${BUILD_NUMBER}" "${ECR_REPO_FRONTEND}:${MAIN_TAG}"
// //                 docker push "${ECR_REPO_FRONTEND}:${MAIN_TAG}"
// //                 docker tag "${IMAGE_NAME}-frontend:${BUILD_NUMBER}" "${ECR_REPO_FRONTEND}:latest"
// //                 docker push "${ECR_REPO_FRONTEND}:latest"
// //             '''
            
// //             echo "Successfully pushed ${MAIN_TAG} to ECR"
// //         }
// //     }
// // }
// //         stage('Push Git Tag') {
// //             when { 
// //                 branch 'main'
// //             }
// //             steps {
// //                 script {
// //                     if (!MAIN_TAG || MAIN_TAG == '') {
// //                         echo "WARNING: MAIN_TAG not set, skipping git tag"
// //                         return
// //                     }
                    
// //                     echo "Pushing tag ${MAIN_TAG} to repository..."
                    
// //                     sshagent(credentials: ['github']) {
// //                         withCredentials([
// //                             string(credentialsId: 'git-username', variable: 'GIT_USERNAME'),
// //                             string(credentialsId: 'git-email', variable: 'GIT_EMAIL')
// //                         ]) {
// //                             sh """
// //                                 git config user.email "${GIT_EMAIL}"
// //                                 git config user.name "${GIT_USERNAME}"
                                
// //                                 git tag -a ${MAIN_TAG} -m "Release ${MAIN_TAG} - Build ${BUILD_NUMBER}"
// //                                 git push origin ${MAIN_TAG}
// //                             """
// //                         }
// //                     }
                    
// //                     echo "Tag ${MAIN_TAG} pushed successfully"
// //                 }
// //             }
// //         }
        
// //         stage('Deploy via GitOps') {
// //             when { 
// //                 branch 'main' 
// //             }
// //             steps {
// //                 script {
// //                     if (!MAIN_TAG || MAIN_TAG == '') {
// //                         echo "WARNING: MAIN_TAG not set, skipping GitOps update"
// //                         return
// //                     }
                    
// //                     sshagent(['github']) {
// //                         sh '''
// //                             rm -rf gitops-config
// //                             echo "Cloning GitOps repository..."
// //                             git clone ${GITOPS_REPO} gitops-config
// //                         '''
                        
// //                         withCredentials([
// //                             string(credentialsId: 'git-username', variable: 'GIT_USERNAME'),
// //                             string(credentialsId: 'git-email', variable: 'GIT_EMAIL')
// //                         ]) {
// //                             dir('gitops-config') {
// //                                 sh """
// //                                     git config user.email "${GIT_EMAIL}"
// //                                     git config user.name "${GIT_USERNAME}"

// //                                     # Update backend image tag
// //                                     sed -i 's|backendTag: ".*"|backendTag: "${MAIN_TAG}"|g' ${HELM_VALUES_PATH}
                                    
// //                                     # Update frontend image tag
// //                                     sed -i 's|frontendTag: ".*"|frontendTag: "${MAIN_TAG}"|g' ${HELM_VALUES_PATH}
                                    
// //                                     if git diff --quiet ${HELM_VALUES_PATH}; then
// //                                         echo "No changes to deploy - version ${MAIN_TAG} already deployed"
// //                                     else
// //                                         git add ${HELM_VALUES_PATH}
// //                                         git commit -m "Deploy ${APP_NAME} v${MAIN_TAG} - Build ${BUILD_NUMBER}"
// //                                         git push origin ${GITOPS_BRANCH}
// //                                         echo "GitOps updated: ${MAIN_TAG}"
// //                                     fi
// //                                 """
// //                             }
// //                         }
// //                     }
// //                 }
// //             }
// //         }
// //     }
    
// //     post {
// //         always {
// //             sh '''
// //                 docker compose down -v || true
// //                 rm -rf gitops-config || true
// //                 docker rmi ${APP_NAME}:backend-test-${BUILD_NUMBER} || true
// //                 docker rmi ${APP_NAME}:frontend-test-${BUILD_NUMBER} || true
// //                 docker rmi ${APP_NAME}:integration-${BUILD_NUMBER} || true
// //                 docker image prune -f || true
// //             '''
            
// //             cleanWs()
// //         }
        
// //         success {
// //             echo 'Pipeline completed successfully!'
// //         }
        
// //         failure {
// //             echo 'Pipeline failed!'
// //         }
// //     }
// // }





// //WITH ENV

// pipeline {
//     agent any
    
//     environment {
//         APP_NAME = 'automarkly'
//         BUILD_NUMBER = "${env.BUILD_NUMBER}"
//         IMAGE_NAME = 'automarkly/emailservice'
//         AWS_ACCOUNT_ID = credentials('aws-account-id')
//         AWS_REGION = credentials('aws_region')
//         ECR_URL = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com"
//         ECR_REPO_BACKEND = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/automarkly/emailservice-backend"
//         ECR_REPO_FRONTEND = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/automarkly/emailservice-frontend"
//         GITOPS_REPO = 'git@github.com:sara-golombeck/gitops-email-service.git'
//         GITOPS_BRANCH = 'main'
//         HELM_VALUES_PATH = 'charts/automarkly/values.yaml'
//     }
    
//     triggers {
//         githubPush()
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
//                                 docker build -f Dockerfile.test -t "${APP_NAME}:backend-test-${BUILD_NUMBER}" .
//                                 mkdir -p test-results
//                                 docker run --rm \
//                                     -v "${PWD}/test-results:/app/test-results" \
//                                     "${APP_NAME}:backend-test-${BUILD_NUMBER}"
//                             '''
//                         }
//                     }
//                     post {
//                         always {
//                             archiveArtifacts artifacts: 'backend/test-results/**/*', allowEmptyArchive: true
//                         }
//                     }
//                 }
                
//                 stage('Frontend Tests') {
//                     steps {
//                         dir('frontend') {
//                             sh '''
//                                 docker build -f Dockerfile.test -t "${APP_NAME}:frontend-test-${BUILD_NUMBER}" .
//                                 mkdir -p coverage
//                                 docker run --rm \
//                                     -v "${PWD}/coverage:/app/coverage" \
//                                     "${APP_NAME}:frontend-test-${BUILD_NUMBER}"
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
        
//         stage('Build Application Images') {
//             parallel {
//                 stage('Build Backend') {
//                     steps {
//                         dir('backend') {
//                             script {
//                                 def backendImage = docker.build("${IMAGE_NAME}-backend:${BUILD_NUMBER}")
//                             }
//                         }
//                     }
//                 }
                
//                 stage('Build Frontend') {
//                     steps {
//                         dir('frontend') {
//                             script {
//                                 def frontendImage = docker.build("${IMAGE_NAME}-frontend:${BUILD_NUMBER}")
//                             }
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
//                     docker build -t "${APP_NAME}:integration-${BUILD_NUMBER}" tests/integration/
                    
//                     docker compose up -d --build
                    
//                     echo "Waiting for services to start..."
//                     sleep 30
                    
//                     echo "Running integration tests..."
//                     docker run --rm \
//                         --network automarkly_main_app-network \
//                         -e BASE_URL=http://emailservice-frontend:80 \
//                         "${APP_NAME}:integration-${BUILD_NUMBER}"
//                 '''
//             }
//             post {
//                 always {
//                     sh '''
//                         docker compose logs > integration-logs.txt || true
//                         docker compose down -v || true
//                     '''
//                     archiveArtifacts artifacts: 'integration-logs.txt', allowEmptyArchive: true
//                 }
//             }
//         }
        
//         stage('Create Version Tag') {
//             when { 
//                 branch 'main' 
//             }
//             steps {
//                 script {
//                     echo "Creating version tag..."
                    
//                     sshagent(credentials: ['github']) {
//                         sh "git fetch --tags"
                        
//                         def newTag = "1.0.0"  // default
                        
//                         try {
//                             def lastTag = sh(script: "git tag --sort=-version:refname | head -1", returnStdout: true).trim()
//                             if (lastTag && lastTag != '') {
//                                 echo "Found existing tag: ${lastTag}"
                                
//                                 def v = lastTag.tokenize('.')
//                                 if (v.size() >= 3) {
//                                     def newPatch = v[2].toInteger() + 1
//                                     newTag = v[0] + "." + v[1] + "." + newPatch
//                                 }
//                             } else {
//                                 echo "No existing tags found, starting from 1.0.0"
//                             }
//                         } catch (Exception e) {
//                             echo "Error reading tags: ${e.getMessage()}, starting from 1.0.0"
//                         }
                        
//                         echo "Generated new tag: ${newTag}"
                        
//                         // Save as environment variable for other stages
//                         env.MAIN_TAG = newTag
                        
//                         echo "Version tag ${env.MAIN_TAG} prepared successfully"
//                     }
//                 }
//             }
//         }
        
//         stage('Push to ECR') {
//             when { 
//                 branch 'main'
//             }
//             steps {
//                 script {
//                     echo "Debug: env.MAIN_TAG value: '${env.MAIN_TAG}'"
//                     echo "Debug: BUILD_NUMBER: '${BUILD_NUMBER}'"
                    
//                     if (!env.MAIN_TAG || env.MAIN_TAG == '' || env.MAIN_TAG == 'null') {
//                         error("env.MAIN_TAG is empty, null, or invalid: '${env.MAIN_TAG}'")
//                     }
                    
//                     echo "Pushing ${env.MAIN_TAG} to ECR..."
                    
//                     sh '''
//                         # Debug: Print all variables
//                         echo "MAIN_TAG from env: ${MAIN_TAG}"
//                         echo "BUILD_NUMBER: ${BUILD_NUMBER}"
//                         echo "IMAGE_NAME: ${IMAGE_NAME}"
//                         echo "ECR_REPO_BACKEND: ${ECR_REPO_BACKEND}"
//                         echo "ECR_REPO_FRONTEND: ${ECR_REPO_FRONTEND}"
                        
//                         # Login to ECR
//                         aws ecr get-login-password --region "${AWS_REGION}" | \
//                             docker login --username AWS --password-stdin "${ECR_URL}"
                        
//                         # Push Backend
//                         docker tag "${IMAGE_NAME}-backend:${BUILD_NUMBER}" "${ECR_REPO_BACKEND}:${MAIN_TAG}"
//                         docker push "${ECR_REPO_BACKEND}:${MAIN_TAG}"
//                         docker tag "${IMAGE_NAME}-backend:${BUILD_NUMBER}" "${ECR_REPO_BACKEND}:latest"
//                         docker push "${ECR_REPO_BACKEND}:latest"
                        
//                         # Push Frontend
//                         docker tag "${IMAGE_NAME}-frontend:${BUILD_NUMBER}" "${ECR_REPO_FRONTEND}:${MAIN_TAG}"
//                         docker push "${ECR_REPO_FRONTEND}:${MAIN_TAG}"
//                         docker tag "${IMAGE_NAME}-frontend:${BUILD_NUMBER}" "${ECR_REPO_FRONTEND}:latest"
//                         docker push "${ECR_REPO_FRONTEND}:latest"
//                     '''
                    
//                     echo "Successfully pushed ${env.MAIN_TAG} to ECR"
//                 }
//             }
//         }
        
//         stage('Push Git Tag') {
//             when { 
//                 branch 'main'
//             }
//             steps {
//                 script {
//                     if (!env.MAIN_TAG || env.MAIN_TAG == '') {
//                         echo "WARNING: env.MAIN_TAG not set, skipping git tag"
//                         return
//                     }
                    
//                     echo "Pushing tag ${env.MAIN_TAG} to repository..."
                    
//                     sshagent(credentials: ['github']) {
//                         withCredentials([
//                             string(credentialsId: 'git-username', variable: 'GIT_USERNAME'),
//                             string(credentialsId: 'git-email', variable: 'GIT_EMAIL')
//                         ]) {
//                             sh '''
//                                 git config user.email "${GIT_EMAIL}"
//                                 git config user.name "${GIT_USERNAME}"
                                
//                                 git tag -a "${MAIN_TAG}" -m "Release ${MAIN_TAG} - Build ${BUILD_NUMBER}"
//                                 git push origin "${MAIN_TAG}"
//                             '''
//                         }
//                     }
                    
//                     echo "Tag ${env.MAIN_TAG} pushed successfully"
//                 }
//             }
//         }
        
//         stage('Deploy via GitOps') {
//             when { 
//                 branch 'main' 
//             }
//             steps {
//                 script {
//                     if (!env.MAIN_TAG || env.MAIN_TAG == '') {
//                         echo "WARNING: env.MAIN_TAG not set, skipping GitOps update"
//                         return
//                     }
                    
//                     sshagent(['github']) {
//                         sh '''
//                             rm -rf gitops-config
//                             echo "Cloning GitOps repository..."
//                             git clone "${GITOPS_REPO}" gitops-config
//                         '''
                        
//                         withCredentials([
//                             string(credentialsId: 'git-username', variable: 'GIT_USERNAME'),
//                             string(credentialsId: 'git-email', variable: 'GIT_EMAIL')
//                         ]) {
//                             dir('gitops-config') {
//                                 sh '''
//                                     git config user.email "${GIT_EMAIL}"
//                                     git config user.name "${GIT_USERNAME}"

//                                     # Update backend image tag
//                                     sed -i "s|backendTag: \\".*\\"|backendTag: \\"${MAIN_TAG}\\"|g" "${HELM_VALUES_PATH}"
                                    
//                                     # Update frontend image tag
//                                     sed -i "s|frontendTag: \\".*\\"|frontendTag: \\"${MAIN_TAG}\\"|g" "${HELM_VALUES_PATH}"
                                    
//                                     if git diff --quiet "${HELM_VALUES_PATH}"; then
//                                         echo "No changes to deploy - version ${MAIN_TAG} already deployed"
//                                     else
//                                         git add "${HELM_VALUES_PATH}"
//                                         git commit -m "Deploy ${APP_NAME} v${MAIN_TAG} - Build ${BUILD_NUMBER}"
//                                         git push origin "${GITOPS_BRANCH}"
//                                         echo "GitOps updated: ${MAIN_TAG}"
//                                     fi
//                                 '''
//                             }
//                         }
//                     }
//                 }
//             }
//         }
//     }
    
//     post {
//         always {
//             sh '''
//                 docker compose down -v || true
//                 rm -rf gitops-config || true
//                 docker rmi "${APP_NAME}:backend-test-${BUILD_NUMBER}" || true
//                 docker rmi "${APP_NAME}:frontend-test-${BUILD_NUMBER}" || true
//                 docker rmi "${APP_NAME}:integration-${BUILD_NUMBER}" || true
//                 docker image prune -f || true
//             '''
            
//             cleanWs()
//         }
        
//         success {
//             echo 'Pipeline completed successfully!'
//         }
        
//         failure {
//             echo 'Pipeline failed!'
//         }
//     }
// }





pipeline {
    agent any
    
    environment {
        APP_NAME = 'automarkly'
        BUILD_NUMBER = "${env.BUILD_NUMBER}"
        IMAGE_NAME = 'automarkly/emailservice'
        AWS_ACCOUNT_ID = credentials('aws-account-id')
        AWS_REGION = credentials('aws_region')
        ECR_URL = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com"
        ECR_REPO_BACKEND = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/automarkly/emailservice-backend"
        ECR_REPO_FRONTEND = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/automarkly/emailservice-frontend"
        GITOPS_REPO = 'git@github.com:sara-golombeck/gitops-email-service.git'
        GITOPS_BRANCH = 'main'
        HELM_VALUES_PATH = 'charts/automarkly/values.yaml'
    }
    
    triggers {
        githubPush()
    }
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Verify IAM Role Access') {
            steps {
                script {
                    echo "Testing IAM Role access from Jenkins host..."
                    
                    // בדיקה שIAM Role עובד מה-host
                    def hostCheck = sh(script: "aws sts get-caller-identity", returnStatus: true)
                    if (hostCheck != 0) {
                        error("IAM Role not working on Jenkins host!")
                    }
                    
                    echo "Testing IAM Role access from container..."
                    
                    // בדיקה שIAM Role עובד מcontainer
                    def containerCheck = sh(script: '''
                        docker run --rm amazon/aws-cli:latest aws sts get-caller-identity
                    ''', returnStatus: true)
                    
                    if (containerCheck == 0) {
                        echo "✅ IAM Role works from containers! Using it for tests."
                        env.USE_IAM_ROLE = "true"
                    } else {
                        error("❌ IAM Role doesn't work from containers. Check hop limit configuration!")
                    }
                }
            }
        }
        
        stage('Unit Tests') {
            parallel {
                stage('Backend Tests') {
                    steps {
                        dir('backend') {
                            sh '''
                                docker build -f Dockerfile.test -t "${APP_NAME}:backend-test-${BUILD_NUMBER}" .
                                mkdir -p test-results
                                docker run --rm \
                                    -v "${PWD}/test-results:/app/test-results" \
                                    "${APP_NAME}:backend-test-${BUILD_NUMBER}"
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
                                docker build -f Dockerfile.test -t "${APP_NAME}:frontend-test-${BUILD_NUMBER}" .
                                mkdir -p coverage
                                docker run --rm \
                                    -v "${PWD}/coverage:/app/coverage" \
                                    "${APP_NAME}:frontend-test-${BUILD_NUMBER}"
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
        
        stage('Build Application Images') {
            parallel {
                stage('Build Backend') {
                    steps {
                        dir('backend') {
                            script {
                                def backendImage = docker.build("${IMAGE_NAME}-backend:${BUILD_NUMBER}")
                            }
                        }
                    }
                }
                
                stage('Build Frontend') {
                    steps {
                        dir('frontend') {
                            script {
                                def frontendImage = docker.build("${IMAGE_NAME}-frontend:${BUILD_NUMBER}")
                            }
                        }
                    }
                }
            }
        }
        
        stage('Integration Tests with IAM Role') {
            when {
                expression { currentBuild.currentResult == 'SUCCESS' }
            }
            steps {
                sh '''
                    echo "Building integration test image..."
                    docker build -t "${APP_NAME}:integration-${BUILD_NUMBER}" tests/integration/
                    
                    echo "Starting application with IAM Role access..."
                    export COMPOSE_PROJECT_NAME="test-${BUILD_NUMBER}"
                    
                    # בדיקה מהירה שIAM Role עדיין עובד
                    docker run --rm amazon/aws-cli:latest aws sts get-caller-identity
                    
                    # הרמת הסביבה - containers יקבלו IAM Role אוטומטית
                    docker compose up -d --build
                    
                    echo "Waiting for services to start..."
                    sleep 30
                    
                    echo "Running integration tests..."
                    docker run --rm \
                        --network "test-${BUILD_NUMBER}_app-network" \
                        -e BASE_URL=http://emailservice-frontend:80 \
                        "${APP_NAME}:integration-${BUILD_NUMBER}"
                '''
            }
            post {
                always {
                    sh '''
                        echo "Collecting logs..."
                        export COMPOSE_PROJECT_NAME="test-${BUILD_NUMBER}"
                        docker compose logs > "integration-logs-${BUILD_NUMBER}.txt" || true
                        docker compose down -v || true
                    '''
                    archiveArtifacts artifacts: "integration-logs-${BUILD_NUMBER}.txt", allowEmptyArchive: true
                }
            }
        }
        
        stage('Create Version Tag') {
            when { 
                branch 'main' 
            }
            steps {
                script {
                    echo "Creating version tag..."
                    
                    sshagent(credentials: ['github']) {
                        sh "git fetch --tags"
                        
                        def newTag = "1.0.0"
                        
                        try {
                            def lastTag = sh(script: "git tag --sort=-version:refname | head -1", returnStdout: true).trim()
                            if (lastTag && lastTag != '') {
                                echo "Found existing tag: ${lastTag}"
                                
                                def v = lastTag.tokenize('.')
                                if (v.size() >= 3) {
                                    def newPatch = v[2].toInteger() + 1
                                    newTag = v[0] + "." + v[1] + "." + newPatch
                                }
                            } else {
                                echo "No existing tags found, starting from 1.0.0"
                            }
                        } catch (Exception e) {
                            echo "Error reading tags: ${e.getMessage()}, starting from 1.0.0"
                        }
                        
                        echo "Generated new tag: ${newTag}"
                        env.MAIN_TAG = newTag
                        echo "Version tag ${env.MAIN_TAG} prepared successfully"
                    }
                }
            }
        }
        
        stage('Push to ECR with IAM Role') {
            when { 
                branch 'main'
            }
            steps {
                script {
                    if (!env.MAIN_TAG || env.MAIN_TAG == '' || env.MAIN_TAG == 'null') {
                        error("env.MAIN_TAG is empty, null, or invalid: '${env.MAIN_TAG}'")
                    }
                    
                    echo "Pushing ${env.MAIN_TAG} to ECR using IAM Role..."
                    
                    sh '''
                        echo "Using IAM Role for ECR access - no credentials needed!"
                        
                        # בדיקה שIAM Role עדיין עובד
                        aws sts get-caller-identity
                        
                        # התחברות ל-ECR עם IAM Role
                        aws ecr get-login-password --region "${AWS_REGION}" | \
                            docker login --username AWS --password-stdin "${ECR_URL}"
                        
                        # Push Backend
                        docker tag "${IMAGE_NAME}-backend:${BUILD_NUMBER}" "${ECR_REPO_BACKEND}:${MAIN_TAG}"
                        docker push "${ECR_REPO_BACKEND}:${MAIN_TAG}"
                        docker tag "${IMAGE_NAME}-backend:${BUILD_NUMBER}" "${ECR_REPO_BACKEND}:latest"
                        docker push "${ECR_REPO_BACKEND}:latest"
                        
                        # Push Frontend
                        docker tag "${IMAGE_NAME}-frontend:${BUILD_NUMBER}" "${ECR_REPO_FRONTEND}:${MAIN_TAG}"
                        docker push "${ECR_REPO_FRONTEND}:${MAIN_TAG}"
                        docker tag "${IMAGE_NAME}-frontend:${BUILD_NUMBER}" "${ECR_REPO_FRONTEND}:latest"
                        docker push "${ECR_REPO_FRONTEND}:latest"
                    '''
                    
                    echo "Successfully pushed ${env.MAIN_TAG} to ECR using IAM Role"
                }
            }
        }
        
        stage('Push Git Tag') {
            when { 
                branch 'main'
            }
            steps {
                script {
                    if (!env.MAIN_TAG || env.MAIN_TAG == '') {
                        echo "WARNING: env.MAIN_TAG not set, skipping git tag"
                        return
                    }
                    
                    echo "Pushing tag ${env.MAIN_TAG} to repository..."
                    
                    sshagent(credentials: ['github']) {
                        withCredentials([
                            string(credentialsId: 'git-username', variable: 'GIT_USERNAME'),
                            string(credentialsId: 'git-email', variable: 'GIT_EMAIL')
                        ]) {
                            sh '''
                                git config user.email "${GIT_EMAIL}"
                                git config user.name "${GIT_USERNAME}"
                                
                                git tag -a "${MAIN_TAG}" -m "Release ${MAIN_TAG} - Build ${BUILD_NUMBER}"
                                git push origin "${MAIN_TAG}"
                            '''
                        }
                    }
                    
                    echo "Tag ${env.MAIN_TAG} pushed successfully"
                }
            }
        }
        
        stage('Deploy via GitOps') {
            when { 
                branch 'main' 
            }
            steps {
                script {
                    if (!env.MAIN_TAG || env.MAIN_TAG == '') {
                        echo "WARNING: env.MAIN_TAG not set, skipping GitOps update"
                        return
                    }
                    
                    sshagent(['github']) {
                        sh '''
                            rm -rf gitops-config
                            echo "Cloning GitOps repository..."
                            git clone "${GITOPS_REPO}" gitops-config
                        '''
                        
                        withCredentials([
                            string(credentialsId: 'git-username', variable: 'GIT_USERNAME'),
                            string(credentialsId: 'git-email', variable: 'GIT_EMAIL')
                        ]) {
                            dir('gitops-config') {
                                sh '''
                                    git config user.email "${GIT_EMAIL}"
                                    git config user.name "${GIT_USERNAME}"

                                    # Update backend image tag
                                    sed -i "s|backendTag: \\".*\\"|backendTag: \\"${MAIN_TAG}\\"|g" "${HELM_VALUES_PATH}"
                                    
                                    # Update frontend image tag
                                    sed -i "s|frontendTag: \\".*\\"|frontendTag: \\"${MAIN_TAG}\\"|g" "${HELM_VALUES_PATH}"
                                    
                                    if git diff --quiet "${HELM_VALUES_PATH}"; then
                                        echo "No changes to deploy - version ${MAIN_TAG} already deployed"
                                    else
                                        git add "${HELM_VALUES_PATH}"
                                        git commit -m "Deploy ${APP_NAME} v${MAIN_TAG} - Build ${BUILD_NUMBER}"
                                        git push origin "${GITOPS_BRANCH}"
                                        echo "GitOps updated: ${MAIN_TAG}"
                                    fi
                                '''
                            }
                        }
                    }
                }
            }
        }
    }
    
    post {
        always {
            sh '''
                docker compose down -v || true
                rm -rf gitops-config || true
                docker rmi "${APP_NAME}:backend-test-${BUILD_NUMBER}" || true
                docker rmi "${APP_NAME}:frontend-test-${BUILD_NUMBER}" || true
                docker rmi "${APP_NAME}:integration-${BUILD_NUMBER}" || true
                docker image prune -f || true
            '''
            
            cleanWs()
        }
        
        success {
            echo 'Pipeline completed successfully using IAM Role!'
        }
        
        failure {
            echo 'Pipeline failed!'
        }
    }
}