FROM openjdk:17-jdk-alpine
ARG JAR_FILE=target/*.jar
COPY /target/receiver-backend-project-0.0.1.jar app.jar
ADD https://github.com/open-telemetry/opentelemetry-java-instrumentation/releases/latest/download/opentelemetry-javaagent.jar .
ENV JAVA_TOOL_OPTIONS="-javaagent:./opentelemetry-javaagent.jar"
ENTRYPOINT ["java","-jar","/app.jar"]