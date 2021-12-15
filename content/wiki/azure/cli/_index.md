---
title: Azure CLI
description: Azure CLI tips, tricks & snippets
---

## App Service

## Viewing and updating properties and configuration

`az webapp show --ids <id>` to view a complete list of settings and properties, inclusive of most things.

`az webapp config show --ids <id>` will give you a subset, the `siteConfig` node from the previous command.
