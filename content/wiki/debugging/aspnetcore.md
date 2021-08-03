---
title: Debugging ASP.NET Core
---

```csharp
                ["ASPNETCORE_URLS"] = _urls,
                ["ASPNETCORE_ENVIRONMENT"] = "Development",
                ["ASPNETCORE_Logging__Console__LogLevel__Default"] = "Debug",
                ["ASPNETCORE_Logging__Console__LogLevel__System"] = "Debug",
                ["ASPNETCORE_Logging__Console__LogLevel__Microsoft"] = "Debug",
                ["ASPNETCORE_Logging__Console__FormatterOptions__IncludeScopes"] = "true",
```