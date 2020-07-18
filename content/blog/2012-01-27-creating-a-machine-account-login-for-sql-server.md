---
author: David
categories:
- Entertainment
date: 2012-01-27T09:49:25Z
guid: https://www.davidmoore.info/2012/01/27/creating-a-machine-account-login-for-sql-server/
id: 520
title: Creating a machine account login for SQL Server
url: /blog/2012/01/27/creating-a-machine-account-login-for-sql-server/
aliases: /2012/01/27/creating-a-machine-account-login-for-sql-server/
---

You can create a machine account login for SQL Server with the following command:

```tsql
CREATE USER [MACHINENAME] FOR LOGIN [DOMAIN\MACHINENAME$] WITH DEFAULT_SCHEMA=[dbo]
```