---
title: Priority order of configuration
---

## Overview

In the default ASP.NET app builder, the configuration providers are added in this order:

```csharp
var env = hostingContext.HostingEnvironment;

config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (env.IsDevelopment())
{
    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
    if (appAssembly != null)
    {
        config.AddUserSecrets(appAssembly, optional: true);
    }
}

config.AddEnvironmentVariables();

if (args != null)
{
    config.AddCommandLine(args);
}
```

The more specific the settings, the later they're loaded and the higher priority (as they will override existing settings).

| Priority | Scope | Provider | Parameters | Comments |
|----------|--------|---------|----------|--------|
| Lowest   | Project | JsonProvider | appsettings.json |   |
|    | Project and Environment | JsonProvider | appsettings.<environment>.json | |
|    | Project and Environment | JsonProvider | appsettings.<environment>.json | |
|  | Project and User | UserSecretsProvider | | Only loaded if in the Development environment. |
|  | Environment (Process > User > Machine) | EnvironmentVariableProvider | | |
| Highest | Command-line (Process) | EnvironmentVariableProvider | | |
