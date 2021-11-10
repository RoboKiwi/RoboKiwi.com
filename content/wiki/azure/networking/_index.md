---
title: Azure Networking
---

## Overview

Azure networking resources are typically virtual network components and appliances.

NSGs, ASGs and similar traffic filtering at enforced at the virtual switch level on the virtual machine and don't involve edge devices or other hardware or network appliances external to the VM host.

When Azure resources are talking to each other, they come via their public IPs. Despite this, if an Azure resource is talking to another Azure resource, via its public ip, because of the routing within the Azure networks, that traffic won't go out on the public internet.

## Security

To mutually restrict access between a vnet resource and a PaaS or SaaS resource:

ResourceA in SubnetA in VnetA interacts with StorageA.

A Service Endpoint can be used to ensure StorageA can only be access by resource on SubnetA

A Service Endpoint policy can ensure ResourceA cannot access storage accounts other than StorageA.
