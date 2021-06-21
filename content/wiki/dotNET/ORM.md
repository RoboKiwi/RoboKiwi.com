---
title: "ORM"
toc: true
description: "Guidance on the Object Relational Model (ORM) in .NET"
menu:
    wiki:
        name: "ORM"
        parent: "net"
        weight: 30
---

# Generic Implementation


# Unit of Work and Repository

Repository: Queries & CRUD operations on a single type of entity.

Unit of Work: A collection of operations, usually across repositories. This is not necessarily an atomic operation.

Repository strategies:

Extension methods

How things relate to CQRS

Ambient transactions / unit of work.

Explicit / declarative over implicit

Abstracting the ORM

| Pattern | NHibernate | Entity Framework |
|---------|------------|------------------|
| Repository | ISession.Query | DbSet |
|--------------|-----------|-------------|
| Unit of Work | ISession | DbContext |