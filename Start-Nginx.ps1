# https://github.com/testcontainers/testcontainers-dotnet

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

Push-Location C:\code\testcontainer\bin\Debug\net6.0

$testContainers = $null

try {
    Add-Type -Path .\Testcontainers.dll
    # add-type -path .\Microsoft.Extensions.Logging.Abstractions.dll
    # [DotNet.Testcontainers.Configurations.TestcontainersSettings]::Logger = [Microsoft.Extensions.Logging.Abstractions.NullLogger]::Instance
    $builder = New-Object "DotNet.Testcontainers.Builders.TestcontainersBuilder[DotNet.Testcontainers.Containers.TestcontainersContainer]"

    $testContainersBuilder = $builder.WithImage("nginx").
                                WithName("nginx").
                                WithPortBinding(80).
                                WithWaitStrategy([DotNet.Testcontainers.Builders.Wait]::ForUnixContainer().
                                UntilPortIsAvailable(80))

    $testContainers = $testContainersBuilder.Build()

    $task = $testContainers.StartAsync()
    $task.Result

    Write-Information "Started nginx" -InformationAction Continue

} finally {
    if ($testContainers) {
        Read-Host "Press enter to dispose"
        Write-Information "Disposing" -InformationAction Continue

        $testContainers.DisposeAsync()
    } else {
        Write-Information "NOT Disposing" -InformationAction Continue
    }
    Pop-Location
}