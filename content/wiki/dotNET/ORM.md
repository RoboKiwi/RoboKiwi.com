---
title: "ORM"
toc: true
description: "Guidance on the Object Relational Model (ORM) in .NET"
draft: true
menu:
    wiki:
        name: "ORM"
        parent: "dotnet"
        weight: 30
---

## Generic Implementation

## Definitions

ActiveRecord pattern: https://en.wikipedia.org/wiki/Active_record_pattern

### Unit of Work

https://ayende.com/blog/4117/nhibernate-unit-of-work-multiple-reuqests

From Martin Fowler's definition: https://martinfowler.com/eaaCatalog/unitOfWork.html

>When you're pulling data in and out of a database, it's important to keep track of what you've changed; otherwise, that data won't be written back into the database. Similarly you have to insert new objects you create and remove any objects you delete.  
You can change the database with each change to your object model, but this can lead to lots of very small database calls, which ends up being very slow. Furthermore it requires you to have a transaction open for the whole interaction, which is impractical if you have a business transaction that spans multiple requests. The situation is even worse if you need to keep track of the objects you've read so you can avoid inconsistent reads.  
A Unit of Work keeps track of everything you do during a business transaction that can affect the database. When you're done, it figures out everything that needs to be done to alter the database as a result of your work.

## Advantages

* The ORM pattern can save you from the complexity of managing CRuD operations and the relationships between your entities

## Unit of Work and Repository

Repository: Queries & CRUD operations on a single type of entity.

Unit of Work: A collection of operations, usually across repositories. This is not necessarily an atomic operation.

Repository strategies:

Extension methods

How things relate to CQRS

Ambient transactions / unit of work.

Explicit / declarative over implicit

Abstracting the ORM

ORMs are very opinionated libraries, so it's a lost cause to try to abstract them away. Instead, it's best to embrace your ORM of choice so you can use all of its features and best practices.

This becomes very apparent when you start having to do more complex queries that involve fetching strategies, joins, or handling things such as evicting or loading in entities to and from the caches.

| Pattern | NHibernate | Entity Framework |
|---------|------------|------------------|
| Repository | ISession.Query | DbSet |
|--------------|-----------|-------------|
| Unit of Work | ISession | DbContext |

## Recommendations

* The ORM can't save you from bad underlying database schemas
* A UnitOfWork class should be disposable, and use a configured UnitOfWorkFactory for implementation details.
* Favour extension methods on IUnitOfWork, IRepository and IQueryable for your queries, rather than cluttering your interfaces and implementations, which can make your code harder to mock or test.
* Repositories should get the unit of work injected. This may seem circular and counter-intuitive but is a necessity for things like joins, fetching etc. This will make your code less cluttered, as you won't find yourself polluted with mass dependency injection of repositories.
* Services should have the unit of work injected, not the repositories.
* Use bounded unit of works if you really wish to enforce boundaries.
* Allow your ORM to "bubble" up and appear in your unit of work and repositories. ORMs are very opinionated, and trying to abstract this away is a false economy.
* Always map foreign key properties, so when setting a relationship, you can simply set the key value without having to create a full child object

 Unit of Work, Session Per Request:

* Avoid opening a transaction at the start of the session or unit of work. During the request, you can do CPU-intensive, IO intensive or network-based work during the request. Any data that is manipulated during the request will acquire locks; as your application gets busier, this pattern will degrade performance rapidly.
* The transaction can be opened and committed at write time: e.g. the saving of changes; this is to ensure consistent state (i.e. no dangling relationships).
* To protect against stale data, encourage using optimistic concurrency in combination with transactional commit (rather than wrapping everything in the transaction)

 Nesting / inheriting sessions?

e.g.

```csharp

using(var uow = new UnitOfWork())
{
    var user = uow.Users.GetByUsername("Joe");
    var group = uow.Groups.Get("Administrators");
    user.AddToGroup(group);

    // Make asynchronous call to an API
    await restfulHttpClient.DoSomething();

    using(var transaction = uow.BeginTransaction(TransactionIsolation.ReadCommitted))
    {
        // When we save changes, if the user has been changed out from under us, we could
        // get a StaleObjectException if we chose to use optimistic concurrency.
        uow.SaveChanges();

        // If the UoW saves changes successfully, we can now commit them with the transaction. If there
        // were any issues with saving, including any stale data, then our transaction will be rolled back.
        transaction.Commit();
    }
}

```
