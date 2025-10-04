---
title: S.O.L.I.D. (SOLID) Principles
menu:
    wiki:
        parent: principles
description: Description of the 5 principles and practices that make up the SOLID acronym
guid: "d7c62cd6-2b07-434c-bcb2-1be5e420dba0"
---

# Introduction

S.O.L.I.D. aka SOLID stands for:

# S: Single Responsibility Principle aka SRP

A class should have a single responsibility.

# O: Open / Closed

A class should be open for extension, but closed to modification.

The Open Closed Principle (OCP) was originated by Bertrand Meyer. Robert C. Martin declared this to be the most important principle.

# L: Liskov substitution principle

An object should be replaceable with an object of its subtype without breaking the program.

# I: Interface Segregation Principle (ISP)

Many interfaces are desirable over one general-purpose interface

# D: Dependency Inversion Principle aka (DIP)

Classes should depend on supplied abstractions, instead of concrete implementations.

This is usually extrapolated into these abstractions being supplied by a container, so that consumer objects are
not responsible for instantiating and configuring its dependencies.

## History

The principles were documented in the paper Design Principles and Design Patterns by Robert C Martin ("Uncle Bob") in 2000.

Michael Feathers is credited with later coining the SOLID acronym around 2004.
