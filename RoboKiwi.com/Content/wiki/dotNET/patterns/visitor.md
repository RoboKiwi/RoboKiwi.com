---
title: "Visitor Pattern"
toc: true
description: "Explanation of the Visitor pattern, with implementations in C#"
menu:
    wiki:
        name: "Visitor Pattern"
        parent: "net"
        weight: 30
guid: "1e951dcf-45fe-4088-b8f9-a11a68fc98a0"
---

# Generic Implementation

When implementing the Visitor pattern, in some examples you may have to define overrides for all supported visitable types in the interface, and then have implementations.

If visiting each node simply involves invoking Accept on the node, passing in the visitor implementation, then that's a lot of unnecessary work.

You can use a generic implementation, so that you don't need to update the interface and implementation(s) of the visitor each time you want to support a new node:

```csharp
interface IVisitor {

    T Visit<T>(T node) where T : INode;
}

interface INode {
    void Accept(IVisitor visitor);
}
```
