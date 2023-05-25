pipeline {
    agent any
    triggers {
        pollSCM("H * * * *")
    }
    environment {
        DEPLOY_NUMBER = "${BUILD_NUMBER}"
        dockerimageapiname = "nitrozeus1/api-search:latest" //"nitrozeus1/api-search:${DEPLOY_NUMBER}"
        dockerimageloadname = "nitrozeus1/web-search:latest" // "nitrozeus1/web-search:${DEPLOY_NUMBER}"
        dockerimagewebname = "nitrozeus1/search-engine-load-balancer:latest" //"nitrozeus1/search-engine-load-balancer:${DEPLOY_NUMBER}"
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
                dockerimageweb = docker.build dockerimagewebname
                }
            }
        }
        
        
        stage('Pushing Image') {
            environment {
                            registryCredential = 'DockerHub'
                        }
            steps
            {
                script 
                {
                    docker.withRegistry( 'https://registry.hub.docker.com', registryCredential ) 
                        {
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