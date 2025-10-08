---
title: Data Access Layer strategies
---

## In a nutshell

* Use Unit of Work for coordinating queries and writes, ideally using an ORM such as Entity Framework or NHibernate
* Unit of Work and transactions should be explicit rather than implicit, for performance and expression of intent.
* Consider query objects over repository pattern
* Use LINQ and extension methods to create expressive, fluent queries
* Use ORM-specific features liberally; abstracting away the ORM is a lost cause.
