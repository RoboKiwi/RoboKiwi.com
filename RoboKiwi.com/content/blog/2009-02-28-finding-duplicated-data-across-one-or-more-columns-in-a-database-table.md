---
author: David
categories:
- How To
- Software Development
date: 2009-02-28T10:59:09Z
excerpt: |2

  <![CDATA[]]>
guid: "20c7bf32-3861-442b-a8d9-3b62c16752b6"
id: 1421
tags:
- column
- columns
- duplicate
- duplicated
- duplicates
- mysql
- sql
- table
title: Finding duplicated data across one or more columns in a database table
url: /blog/2009/02/28/finding-duplicated-data-across-one-or-more-columns-in-a-database-table/
aliases: /2009/02/28/finding-duplicated-data-across-one-or-more-columns-in-a-database-table/
---

A few months ago I posted a little query about [finding duplicate rows in a database table]({{<relref "/blog/2008-10-17-finding-duplicate-rows-in-the-database">}} "Finding duplicate rows in a database table").

I'm revisiting this because I helped out Doogie with a similar query last night but with some complications.

Let's start with the original simple scenario of checking duplicates in a single column.

Some example data, a Users table:

| Id  | Email            |
|-----|------------------|
| `1` | `joe@bloggs.com` |
| `2` | `joe@bloggs.com` |
| `3` | `joe@bloggs.com` |
| `4` | `jane@doe.com`   |
| `5` | `jane@doe.com`   |
| `6` | `john@doe.com`   |

You can see that `joe@bloggs.com` and `jane@doe.com` have been duplicated. This could have been prevented by putting a unique index on the Email column.
  
So to find what emails have duplicates in our table:

```mysql
SELECT Email, COUNT(Email) AS Duplicates FROM `Users` GROUP BY Email HAVING ( Duplicates > 1 )
```

Results:

| Email            | Duplicates |
|------------------|------------|
| `jane@doe.com`   | 2          |
| `joe@bloggs.com` | 3          |

So, to help us manually correct our data, what are the Ids of the duplicates?

In MySQL (4.1+), we can use `GROUP_CONCAT` (after casting the numerical Id to a character string):

```mysql
SELECT Email, COUNT(Email) AS Duplicates, GROUP_CONCAT( CAST(Id AS CHAR) ) AS Culprits 
FROM `Users` GROUP BY Email HAVING ( Duplicates > 1 )
```

Our results:

| Email            | Duplicates | Culprits |
|------------------|------------|----------|
| `jane@doe.com`   | 2          | 4,5      |
| `joe@bloggs.com` | 3          | 1,2,3    |

That's quite handy, but what about just a list of the duplicates we can go through, instead of these rows of comma-separated Ids? This fugly query will do that for us: (I'm sure I could do this a better way but I'm tired and this works!)

```mysql
SELECT Id, Email FROM `Users` WHERE Email IN 
    (SELECT Email FROM `Users` GROUP BY Email HAVING ( COUNT(Email) > 1 ))
ORDER BY Email
```

| Id | Email            |
|----|------------------|
| 4  | `jane@doe.com`   |
| 5  | `jane@doe.com`   |
| 1  | `joe@bloggs.com` |
| 2  | `joe@bloggs.com` |
| 3  | `joe@bloggs.com` |

Now you can edit / delete the rows you want to get rid of if you ran the query in something like phpMyAdmin.

And don't forget, after the clean-up job, add that index to prevent duplicates re-appearing:

```mysql
ALTER TABLE `Users` ADD UNIQUE (`Email`)
```

Now, the new scenario. What about duplicates across multiple columns? For example, our Locations table:

| Id | CountryCode | AreaCode | Prefix |
|----|-------------|----------|--------|
| 1  | 64          | 9        | 489    |
| 2  | 64          | 9        | 489    |
| 3  | 64          | 9        | 489    |
| 4  | 64          | 3        | 942    |
| 5  | 64          | 3        | 942    |
| 6  | 64          | 9        | 536    |

Here, we want to find duplicates that have the same values in the 3 columns.

For example, you can see that 64-9-489 is duplicated three times, and 64-3-942 two times.

We can do this without much alteration to our original queries:

```mysql {hl_lines=[1,3]}
SELECT CountryCode, AreaCode, Prefix, COUNT(*)
AS Duplicates FROM `Locations` 
GROUP BY CountryCode, AreaCode, Prefix
HAVING ( Duplicates > 1 )
 ```

| CountryCode | AreaCode | Prefix | Duplicates |
|-------------|----------|--------|------------|
| 64          | 3        | 942    | 2          |
| 64          | 9        | 489    | 3          |

Then to get the Ids:

```mysql {hl_lines=[2]}
SELECT CountryCode, AreaCode, Prefix, COUNT(*) AS Duplicates, 
GROUP_CONCAT( CAST(Id AS CHAR) ) AS Culprits 
FROM `Locations` GROUP BY CountryCode, AreaCode, Prefix HAVING ( Duplicates > 1 )
```

| CountryCode | AreaCode | Prefix | Duplicates | Culprits |
|-------------|----------|--------|------------|----------|
| 64          | 3        | 942    | 2          | 4,5      |
| 64          | 9        | 489    | 3          | 1,2,3    |

I think you're getting the point.

Here's to get the rows for the culprits:

```mysql
SELECT Id, CountryCode, AreaCode, Prefix 
FROM `Locations` 
WHERE Id NOT IN 
  (SELECT Id FROM `Locations`
    GROUP BY CountryCode, AreaCode, Prefix 
    HAVING ( COUNT(CountryCode) = 1 ))
ORDER BY CountryCode, AreaCode, Prefix
```

| Id | CountryCode | AreaCode | Prefix |
|----|-------------|----------|--------|
| 4  | 64          | 3        | 942    |
| 5  | 64          | 3        | 942    |
| 1  | 64          | 9        | 489    |
| 2  | 64          | 9        | 489    |
| 3  | 64          | 9        | 489    |
  
Again I'm sure there'd be an easier way to do that, but hey, it works, and for something that should be a one-off.

So how to prevent the duplicated data in our second scenario? Add a composite unique key on those columns:

```mysql
ALTER TABLE `locations` ADD UNIQUE (`CountryCode`, `AreaCode`, `Prefix`)
```
