---
title: "MSBuild"
description: "MSBuild"
toc: true
menu:
    wiki:
        parent: net
guid: "0585a5e2-dc6f-42a6-a8b2-334b4720e68d"
---
## Carriage return line feed

You can escape the carriage return line feed using `%0A` and `%0D` respectively.

## Display item lists

This will print out the items on their own line:

```xml
<Message Text="Items: @(Items, '%0a%0d')" />
```

## Emitting .metaproj from a Visual Studio .sln

When building a Visual Studio solution file (.sln), MSBuild creates a temporary meta MSBuild project file from the proprietary solution format.

You can get `dotnet`, Visual Studio or MSBuild to emit the `.metaproj` for you to see the results.

## Diagnosing what triggered a build

MSBuild won't rebuild a project if not necessary; usually if the inputs haven't changed, the outputs don't need to be re-generated.

In older versions of MSBuild, you could pass `/diag` to enable diagnostic logging, and see in the first line what triggered the build.

## Optimizations

Never use `Copy Always` for files in your projects; this will always trigger a build of your project(s). This can cause unexpected side effects in your CI/CD pipeline,
or stop you from debugging in Visual Studio easily without a slow build every time even when your code hasn't changed.

> Recent versions of MSBuild may have optimized this issue away but I need to check this in the future.

# Parallel builds

By default, MSBuild uses one process to build your project.

You can pass the `/m` or `/maxCpuCount` argument to use all CPU cores to build projects in parallel.

You can limit the number of processes by specifying `/m:[count]` e.g. `/m:2` to use 2 processes.

## Build Optimization

Never use Copy Always; use Copy If Newer. Copy Always will always force a "build" of that project and its dependent projects. This can do things like preventing quick running or debugging when you haven't changed code. It can also affect CI/CD DevOps; if the build pipeline does several operations, they can trigger multiple builds when you're likely only wanting to run the build once.

## Notable Tasks & Targets

Microsoft.Common.CurrentVersion.targets:
    Targets: CoreResGen, BeforeResGen, AfterResGen
    Properties: $(CoreResGenDependsOn)

```xml
  <Target
      Name="CoreResGen"
      DependsOnTargets="$(CoreResGenDependsOn)">
```
