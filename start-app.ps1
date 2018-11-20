Set-ExecutionPolicy RemoteSigned

start-process dotnet -ArgumentList run -WorkingDirectory ./ApiGateway
Start-Sleep -s 2

start-process dotnet -ArgumentList run -WorkingDirectory ./Login
Start-Sleep -s 2

start-process dotnet -ArgumentList run -WorkingDirectory ./PatientsService
Start-Sleep -s 2

start-process dotnet -ArgumentList run -WorkingDirectory ./HospitalsService
Start-Sleep -s 2

start-process dotnet -ArgumentList run -WorkingDirectory ./EmergencyRequestDispatchService
Start-Sleep -s 2

start-process dotnet -ArgumentList run -WorkingDirectory ./AmbulanceDispatchService
Start-Sleep -s 2
