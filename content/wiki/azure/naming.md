---
title: Naming restrictions and conventions
---

## Overview

Naming of resources across Azure can be inconsistent due to various limitations.

You will normally encounter restrictions to resources that use their name in DNS.

## Notable naming restrictions

| Service            | DNS Hostname | Char. limit | Scope                          | Restrictions                         | Additional restrictions    |
|--------------------|--------------|-------------|--------------------------------|--------------------------------------|----------------------------|
| Azure SQL Server   | Y            | 63          | global                         | Lower-case letters, numbers, hyphens | No leading or trailing `-` |
| Azure SQL Database / Elastic Pool | Y            | 63          | server                         | No control chars or `<>*%&:\/?`      | No trailing ' ' or '.'     |
| Storage Account | Y            | 24          | global  | Lower-case letters and numbers                |                            |
| Row3               |              |             |                                |                                      |                            |
| Row4               |              |             |                                |                                      |                            |

## Storage restrictions

| Resource          | DNS Hostname | Char. limit | Restrictions                        | Additional restrictions    |
|------------------|--------------|-------------|-------------------------------------|----------------------------|
| Blob | N           | 255          | Letters, numbers, hyphens, underscore | No consecutive `-`. Folder names 3 to 63 characters. |
| Storage | Y | 24 | Lower-case letters and numbers |                            |
| Row3             |              |             |                                     |                            |
| Row4             |              |             |                                     |                            |

## References & Further Reading

* [Naming rules and restrictions for Azure resources](https://docs.microsoft.com/azure/azure-resource-manager/management/resource-name-rules)
