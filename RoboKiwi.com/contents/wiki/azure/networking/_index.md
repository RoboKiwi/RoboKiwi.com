---
title: Azure Networking
description: Explaining the networking and related security options in Azure, and how to use them.
guid: "1211f2eb-a5b0-4264-b5a9-081c4771e615"
---

## Overview

Azure networking resources are typically virtual network components and appliances.

NSGs, ASGs and similar traffic filtering are enforced at the Virtual Switch level on the host Virtual Machine, and don't involve edge devices or other hardware or network appliances external to the VM host.

When Azure resources are talking to each other, they come via their public IPs. Despite this, if an Azure resource is talking to another Azure resource via its public IP, because of the routing within the Azure networks, that traffic won't go out on the public internet.

## Security

To mutually restrict access between a VNet resource and a PaaS or SaaS resource:

ResourceA in SubnetA in VNetA interacts with StorageA.

* A Service Endpoint can be used to ensure StorageA can only be accessed by resources on SubnetA
* A Service Endpoint Policy can ensure ResourceA cannot access storage accounts other than StorageA.
