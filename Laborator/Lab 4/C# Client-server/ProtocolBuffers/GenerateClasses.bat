
echo "Generating C# classes"
protoc -I=. --java_out=javaFiles --csharp_out=csharpFiles AthletesModel.proto AthletesRequest.proto AthletesResponse.proto
