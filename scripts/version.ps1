$env:rev = git rev-list HEAD --count
$env:assembly_version = "$env:APPVEYOR_BUILD_VERSION.$env:rev"
Write-Host -Backgroundcolor Green -Foregroundcolor White "Assembly Version: $env:assembly_version"