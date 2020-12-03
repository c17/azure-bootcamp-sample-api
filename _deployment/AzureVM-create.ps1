. "$PSScriptRoot/AzureVM-config.ps1"

Write-Host "Checking existing vnet for resource group $RG ..."
$SUBNET = $null
$vnets = az network vnet list -g $RG | ConvertFrom-Json
if (!$?) {
    throw "Failed to get existing vnet"
}
if (!$vnets[0].name) {
    Write-Host -NoNewline "Non existing VNet, creating $VNET ..."
    az network vnet create --name $VNET `
        --resource-group $RG `
        -o none
    if (!$?) {
        throw "Failed to create the VNet"
    }
    Write-Host "`tdone"
} else {
    $VNET = $vnets[0].name
    $SUBNET = $vnets[0].subnets[0].name
    Write-Host "Found '$VNET' with subnet '$SUBNET'"
}


Write-Host "Creating the Network Security Group $VM_NSG ..."
az network nsg create --name $VM_NSG `
    --resource-group $RG `
    --location $LOCATION `
    -o none
if (!$?) {
    throw "Failed to create the NSG"
}
Write-Host "`tdone"

if ($null -eq $SUBNET) {
    Write-Host "Creating default subnet attached to the NSG ..."
    $SUBNET = "default"
    az network vnet subnet create --name $SUBNET `
        --resource-group $RG `
        --vnet-name $VNET `
        --address-prefixes "10.0.0.0/24"
    Write-Host "`tdone"
}

Write-Host "Creating the public IP $VM_PIP_NAME ..."
az network public-ip create --name $VM_PIP_NAME `
    --resource-group $RG `
    --sku "Basic" `
    -o none
if (!$?) {
    throw "Failed to create the public IP"
}
Write-Host "`tdone"


Write-Host "Creating VM $VM_NAME ..."
az vm create --name $VM_NAME `
    --resource-group $RG `
    --location $LOCATION `
    --image $VM_IMAGE `
    --size $VM_SIZE `
    --admin-username $ADMINUSER `
    --vnet-name $VNET `
    --subnet $SUBNET `
    --storage-sku $VM_OSDISK_SKU `
    --public-ip-address $VM_PIP_NAME `
    --nsg $VM_NSG `
    -o none
if (!$?) {
    throw "Failed to create VM"
}
Write-Host "`tdone"


Write-Host "Creating and attaching the data disk $DATADISK_NAME ..."
az vm disk attach --name $DATADISK_NAME `
    --resource-group $RG `
    --vm-name $VM_NAME `
    --sku $DATADISK_SKU `
    --size-gb $DATADISK_SIZE `
    --new
if (!$?) {
    throw "Failed to create the data disk"
}
Write-Host "`tdone"


Write-Host "Allowing port 80 on the Network Security Group $VM_NSG ..."
az network nsg rule create --name "Port_80"`
    --nsg-name $VM_NSG `
    --resource-group $RG `
    --priority 1010 `
    --access "Allow" `
    --destination-port-ranges 80 `
    -o none
if (!$?) {
    throw "Failed to configure the NSG"
}
Write-Host "`tdone"


Write-Host "Creation completed successfully"
