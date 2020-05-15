@echo off
echo Generating Java classes
protoc -I=. --java_out=javaFiles AthletesModel.proto AthletesRequest.proto AthletesResponse.proto
echo Successfully created
