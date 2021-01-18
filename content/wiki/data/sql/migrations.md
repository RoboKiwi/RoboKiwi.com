---
title: Migrations
---

## Exporting all tables with BCP

Migrations, like a changescript, should be able to be run repeatedly. This means defensive coding; checking it columns or tables exist before doing operations. Or, if necessary, dropping and recreating.

In many cases, migration order doesn't matter, at least in the short term. But when the order matters, then the ordering should be explicit and controlled by the user.

Use list / ordering instead of numbering, putting the burden on users to handle numbers, and having to deal with collisions and difficult merges because of this.

Hashing of migrations can be automatic, and allow migrations to be re-run automatically if the hash has changed.

> Can use [C# Code Generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to hash code files in .NET Core

Some migrations could be marked as not repeatable, but all migrations should be assumed repeatable.

Migrations won't be re-run if they've already been run against the database, and the hash hasn't changed.

You can force re-running of migrations.



# Migration libraries and approaches

## .NET

* [FluentMigrator @ GitHub](https://github.com/fluentmigrator/fluentmigrator)
* [Entity Framework Core Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations)

## Java

* [Flyway by RedGate](https://flywaydb.org/)