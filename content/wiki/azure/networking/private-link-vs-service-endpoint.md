---
title: Comparing Private Link and Service Endpoints
guid: "b78b29c8-9675-49c9-9208-bfe9cb8cbdfc"
---

## Overview

Private Link assigns a private IP to a particular instance of a service and makes it available to the private network, while a Service Endpoint is giving a direct path to that whole service type.

If you had a web application that wanted to talk to two different storage accounts, you'd need to set up each storage account with a private endpoint and two separate private IPs; meanwhile
a service endpoint could cover all storage accounts (a wide range of IPs described by a service tag).

|Description  | Private Link  | Service Endpoint |  |
|---------|---------|---------|---------|
| Peered vnets | Y  |  N |         |
| Connected networks (S2S, ExpressRoute) | Y  |  N |         |
| vnet     | Y | N |         |
| subnet     | Y | Y |         |
|      |         |         |         |

Because the Private Link opens up your resource to connected networks, that is actually far less restrictive than the service endpoint (which can be a positive or a negative).

Private Link doesn't obey NSG rules on the subnet.
