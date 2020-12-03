
$tenant = "<tenant>"
$RG = "<resource_group_name>"
$LOCATION = "FranceCentral"
$VNET = "$RG-vnet"
$VM_NAME = "AzBootcampVM1"
$ADMINUSER = "bootcamp"
$VM_NSG = "$VM_NAME-nsg"

#   to get default image list
#>  az vm image list --output table
#   to get all using filtering for WindowsServer
#>  az vm image list --offer WindowsServer --all --output table
$VM_IMAGE = "Win2019Datacenter"

#   to get available VM sizes
#>  az vm list-sizes --location $LOCATION --output table
$VM_SIZE = "Standard_B1ms"

$VM_OSDISK_SKU = "StandardSSD_LRS"

$DATADISK_NAME = "$VM_NAME-datadisk"
$DATADISK_SKU = "Standard_LRS"
$DATADISK_SIZE = 4

# public IP name
$VM_PIP_NAME = "$VM_NAME-pip"

function AzLogin {
    param (
        
    )
    az login --tenant $tenant | Out-Null
    if (!$?) {
        throw "Failed to login"
    }
}

$account = az account show | ConvertFrom-Json
if (!$?) {
    Write-Host "Failed to get current account, prompting for login"
    AzLogin
} else {
    $confirm = $Host.UI.PromptForChoice("Confirm account $($account.user.name) on tenant $($account.name)", "Do you want to continue with this account ?", @("&Yes", "&No"), 1)
    switch ($confirm) {
        0 { break }
        Default {
            az logout
            AzLogin
        }
    }
}
Write-Host "Connected with account $($account.user.name) on tenant $($account.name)"
