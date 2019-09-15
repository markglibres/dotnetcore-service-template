function DotNetCoreSdkInstall
{
    $minVersionFound = $FALSE
    $minVersion = [decimal]2.2
    try
    {
        $installedVersions = (dir (Get-Command dotnet).Path.Replace('dotnet.exe', 'sdk')).Name
        $installedVersions | ForEach-Object { 
            
            $isValidVersion = $_ -match "\d+\.\d+"
            if($isValidVersion) { 
                
                $version = [decimal]$Matches[0]

                if($version -ge $minVersion) { 
                    $minVersionFound = $TRUE 
                    write-output "DotNetCore SDK $version found.. skipping"
                    return
                }
            }
        }
    }
    catch 
    {

    }

    if($minVersionFound) { return }

    write-output "DotNetCore SDK $minVersion not found.. installing"
    choco install dotnetcore-sdk --version 2.2.203
}