Set-ExecutionPolicy RemoteSigned

start-process dotnet -ArgumentList run -WorkingDirectory ./ApiGateway
start-process dotnet -ArgumentList run -WorkingDirectory ./Login
start-process dotnet -ArgumentList run -WorkingDirectory ./PatientsService
