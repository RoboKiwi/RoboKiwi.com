---
title: Azure Security Cheatsheet
---

## Azure SQL

## Firewall

- Database Firewall Rules are checked first, then falls back to Server Firewall Rules.
- Up to 256 IP firewall rules per server, and per Database
- "Allow Azure Services and resources to access this server" adds firewall IP rule `0.0.0.0` which allows all Azure resources access, even from outside the database's subscription.
- Database-level IP firewall rules can only be created and managed by using Transact-SQL.
- To configure firewall rules from the Portal or PowerShell, you must be the subscription owner or a subscription contributor.
- To configure the firewall IP rules using Transact-SQL, you must connect to the `master` database as the server-level principal login or as the Azure Active Directory administrator.

## Best Practices

- Configure firewall IP rules per database (blast radius)
- Set firewall IP rules on server for administrators, or when the databases have the same access requirements

## References

[Azure SQL Database and Azure Synapse IP firewall rules](https://docs.microsoft.com/azure/azure-sql/database/firewall-configure)
