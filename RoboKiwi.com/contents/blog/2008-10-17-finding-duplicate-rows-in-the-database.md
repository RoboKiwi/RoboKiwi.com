---
author: David
categories:
- Software Development
date: 2008-10-17T11:39:40Z
guid: "9863f979-1f87-446a-837d-9f8010d5d7b1"
id: 32
tags:
- database
- duplicates
- sql
title: Finding duplicate rows in the database
url: /blog/2008/10/17/finding-duplicate-rows-in-the-database/
aliases: /2008/10/17/finding-duplicate-rows-in-the-database/
---

{{<warning>}}This post has been re-visited in [Finding duplicated data across one or more columns in a database table](/2009/02/28/finding-duplicated-data-across-one-or-more-columns-in-a-database-table/){{</warning>}}

Information gleaned from Microsoft KB article: [How to remove duplicate rows from a table in SQL Server](https://support.microsoft.com/default.aspx?scid=kb;en-us;139444 "How to remove duplicate rows from a table in SQL Server")

When you allow duplicated rows in a database that aren't expected and shouldn't be allowed, it's a flag saying that you need a unique index on the table to prevent duplicate rows to be added in the first place.

The first step to fixing the problem is to find and fix the duplicated rows.

The second step is to add an index once the existing duplicate rows have been dealt with, so that the problem won't occur in the future.

If you have a `Users` table, which has an `Email` column, you will likely want that `Email` column to have a unique index. You can find the duplicated emails (and the number of times each email occurs) using this query:

```mysql
SELECT Email, COUNT(Email) AS NumberOfDuplicates FROM `Users` GROUP BY Email HAVING ( COUNT(Email) > 1 )
```

Which may return something like this:

| Email                      | NumberOfDuplicates |
|----------------------------|--------------------|
| `joe@bloggs.com`           | 3                  |
| `spammer@spamilicious.com` | 13                 |
| etc                        |                    |

Now you can go about carefully resolving the duplicates, then adding the constraints to that column to prevent duplicates occurring again.
