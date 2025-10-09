---
title: Aspire
---

# Overview

# Setup / Getting Started

Aspire is introduced to your project by including the Sdk:

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <Sdk Name="Aspire.AppHost.Sdk" Version="x.x.x" />
    
    <!-- ... -->
    
    <ItemGroup>
        <PackageReference Include="Aspire.Hosting.AppHost" Version="x.x.x" />
    </ItemGroup>

    <!-- ... etc -->

    <!-- If you want to add a normal project reference to the AppHost project, set IsAspireProjectResource="false"  -->
    <ProjectReference Include="..\MyProject\MyProject.csproj" IsAspireProjectResource="false" />
    
</Project>
```



## Upgrading

Aspire 8.x onwards uses NuGet packages, so you should remove the old workloads:

`dotnet workload uninstall aspire`

This may still leave the Aspire templates installed, so you can remove them manually from `C:\Program Files\dotnet\template-packs` (otherwise, you may not be able to upgrade or install new template packs)

# References

https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/dotnet-aspire-sdk