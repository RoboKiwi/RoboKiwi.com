---
title: Service Endpoints
---

## Overview

Service Endpoints are a way for you to restrict access to a PaaS or SaaS resource to specific subnets in your virtual network.

On the subnet, you can enable Service Endpoints for the particular services you're looking to restrict such as `Microsoft.Storage` or `Microsoft.Sql`.

## Example

You have an App Service application that uses Azure SQL, and you wish to restrict the SQL database so that only the App Service can reach it.

You have an App Service in a Virtual Network on subnet "web-tier".

In the subnet, you can expand the Service Endpoints and enable the `Microsoft.Sql` Service Endpoint. This works even if the subnet is delegated i.e. to `Microsoft.Web/serverFarms`.

Now, go the SQL Database Server you wish to protect

Alternatively, you can go straight to the resource you're wanting to protect, and assign it to the subnet; if the subnet doesn't have the required service endpoint enabled, it will do that for you dynamically (it will show `Service endpoint required` next to the subnet in the list).

## Advantages

The established service endpoint enables an efficient route to the service, directly from the private IP of the virtual network resource to the service.

## Disadvantages

You can protect a resource so that it can only be used by resources from a particular subnet, it doesn't protect from data exfiltration in that that same consumer could then just go and exfiltrate that data to a different unprotected resource. For that, you can use [Service Endpoint Policies]({{<relref "service-endpoint-policy">}}).

## Troubleshooting

You can use a Virtual Machine in your Virtual Network to look at configured rules by looking at the NIC > Effective Routes.

You'll see that the service endpoint is actually using the range of address for a service tag, and routing that to a next hop of VirtualNetworkServiceEndpoint.
