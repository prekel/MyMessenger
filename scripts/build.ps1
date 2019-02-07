dotnet build /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION" /verbosity:minimal /m /logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
if ($LASTEXITCODE -ne 0)
{
	Exit-AppveyorBuild
}