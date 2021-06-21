---
title: Guidance
---

Static methods and extension methods are encouraged
State is the enemy. Global and static state are a no-no. Use dependency-injected classes that can be lifestyle managed by the container (i.e. scoped or singleton) to handle shared state.