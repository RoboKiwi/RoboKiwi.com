---
title: Azure CLI
description: Azure CLI tips, tricks & snippets
guid: "45c455e5-4eb0-47ef-a955-2abf6b0961a3"
---

## App Service

## Viewing and updating properties and configuration

`az webapp show --ids <id>` to view a complete list of settings and properties, inclusive of most things.

`az webapp config show --ids <id>` will give you a subset, the `siteConfig` node from the previous command.
