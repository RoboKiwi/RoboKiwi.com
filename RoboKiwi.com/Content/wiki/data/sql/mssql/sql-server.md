---
id: 523
title: SQL Server
date: 2012-01-27T09:51:31+00:00
author: David

guid: "bf689207-96bf-41dd-93c8-5a262d79f40e"
---

## Exporting all tables with BCP

## MSSQL on Docker

This will start a container called `mssql`, on the default port of `1433` while limiting it to 8GB RAM and setting a password for `sa`.

```bash
docker run --name mssql -m 8G -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<password>' -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

> Note you have to accept the EULA by passing `'ACCEPT_EULA=Y'` or the container won't start

> The container will also shutdown if the sa password you specify doesn't meet the minimum requirements: minimum 8 characters

## Viewing all SQL Error Codes & Messages

```sql
SELECT * FROM SYSMESSAGES WHERE msglangid='1033' ORDER BY error ASC
```

## How To

[Creating a machine account login for SQL Server](/blog/2012/01/27/creating-a-machine-account-login-for-sql-server/)

## Performance and Optimization

### Disable Customer Experience Improvement Program

Under Services, stop the SQLTELEMETRY service  (SQL Server CEIP service (MSSQLSERVER) service) and set it to Disabled.

## Snapshot Isolation

Enable Snapshot Isolation for the database:

```tsql
ALTER DATABASE [DBNAME] SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
```

Enable Read Committed Snapshot isolation level:

```tsql
ALTER DATABASE [DBNAME] SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE;
GO
```
