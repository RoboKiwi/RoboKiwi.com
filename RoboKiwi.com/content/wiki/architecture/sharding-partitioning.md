---
title: Sharding & Partitioning
---

## Overview

To improve performance and scalability, data can be broken up into smaller chunks called partitions.

Breaking data up into partitions can enable scaling out by moving separate partitions out into separate disks or completely different servers,
allowing each partition to make use of its own resources as demand goes up.

One definition of sharding might be seen as partitions that exist on physically different servers.

In my view, I see sharding as an approach that is known at the application layer and dealt with directly by the application.

For example, you might shard your customer data into separate databases in a multi-tenant scenario. Your biggest, most resource-intensive customer might have their own dedicated database.
Meanwhile, several smaller customers may be able to share the resources of a single database. When a user logged into your application, the application would then determine which database
to connect to.

In partitioning, I see this is an approach that is usually implemented at a platform or systems layer, where while your application is aware of how partitions are defined, how those partitions
are managed, and whether they sit on separate files, disks, hosts or networks is abstracted away and handled by the system; especially in a distributed system that can dynamically scale in or
out.

An example of this might be if one partition begins to grow beyond the bounds of a single file or database shared with other partitioned data; the system may then automatically break the
large partition out into its own file or database to make use of the extra resources.
