---
title: Dynamic DNS for Azure DNS
description: How to update Azure DNS with a dynamically changing IP
---

## Overview

* Create a Service Principal for authenticating to Azure using a certificate
* Install the certificate on the client machine
* Set up a script to run regularly to update Azure DNS with the client IP

## Create Service Principal

```bash
az ad sp create-for-rbac --keyvault MyVault --cert DynamicDns --create-cert
```