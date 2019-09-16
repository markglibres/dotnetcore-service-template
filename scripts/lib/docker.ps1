function DockerServiceInstall
{
    try
    {
        $dockerCheck = docker -v
        write-host "Docker found... skipping"
    }
    catch
    {
        write-host "Docker not found... installing"
        choco install docker-desktop -y --force
        write-host "Docker installed"
        write-host "Starting docker"
        refreshenv
        $service = Get-Service com.docker.service
        $service.WaitForStatus("Running", '00:01:00')
        write-host "Docker started"
    }
}

function DockerComposeInstall
{
    try
    {
        $dockerCheck = docker-compose -v
        write-host "Docker compose found... skipping"
    }
    catch
    {
        write-host "Docker compose not found... installing"
        choco install docker-compose -y --force
        write-host "Docker compose installed"
    }
}

function DockerInstall 
{
    DockerServiceInstall
    DockerComposeInstall
    
}