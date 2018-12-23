dotnet publish -c Release /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION"
dotnet publish -c Release -r win-x64 /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION"
dotnet publish -c Release -r lunux-x64 /p:AssemblyVersion="$env:APPVEYOR_BUILD_VERSION"
