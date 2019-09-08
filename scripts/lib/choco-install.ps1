function ChocoInstall 
{
    $chocoIsInstalled = (Test-Path "$env:ChocolateyInstall")

    if($chocoIsInstalled -and (powershell choco -v))
    {
        write-host "Choco is already installed... skipping"
        return
    }

    write-host "Choco not found... installing"
    Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
    write-host "Choco installed... "

    write-host "Refreshing environment..."
    $env:ChocolateyInstall = Convert-Path "$((Get-Command choco).path)\..\.."
    Import-Module "$env:ChocolateyInstall\helpers\chocolateyProfile.psm1"

    refreshenv

    write-host "Environment variables refreshed..."
}


