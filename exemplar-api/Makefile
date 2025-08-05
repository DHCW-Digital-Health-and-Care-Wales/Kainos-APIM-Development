.PHONY: restore build clean

TARGET_DIR?=/api

restore:
	dotnet restore
	
build: 
	dotnet publish ./src/PatientDemographicsAPI.csproj -c release -p:AssemblyName=patient_demographics_api -o $(TARGET_DIR)

format:
	dotnet format $(TARGET_DIR)

clean:
	rm -rf **/bin/
	rm -rf **/obj/