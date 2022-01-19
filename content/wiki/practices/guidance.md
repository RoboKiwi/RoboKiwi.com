---
title: Guidance
---

* Static methods and extension methods are encouraged. They are easily testable, operating simply on inputs and outputs, not creating side effects and not relying on external state or conditions, in the functional paradigm.
  * Static methods "break" when you use static state or service location inside your static methods.

* State is the enemy.
  * Global and static state are a no-no.
  * Use dependency-injected classes that can be lifestyle managed by the container (i.e. scoped or singleton) to handle shared state.
