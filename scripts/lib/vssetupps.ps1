function VSSetupPowerShellInstall()
{
    try
    {
        Install-PackageProvider -Name NuGet -MinimumVersion 2.8.5.201 -Force
        $vsSetupPs = Get-VSSetupInstance
        write-host "VSSetupPowershell found... skipping"
    }
    catch
    {
        write-host "VSSetupPowershell not found... installing"
        Install-Module VSSetup -Force
        write-host "VSSetupPowershell installed"
        refreshenv
    }
}