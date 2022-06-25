---
title: Guidance
guid: "72a5e56c-6cc7-4159-aa9d-8c22cdd003a6"
---

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
