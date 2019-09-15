function VSInstall 
{
    $vsIntances = Get-VSSetupInstance -All
    $vsMinimumVersion = 16
    $vsMininumFound = $FALSE

    $vsIntances | ForEach-Object {
        if ($_.InstallationVersion.Major -ge $vsMinimumVersion) 
        {
            write-output "VS 2019 or higher found... skipping"
            $vsMininumFound = $TRUE
            return
        }
    }

    if ($vsMininumFound) { return }

    write-output "VS 2019 not found.. installing"
    choco install visualstudio2019community -Force
    
}