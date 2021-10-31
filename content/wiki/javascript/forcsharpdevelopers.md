---
title: JavaScript for C# Developers
---

## Objects

A Javascript object has a lot in common with a C# dictionary.

You could imagine it as a dynamic type in C# that looks up values in a dictionary.

Methods are object properties that reference a function.

`this` is the *calling context*, **not** the current object instance.

## Functions

* Can return a reference to themselves.
* There is an `arguments` implicit variable.
* All parameters are optional.
* Unspecified parameters are set to `undefined`

## Mixins

## Null coalescing

C#:

```csharp
    var value = GetPossiblyNullValue() ?? "Default value";
```

JavaScript:

```javascript
var value = getPossiblyNullValue() || "Default value";
```

## Globals

Anything not defined within a function or using the var keyword, will be set on the global `window` object.

## Arrays

Arrays are more like `Dictionary<int, object>` where the index is an auto-incrementing value.

## Classes

No `class` in javascript, but there is a `new` keyword.

Prototypical inheritance etc.

[Polyfills for Object.create and inheritance]({{<relref "/wiki/javascript/polyfills">}})

## Closures

## Polymorphism

Functions cannot be overloaded in Javascript

## Equality & Truthiness

In C#, you have `null` as your default value for empty references.

In JavaScript, you have `null`, and also `undefined`.

In C#, conditional tests must evaluate to a `bool` value or they won't even compile.

e.g. this is illegal in C#:

```csharp
var value = "Non-null string value";
if (value) Console.WriteLine("Say something");
```

## Boolean Logic

### !! (Bang bang) operator

`!!` coerces a truthy value to a boolean.

This is not actually an operator, but simply doubling up on the not operator.

1. The first ``!`` converts the value to a boolean value, at the same time inverting the truthy value.
2. The second ``!`` then inverts the boolean value, so that you get the boolean representing the initial truthy value.

You can see this technique used in other languages that have truthiness, e.g. C.

Examples:

```javascript
    !!false === false
    !!true === true

    !!0 === false
    !!parseInt("foo") === false // NaN is falsey
    !!1 === true
    !!-1 === true  // -1 is truthy

    !!"" === false // empty string is falsey
    !!"foo" === true  // non-empty string is truthy
    !!"false" === true  // ...even if it contains a falsey value

    !!window.foo === false // undefined is falsey
    !!null === false // null is falsey

    !!{} === true  // an (empty) object is truthy
    !![] === true  // an (empty) array is truthy
```

### Equals

In JavaScript, `==` is a soft comparison. You can use `===` for hard comparison, which strictly matches type and value, and is more equivalent to `.Equals` or `==` in C#.

In JavaScript, any value that is `null`, `0`, empty or `undefined` is considered `false`.

```javascript
"" == false;
0 == false;
null == false;
```

Objects are only equal to themselves.

```javascript
"yes" == "yes";
"yes" === "yes";
1 == -1;
1 === -1;
```

## Multi-line strings

C#:

```csharp
var value = @"First line
Second line
Third line";
```

JavaScript uses backticks:

```javascript
var value = `First line
Second line
Third line`;
```
## Anonymous functions

## Lambdas

C#

```csharp
var expression = () => "Test";
```

Javascript

```javascript
var expression = () => "Test";
```

## Promises & asynchronous programming

Javascript is single-threaded, meaning blocking and long-running calls can make websites unusable.

> Node.js is also single-threaded, but can scale massively by using non-blocking, asynchronous programming.

The traditional JavaScript asynchronous programming model involves specifying callback functions to blocking or long-running code.

Promises seek to standardize this pattern, and provide native support for promises in modern browsers.

It allows easily chaining asynchronous calls in a readable, consistent manner, while reducing nesting and simplifying error handling.

Asynchronous programming came to .NET as a first class citizen with async and await on top of the existing Task libraries.

Promises can be changed; if you return a value, that gets passed to the next then. If you return a "thenable", then it will get resolved before the return value getting passed to the next then.

## Development tips and tricks

### Debugging

You can put the ``debugger;`` statement anywhere to force a breakpoint in development tools.

In all modern browsers, you can hit `F12` to open the developer tools
