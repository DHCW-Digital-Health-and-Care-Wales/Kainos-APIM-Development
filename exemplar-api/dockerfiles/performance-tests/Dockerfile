FROM eclipse-temurin:21-jdk AS build

WORKDIR /performance-tests

COPY ./certs /usr/local/share/ca-certificates
COPY ./test/performance /performance-tests

RUN apt-get update && \
    apt-get install -y ca-certificates && \
    update-ca-certificates && \
	apt-get install -y maven

CMD mvn gatling:test -Dgatling.resultsFolder=/test-results
