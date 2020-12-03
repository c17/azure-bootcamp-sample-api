##  automatically delete : OS Disk and Network interface from VM information

param (
    [Alias("del-vnet")][bool] $DeleteVnet = $false
    )

. "$PSScriptRoot/AzureVM-config.ps1"

Write-Host -NoNewline "Checking VM '$VM_NAME' running state ..."
$VM = $(az vm list -d --query "[?name=='$VM_NAME']" | ConvertFrom-Json)[0]
if (!$?) {
    throw "Failed to get VM status"
}
Write-Host "`t$($VM.powerState)"
if ($VM.powerState -eq "VM running") {
    Write-Host -NoNewline "Stopping the VM '$VM_NAME' ..."
    az vm stop --ids $VM.id
    if (!$?) {
        throw "Failed to stop the VM"
    }
    Write-Host "done"
}

Write-Host -NoNewline "Deleting the VM '$VM_NAME' ..."
if (![string]::IsNullOrWhiteSpace($VM.id)) {
    Write-Host
    az vm delete --ids $VM.id --yes
    if (!$?) {
        throw "Failed to delete the VM"
    }
    Write-Host "`tdone"
} else {
    Write-Host "`skipped (VM doesn't exist)"
}

$disk = $VM.storageProfile.osDisk.name
if ([string]::IsNullOrWhiteSpace($VM.storageProfile.osDisk.managedDisk.id)) {
    Write-Warning "Unable to retrieve the OS disk (skipping)"
} else {
    Write-Host -NoNewline "Deleting the OS disk '$disk' ..."
    Write-Host
    az disk delete --ids $VM.storageProfile.osDisk.managedDisk.id --yes
    if (!$?) {
        throw "Failed to delete the disk"
    }
    Write-Host "`tdone"
}


$nic = $VM.networkProfile.networkInterfaces[0].id
if ([string]::IsNullOrWhiteSpace($nic)) {
    Write-Warning "Unable to retrieve the Network interface (skipping)"
} else {
    Write-Host -NoNewline "Deleting the Network interface ..."
    az network nic delete --ids $nic -o none
    if (!$?) {
        throw "Failed to delete the Network interface"
    }
    Write-Host "`tdone"
}


Write-Host -NoNewline "Deleting the public IP '$VM_PIP_NAME' ..."
Write-Host
az network public-ip delete --name $VM_PIP_NAME --resource-group $RG -o none
if (!$?) {
    throw "Failed to delete the public IP"
}
Write-Host "`tdone"


Write-Host -NoNewline "Deleting the data disk '$DATADISK_NAME' ..."
Write-Host
az disk delete --name $DATADISK_NAME --resource-group $RG --yes
if (!$?) {
    throw "Failed to delete the disk"
}
Write-Host "`tdone"


Write-Host -NoNewline "Deleting the NSG '$VM_NSG' ..."
Write-Host
az network nsg delete --name $VM_NSG --resource-group $RG -o none
if (!$?) {
    throw "Failed to delete the NSG"
}
Write-Host "`tdone"


Write-Host -NoNewline "Deleting the VNet '$VNET' ..."
if (!$DeleteVnet) {
    Write-Host "`tskipped"
} else {
    Write-Host
    az network vnet delete --name $VNET --resource-group $RG -o none
    if (!$?) {
        throw "Failed to delete the NSG"
    }
    Write-Host "`tdone"
}

Write-Host "Cleanup completed"
