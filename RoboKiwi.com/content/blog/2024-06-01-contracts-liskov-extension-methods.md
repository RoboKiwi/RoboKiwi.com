---
title: Simpler contracts and rich extension obeyeing the Liskov Substitution Principle
author: John Doe
date: 2024-06-01
tags:
    - Markdown
    - Frontmatter
---

# Open-Closed Principle

The Open-Closed Principle states that a class should be open for extension but closed for modification.

This allows you to add new functionality without changing the existing code; usually meaning you then aren't introducing breaking changes.

# Example: Logging

Defining a contract for a logger might look like so:

```csharp
public interface ILogger
{
    void Information(string message);
    void Warning(string message);
    void Debug(string message);
    void Error(string message);
    void Error(Exception ex, string message);
}
```

At first glance, we now have a contract that requires you to write four methods when implementing a logger.

What if we want to enhance the other logging levels so that non-errors can also have exceptions?

```csharp
public interface ILogger
{
    void Information(string message);
    void Information(Exception ex, string message);
    void Warning(string message);
    void Warning(Exception ex, string message);
    void Debug(string message);
    void Debug(Exception ex, string message);
    void Error(string message);
    void Error(Exception ex, string message);
}
```

This adds more methods to implement the contract, which is a breaking change for any existing implementations.

What if we want to allow formattable messages, and error codes?

```csharp
public interface ILogger
{
    void Information(string message);
    void Information(string message, params object[] args);
    void Information(Exception ex, string message);
    void Information(Exception ex, string message, params object[] args);
    void Warning(string message);
    void Warning(string message, params object[] args);
    void Warning(Exception ex, string message);
    void Warning(Exception ex, string message, params object[] args);
    void Debug(string message);
    void Debug(string message, params object[] args);
    void Debug(Exception ex, string message);
    void Debug(Exception ex, string message, params object[] args);
    void Error(string message);
    void Error(string message, params object[] args);
    void Error(Exception ex, string message);
    void Error(Exception ex, string message, params object[] args);
}
```

A better design is to use extension methods to add functionality to the contract without breaking existing implementations.

```csharp
public static class LoggerExtensions
{
    public static void Warning(this ILogger logger, string message, params object[] args)
    {
        logger.Information(string.Format(message, args));
    }

    public static void Warning(this ILogger logger, Exception? ex, string message, params object[] args)
    {
        var formatted = args == null || args.Length == 0 ? message : string.Format(message, args);
        
        if (ex != null)
        {
            logger.Warning(ex, $"{message} Exception: {ex.Message}", args);
        }
        else
        {
            logger.Information(string.Format(message, args));
        }
    }
}
```