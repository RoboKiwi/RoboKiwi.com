---
title: "MSBuild"
description: "MSBuild"
toc: true
menu:
    wiki:
        parent: net
---
# Carriage return line feed

You can escape the carriage return line feed using `%0A` and `%0D` respectively.

# Display item lists

This will print out the items on their own line:

```xml
<Message Text="Items: @(Items, '%0a%0d')" />
```

# Build Optimization

Never use Copy Always; use Copy If Newer. Copy Always will always force a "build" of that project and its dependent projects. This can do things like preventing quick running or debugging when you haven't changed code. It can also affect CI/CD DevOps; if the build pipeline does several operations, they can trigger multiple builds when you're likely only wanting to run the build once.

# Notable Tasks & Targets

Microsoft.Common.CurrentVersion.targets:
    Targets: CoreResGen, BeforeResGen, AfterResGen
    Properties: $(CoreResGenDependsOn)

```xml
  <Target
      Name="CoreResGen"
      DependsOnTargets="$(CoreResGenDependsOn)">
```