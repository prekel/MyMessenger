dotnet build /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION" /verbosity:minimal /m
if ($LASTEXITCODE -ne 0)
{
	Exit-AppveyorBuild
}