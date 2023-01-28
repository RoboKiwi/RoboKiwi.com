---
title: Azure SQL tiers & pricing
---

## Available Editions

You can list the available editions for a location:

```bash
az sql db list-editions -a -o table -l AustraliaEast
```

Editions: Basic, Standard, Premium or Data Warehouse.

Service Objective:

ElasticPool
Basic: Basic
Standard: S0, S1, S2, S3, S4, S6, S7, S9 or S12
Premium: P1, P2, P4, P6, P11 or P15
Azure Synapse Analytics: DW100 - DW30000c

## Single Databases

Reference: [Resource limits for single databases using the DTU purchasing model - Azure SQL Database](https://docs.microsoft.com/azure/azure-sql/database/resource-limits-dtu-single-databases)

<table>
<thead>
<tr><th colspan="7">Basic</tr>
<tr><th>Edition</th><th>Max DTUs</th><th>Storage</th><th>Max Storage</th><th>Max concurrent workers</th><th>Max concurrent sessions</th><th>Max in-memory OLTP storage</th></tr>
<tr><td>Basic</td><td>5</td><td>2</td><td>2</td><td></td><td>30</td><td>300</td></tr>
<tr><th colspan="7">Standard</tr>
<tr><th>Edition</th><th>Max DTUs</th><th>Storage</th><th>Max Storage</th><th>Max concurrent workers</th><th>Max concurrent sessions</th><th>Max in-memory OLTP storage</th></tr>
<tr><td>S0</td><td>10</td><td>250</td><td>250</td><td></td><td>60</td><td>600</td></tr>
<tr><td>S1</td><td>20</td><td>250</td><td>250</td><td></td><td>90</td><td>900</td></tr>
<tr><td>S2</td><td>50</td><td>250</td><td>250</td><td></td><td>120</td><td>1200</td></tr>
<tr><td>S3</td><td>100</td><td>250</td><td>1024</td><td></td><td>200</td><td>2400</td></tr>
<tr><td>S4</td><td>200</td><td>250</td><td>1024</td><td></td><td>400</td><td>4800</td></tr>
<tr><td>S6</td><td>400</td><td>250</td><td>1024</td><td></td><td>800</td><td>9600</td></tr>
<tr><td>S7</td><td>800</td><td>250</td><td>1024</td><td></td><td>1600</td><td>19200</td></tr>
<tr><td>S9</td><td>1600</td><td>250</td><td>1024</td><td></td><td>3200</td><td>30000</td></tr>
<tr><td>S12</td><td>3000</td><td>250</td><td>1024</td><td></td><td>6000</td><td>30000</td></tr>
<tr><th colspan="7">Premium</tr>
<tr><th>Edition</th><th>Max DTUs</th><th>Storage</th><th>Max Storage</th><th>Max concurrent workers</th><th>Max concurrent sessions</th><th>Max in-memory OLTP storage</th></tr>
<tr><td>P1</td><td>125</td><td>500</td><td>1024</td><td>1</td><td>200</td><td>30000</td></tr>
<tr><td>P2</td><td>250</td><td>500</td><td>1024</td><td>2</td><td>400</td><td>30000</td></tr>
<tr><td>P4</td><td>500</td><td>500</td><td>1024</td><td>4</td><td>800</td><td>30000</td></tr>
<tr><td>P6</td><td>1000</td><td>500</td><td>1024</td><td>8</td><td>1600</td><td>30000</td></tr>
<tr><td>P11</td><td>1750</td><td>4096</td><td>4096</td><td>14</td><td>2800</td><td>30000</td></tr>
<tr><td>P15</td><td>4000</td><td>4096</td><td>4096</td><td>32</td><td>6400</td><td>30000</td></tr>
</table>
