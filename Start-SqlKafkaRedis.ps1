[CmdletBinding()]
param(
    [string] $Location = (Join-Path $PSScriptRoot 'bin/Debug/net6.0'),
    [string] $SqlPw = "Test123Friday!"
)
# https://github.com/testcontainers/testcontainers-dotnet

$ErrorActionPreference = 'Stop'
$InformationPreference = 'Continue'
Set-StrictMode -Version Latest

Push-Location $Location

$testContainers = $null

try {
    Add-Type -Path .\Testcontainers.dll

    $dbBuilder = New-Object "DotNet.Testcontainers.Builders.TestcontainersBuilder[DotNet.Testcontainers.Containers.MsSqlTestcontainer]"
    $config = New-Object "DotNet.Testcontainers.Configurations.MsSqlTestcontainerConfiguration"
    $config.Password = $SqlPw
    [DotNet.Testcontainers.Builders.TestcontainersBuilderDatabaseExtension]::WithDataBase($dbBuilder, $config)

    $testContainers = $dbBuilder.Build()

    $task = $testContainers.StartAsync()
    $task.Result

    Write-Information "Connect with Data Source=127.0.0.1,49155;Database=master;User Id=sa;Password=$SqlPw"

} catch {
    Write-Error "Ow!`n$_`n$($_.ScriptStackTrace)"
} finally {
    if ($testContainers) {
        Read-Host "Press enter to dispose"
        Write-Information "Disposing" -InformationAction Continue

        $global:result = $testContainers.DisposeAsync()
    } else {
        Write-Information "NOT Disposing" -InformationAction Continue
    }
    Pop-Location
}