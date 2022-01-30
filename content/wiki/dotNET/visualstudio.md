---
title: Visual Studio
description: Visual Studio Performance, Tips and Tricks
guid: "335c6b70-2416-4fa7-9b89-28674097950f"
---

# Web project node_modules performance

If you have a web project that consumes node_modules, such as a React web app, then the Solution Explorer may become unresponsive when doing file operations (right clicking nodes, creating / renaming / deleting files or folders).

## Solution: DefaultItemExcludes

You must add the `node_modules` path(s) to the `DefaultItemExcludes` property at the top of your project file.

## Detailed example

A more detailed example based on the ASP.NET Core project template, which will also disable the Visual Studio TypeScript compilation, and only publish the built outputs from webpack.

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
    <TypeScriptToolsVersion>Latest</TypeScriptToolsVersion>
    <IsPackable>false</IsPackable>
    <SpaRoot>app\</SpaRoot>
    <!-- This is the key line that stops node_modules scanning from impacting Visual Studio performance -->
    <DefaultItemExcludes>$(DefaultItemExcludes);$(SpaRoot)node_modules\**</DefaultItemExcludes>
  </PropertyGroup>

  <!-- Package references, project references here etc -->

  <!-- Exclude source files from publishing -->
  <ItemGroup>
    <Content Remove="$(SpaRoot)**" />
    <None Remove="$(SpaRoot)**" />
    <None Include="$(SpaRoot)**" Exclude="$(SpaRoot)node_modules\**" />
  </ItemGroup>

  <!-- Ensure outputs from webpack are published -->
  <Target Name="PublishRunWebpack" AfterTargets="ComputeFilesToPublish">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm install" />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run build" />

    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
      <DistFiles Include="$(SpaRoot)build\**" />
      <ResolvedFileToPublish Include="@(DistFiles->'%(FullPath)')" Exclude="@(ResolvedFileToPublish)">
        <RelativePath>%(DistFiles.Identity)</RelativePath>
        <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
        <ExcludeFromSingleFile>true</ExcludeFromSingleFile>
      </ResolvedFileToPublish>
    </ItemGroup>
  </Target>

</Project>
```
