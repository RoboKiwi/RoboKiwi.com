---
title: Importing & Exporting Azure SQL databases
---

## Database copy

You can create a copy of a database using T-SQL:

```sql
CREATE DATABASE <dbname> AS COPY OF <server>.<sourceDatabase> (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S12')
```

While you can change the service objective during the copy, it doesn't seem possible to change the edition e.g.:

```sql
CREATE DATABASE <dbname> AS COPY OF <server>.<sourceDatabase> (EDITION = 'Premium', SERVICE_OBJECTIVE = 'P4')
```

Will give the error `The edition 'Standard' does not support the service objective 'P4'`.

Similarly, you can't set the maximum size of the database during the copy either.

To change the edition and/or max size, alter the database after the copy:

```sql
ALTER DATABASE [<dbname>] MODIFY (EDITION = 'Premium', SERVICE_OBJECTIVE = 'P4', MAXSIZE = 100 GB);
```

Single Database Provisioned Throughput (DTU) Editions: Free, Basic, Standard, Premium or DataWarehouse
Single Database Reserved (vCore) Editions: GeneralPurpose, Hyperscale, BusinessCritical

ElasticPool
Basic: Basic
Standard: S0, S1, S2, S3, S4, S6, S7, S9 or S12
Premium: P1, P2, P4, P6, P11 or P15
Azure Synapse Analytics: DW100 - DW30000c

<table>
<thead>
<tr><th colspan="7">Basic</tr>
<tr><th>Edition</th><th>Max DTUs</th><th>Storage</th><th>Max Storage</th><th>Max concurrent workers</th><th>Max concurrent sessions</th><th>Max in-memory OLTP storage</th></tr>
<tr><td>Basic</td><td>5</td><td>2</td><td>2</td><td></td><td>30</td><td>300</td></tr>
</table>

This also works between subscriptions, using the exact same SQL username and password (e.g. `sa` with the same password).

## Import / Export in Azure

Import and Export jobs use shared regional compute instances and have unpredictable and typically slow performance. I recommend you use SqlPackage if possible when importing and exporting database packages such as `.bacpac` and `.dacpac`.

## SqlPackage

### SqlPackage in GitHub Actions

SqlPackage already comes with the hosted GitHub Actions workers.

An example of manually installing SqlPackage on Ubuntu Linux:

```bash
if test -f "/opt/sqlpackage/sqlpackage"; then
    echo "::debug::SqlPackage already installed in the context"
else
    sudo apt-get install libunwind8
    wget -progress=bar:force -q -O sqlpackage.zip \
    https://aka.ms/sqlpackage-linux \
    && unzip -qq sqlpackage.zip -d /opt/sqlpackage \
    && chmod a+x /opt/sqlpackage/sqlpackage \
    && rm sqlpackage.zip
fi
```

## Further Reading



## References

[Azure/run-sqlpackage-action](https://github.com/Azure/run-sqlpackage-action)
