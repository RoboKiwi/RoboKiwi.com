---
title: Gotchas
description: "Do's and don'ts when working with async"
toc: true
---

## Async / Await Gotchas

Spot the bug:

```csharp
    internal Task<DataFormat> TestAsync()
    {
        using (var stream = File.Open("test.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            return StreamProcessingUtil.DoSomethingAsync(stream);
        }
    }
```

Oops! We actually need to await the method that gets passed the stream, as without an await, the closing of the using will be hit, disposing the stream before our method has a chance to start or finish what it wants to do.

Solution:

```csharp
    internal async Task<DataFormat> TestAsync()
    {
        using (var stream = File.Open("test.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            return await StreamProcessingUtil.DoSomethingAsync(stream);
        }
    }
```

## ConfigureAwait

`ConfigureAwait` is used to manage the `SynchronizationContext` in async / await.

### Recommendations

Use `ConfigureAwait(false)` in non-UI code (i.e. libraries, general purpose code). Ugly, yes, but this will help mitigate threading and deadlocking issues, while also improving performance.

### ReSharper Support

ReSharper can help you with `ConfigureAwait` analysis by adding to your [.editorconfig](/wiki/practices/editorconfig/):

```ini
configure_await_analysis_mode = <library|ui>
```

## Further Reading

* [ConfigureAwait FAQ @ MSDN](https://devblogs.microsoft.com/dotnet/configureawait-faq/)