. $PSScriptRoot\lib\chocolatey.ps1
. $PSScriptRoot\lib\docker.ps1
. $PSScriptRoot\lib\visualstudio.ps1
. $PSScriptRoot\lib\vssetupps.ps1
. $PSScriptRoot\lib\dotnetcore.ps1

ChocoInstall
DockerInstall
VSSetupPowerShellInstall
VSInstall
DotNetCoreSdkInstall