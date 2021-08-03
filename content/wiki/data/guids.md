---
title: The case for unique identifiers (UUID / GUID)
description: Why using a globally / universally unique identifier can make your applications more scalable and maintainable
---

## Preamble

You can use GUID / UUID interchangeably, but I will refer to it as GUID from here on out.

## The case for GUIDs

Once your entities have GUIDs, it makes it easier to identify and find any entity.

It makes things like merging data, such as when combining data sources (e.g. two sites are being consolidated), or refactoring your data layer.

For example, you might have a design where you have a tables for Users and Admins.

You'd like to normalize logins, and grant privileges based on roles or claims. You could merge all the Admins users across to the Users table, without any id collisions due to the GUIDs.

In the same way, if you had an identical application that was installed for two different customers, and they were to be merged, that would be possible.

## Pros

* Global uniqueness can facilitate merging or migration scenarios that would be prohibitively difficult otherwise
* Ids, URLs not guessable as it's not incremental. Security by obscurity is weak, but this also means things like it's harder for competitors to guess your number of users or customers or sales orders, or crawl that data.
* Reduction in chattiness to your data layer; easily build and persist full object graphs without constant data layer calls and higher contention. This lines up well with CQRS, and also makes your code more readable and easier to maintain.

## Cons

* Identifiers are less readable. There is an element of misperception here, as you can usually identify a GUID with the first few characters.

## Performance

* Because GUIDs are not sequential, this can create performance issues in databases if your GUID is used as the clustering index.
  * The solution here is to use an auto-incrementing identity as the clustering index

## Readable identifiers

* For identifiers that are customer facing, such as order or invoice numbers, create a surrogate key that is shorter and more human-readable
* You can potentially compress or crunch the GUID if you wish to make shorter URLs