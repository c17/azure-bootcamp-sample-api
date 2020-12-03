$PSDefaultParameterValues['Out-File:Encoding'] = 'utf8'
$OutputEncoding = [System.Text.Encoding]::UTF8

switch ($args[0]) {
    "create" {
        $params = $args | Select -skip 1
        & "$PSScriptRoot/AzureVM-create.ps1" @params
    }
    "cleanup" {
        $params = $args | Select -skip 1
        & "$PSScriptRoot/AzureVM-cleanup.ps1" @params
    }
    Default { Write-Host "[create|cleanup]" }
}
