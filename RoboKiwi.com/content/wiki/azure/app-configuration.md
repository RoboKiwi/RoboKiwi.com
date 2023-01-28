---
title: App Configuration
guid: "957595f8-36c1-48d0-b7f9-c557d66d32a8"
---

## Overview

App Configuration allows centralized management of application settings, with integration to Azure Key Vault, Managed System Identity, App Service and Functions.

## Command-line operations

You can use the cli to do bulk operations, e.g. delete all keys by label:

```bash
az appconfig kv delete --key "*" --label "*" --subscription "<guid or name>" --name "app-config-resource-name"
```
