---
title: Service Tags
guid: "c3ed2fe1-c096-4171-86a6-ff8dfac8716c"
---

## Overview

Azure SaaS and PaaS services have enormous, constantly changing  lists of IP ranges, which would make managing rules for these services in an NSG a nightmare.

For this reason, Azure has the concept of Service Tags which are an alias for these IP ranges, maintained automatically by Microsoft.

You can also use these Service Tags in Azure Firewall and user-defined routes.

Many Service Tags are also divided into regions, so you can create a rule not just for `Storage` but specifically `Storage.WestUS`.

## Notable Service Tags

## VirtualNetwork

The `VirtualNetwork` service tag is quite special:

> The virtual network address space (all IP address ranges defined for the virtual network), all connected on-premises address spaces, peered virtual networks,
> virtual networks connected to a virtual network gateway, the virtual IP address of the host, and address prefixes used on user-defined routes. This tag might also contain default routes.

## Internet

The `Internet` service tag is broad and notably includes the Azure public IP ranges. It's "everything **but** the virtual network".

> The IP address space that's outside the virtual network and reachable by the public internet.
> The address range includes the Azure-owned public IP address space.

You could block outbound traffic to the `Internet` service tag, but it would also stop you from reaching Azure PaaS and SaaS services; you could then use the service tags for specific services to then allow access to them e.g. `Storage`, or a regional service tags e.g. `Storage.WestUS`.

## List of Service Tags

[List of Service Tags](https://docs.microsoft.com/azure/virtual-network/service-tags-overview)

If you wish to integrate Azure services into your own network infrastructure's security, you can use Azure scripts or APIs:

Az PowerShell:

```powershell
$serviceTags = Get-AzNetworkServiceTag -Location eastus2
$storage = $serviceTags.Values | Where-Object { $_.Name -eq "Storage" }
$storage.Properties.AddressPrefixes
```

`azcli`:

```bash
az network list-service-tags --location eastus2

az network list-service-tags --location australiaeast --query "[?name=='DataFactory']"
```

## See also

* [Service Tags API](https://docs.microsoft.com/rest/api/virtualnetwork/service-tags/list)
* [Downloaded JSON of all Azure Public Service Tags](https://www.microsoft.com/download/details.aspx?id=56519)
