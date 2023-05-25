pipeline {
    agent any
    triggers {
        pollSCM("* * * * *")
    }
    environment {
        DEPLOY_NUMBER = "${BUILD_NUMBER}"
        dockerimageapiname = "nitrozeus1/api-search:${DEPLOY_NUMBER}"
        dockerimageloadname = "nitrozeus1/web-search:${DEPLOY_NUMBER}"
        dockerimagewebname = "nitrozeus1/search-engine-load-balancer:${DEPLOY_NUMBER}"
        dockerimageapi = ""
        dockerimageload = ""
        dockerimageweb = ""
    }
    stages {
        stage('Checkout Source') {
            steps {
            git 'https://github.com/faustis1337/SynopsisProjects.git'
            }
        }
        
        stage('Build image') {
        steps{
            script {
                dockerimageapi = docker.build dockerimageapiname
                dockerimageload = docker.build dockerimageloadname
                dockerImageweb = docker.build dockerimagewebname
            }
        }
        
        stage('Pushing Image') {
            environment {
                    registryCredential = 'DockerHub'
                }
            steps{
                script {
                    docker.withRegistry( 'https://registry.hub.docker.com', registryCredential ) {
                    dockerImage.push("latest")
                    }
                }
            }  
        }
        
        stage('Deploying to Kubernetes') {
            steps {
                script {
                kubernetesDeploy(configs: "deployment.yaml", "service.yaml")
                }
            }
        }
	}
}
    
//         stage("Build") {
//             steps {
//                 sh "dotnet build"
//                 sh "docker compose build"
//                 
//             }
//         }
//         stage("Deliver") {
//             steps {
//                 withCredentials([usernamePassword(credentialsId: 'DockerHub', passwordVariable: 'DH_PASSWORD', usernameVariable: 'DH_USERNAME')]) {
//                     sh 'docker login -u $DH_USERNAME -p $DH_PASSWORD'
//                     sh "docker compose push"
//                 }
//             }
//         }
//         stage("Deploy") {
//             steps {
//                 build job: 'SE-Deploy', parameters: [[$class: 'StringParameterValue', name: 'DEPLOY_NUMBER', value: "${BUILD_NUMBER}"]]
//             }   
//         }
//    }
//}
