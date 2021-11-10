---
title: Private Link
---

## Overview

Private Link assigns a private IP to within your network, which is accessible across your vnet, peered vnets and connected networks (Site-to-site VPN, ExpressRoute) across subnets.

## Troubleshooting

If you look at a Virtual Machine within your vnet, and look at Effective Routes, you should see the private endpoints created in there.

They will show Next Hop Type of InterfaceEndpoint, with the private IP as the address prefix e.g. 10.10.1.4/32.

## Advantages

* Helps protect against data exfiltration

## Disadvantages

* Requires DNS integration. This is simple when using Azure only, but when integrating to on-premises it can be complex.