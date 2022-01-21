---
id: 523
title: SQL Server
date: 2012-01-27T09:51:31+00:00
author: David

---

## Exporting all tables with BCP

## MSSQL on Docker

```bash
docker pull mcr.microsoft.com/mssql/server
docker run --name mssql -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<password>' -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

## Viewing all SQL Error Codes & Messages

```sql
SELECT * FROM SYSMESSAGES WHERE msglangid='1033' ORDER BY error ASC
```

## How To

[Creating a machine account login for SQL Server](/blog/2012/01/27/creating-a-machine-account-login-for-sql-server/)

## Performance and Optimization

### Disable Customer Experience Improvement Program

Under Services, stop the SQLTELEMETRY service  (SQL Server CEIP service (MSSQLSERVER) service) and set it to Disabled.

