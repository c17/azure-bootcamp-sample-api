# azure-bootcamp-sample-api

## Deployment

1. Edit `AzureVM-config.ps1` file to fulfill your `$tenant` and resource group `$RG`
2. Run `AzureVM-create.ps1` to automatically create the VM
3. Configure IIS and NETCore 3.1 hosting runtime
4. Deploy the API
5. Run `AzureVM-cleanup.ps1` to delete resources  
    > By default, the cleanup does **not** delete the VNet. To enforce it, run `AzureVM-cleanup.ps1 -del-vnet 1`
