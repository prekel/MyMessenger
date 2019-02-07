dotnet test MyMessenger.Server.Tests --test-adapter-path:. --logger:Appveyor
if ($LASTEXITCODE -ne 0)
{
	Exit-AppveyorBuild
}