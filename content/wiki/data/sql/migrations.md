---
title: Migrations
---

## Exporting all tables with BCP

Migrations, like a changescript, should be able to be run repeatedly. This means defensive coding; checking if columns or tables exist before doing operations. Or, if necessary, dropping and recreating.

In many cases, migration order doesn't matter, at least in the short term. But when the order matters, then the ordering should be explicit and controlled by the user.

Use list / ordering instead of numbering, which puts the burden on users to handle numbers, and having to deal with collisions and difficult merges because of this.

Hashing of migrations can be automatic, and allow migrations to be re-run automatically if the hash has changed.

> Can use [C# Code Generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to hash code files in .NET Core

Some migrations could be marked as not repeatable, but all migrations should be assumed repeatable.

Migrations won't be re-run if they've already been run against the database, and the hash hasn't changed.

You should be able to force re-running of migrations.

Concept of environments? Test, Prod etc? So migration or chunk of migration can run (or not run) in a particular environment.

Allow the app to start even if the migrations fail

Automatically expose the API and web interface (similar to the Swashbuckle Swagger endpoints)

# Migration libraries and approaches

## .NET

* [FluentMigrator](https://github.com/fluentmigrator/fluentmigrator)
* [Entity Framework Core Migrations](https://docs.microsoft.com/ef/core/managing-schemas/migrations)

## Java

* [Flyway by RedGate](https://flywaydb.org/)