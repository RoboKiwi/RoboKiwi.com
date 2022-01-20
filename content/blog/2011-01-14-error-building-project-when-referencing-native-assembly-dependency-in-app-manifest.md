---
author: David
categories:
- .net
- How To
- Software Development
date: 2011-01-14T08:45:38Z
guid: /?p=404
id: 404
tags:
- manifest
- sxs
title: Error building project when referencing native assembly dependency in app.manifest
url: /blog/2011/01/14/error-building-project-when-referencing-native-assembly-dependency-in-app-manifest/
aliases: /2011/01/14/error-building-project-when-referencing-native-assembly-dependency-in-app-manifest/
---

If you're using an app.manifest, and defining assembly dependencies (i.e. for SxS / Side by side / Reg-free COM etc), you may encounter this error when you build the project:

`Could not find file 'AssemblyName, Version=x.x.x.x, PublicKeyToken=xxxxxxxxxxx, ProcessorArchitecture=x86, Type=win32'.`

This is even when the native assembly is in place where the project can find it.

## Example

Your `app.manifest` may contain this fragment:

```xml
 <dependency>
    <dependentAssembly>
        <assemblyIdentity name="Native.Custom" version="1.0.0.0" processorArchitecture="x86" type="win32" publicKeyToken="12345678"/>
    </dependentAssembly>
</dependency>
```

This manifest is available to the project, with the manifest file in the project directory or in a subdirectory called `Native.Custom`.

In this case, I have a sub directory called `Native.Custom`, which contains my `Native.Custom.manifest` file.

## Solution

The problem may be because the ClickOnce manifests are being generated.

  1. Open your project file in a text editor (or right click it in Visual Studio and choose `Edit Project`)
  1. Find the `GenerateManifests` element and set it to false: ```<GenerateManifests>false</GenerateManifests>```
  1. Save the project and reload it.

Now you should hopefully be able to build.
