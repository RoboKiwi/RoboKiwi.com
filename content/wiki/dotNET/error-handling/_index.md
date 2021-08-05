---
title: Error Handling in .NET
---

## Exponential back-off retry

When you encounter a transient exception, and wish to retry, you can implement exponential back-off so that you wait for an increasingly longer interval before retrying, up to a maximum number of retries.

This can help give the service time to resolve or recover what is causing the transient errors, while allowing the consumer to naturally resume once the service recovers.

```csharp
var consecutiveTransientErrors = 0;

while (!success)
{
    try
    {
        var success = await DoTheThingAsync();
        consecutiveTransientErrors = 0;
    }
    catch (Exception ex)
    {
        // Don't retry for non-transient errors
        if( !IsTransientError(ex) ) throw;

        consecutiveTransientErrors++;

        // Retry after 2 seconds, then 4, 8, 16, up to 32 seconds max.
        await Task.Delay( TimeSpan.FromSeconds(Math.Pow(2, Math.Min(consecutiveTransientErrors, 5))));
    }
}
```