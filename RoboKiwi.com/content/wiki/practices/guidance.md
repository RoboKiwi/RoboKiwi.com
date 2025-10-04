---
title: Guidance
guid: "72a5e56c-6cc7-4159-aa9d-8c22cdd003a6"
---

# Libraries and Frameworks

Avoid introducing dependencies on third party libraries and frameworks.

They may move too slowly (lacking new features, falling behind technology, and failing to address security vulnerabilities or performance issues).

Sometimes they may move too fast.

They can also have issues with backwards compatibility. For example, you may want to take advantage of a new feature, or a security patch, but you can't upgrade easily because the new version introduces breaking changes.

It can also slow you down, where if you want to upgrade your base framework version, the library may not support it, meaning you have to replace the library to be able to move forwards.



* Static methods and extension methods are encouraged. They are easily testable, operating simply on inputs and outputs, not creating side effects and not relying on external state or conditions, in the functional paradigm.
  * Static methods "break" when you use static state or service location inside your static methods.

* State is the enemy.
  * Global and static state are a no-no.
  * Use dependency-injected classes that can be lifestyle managed by the container (i.e. scoped or singleton) to handle shared state.

* Creating an initial class, don't worry about extracting an interface until necessary, despite the Design by Contract principle.

* Only create new project / assembly if necessary; try to keep as few as possible.

* Hand-coded SQL isn't necessarily a bad thing.

* Unit test what's important

* Simple doesn't mean easy, and complex doesn't mean difficult.

# CSharp

## stackalloc

```csharp
Span<byte> buffer = userInput > MaxStackSize ? (rentedFromPool = ArrayPool<byte>.Shared.Rent(userInput)) : stackalloc byte[MaxStackSize];
Span<byte> buffer = userInput > MaxStackSize ? (rentedFromPool = ArrayPool<byte>.Shared.Rent(userInput)) : stackalloc byte[MaxStackSize];
```

- DO:
  - Use a constant for the allocation length
  - Be conservative with use; keep well under 1KB if you can, and use pattern to alloc on heap or use ArrayPool if size is too high.
  - Initialize if necessary
  - Allocate outside of loops
  
- DON'T:
  - Use a dynamic allocation length, as compiler can't check, and overflow will cause process to exit.
  - Use large stackalloc as you risk StackOverflowException.
  - Presume memory is initialized
  - Allocate within a loop

## References

- [Dos and Don'ts of stackalloc](https://vcsjones.dev/stackalloc/)