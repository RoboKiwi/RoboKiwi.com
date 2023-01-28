---
author: David
categories:
- .net
- WiX
date: 2010-06-30T17:09:49Z
guid: "20b1e314-0d10-47c5-a65b-11e270c4cdd8"
id: 302
tags:
- "2010"
- how to
- msi
- redistributable
- vc
- visual c++
- wix
title: 'HOW TO: Detect if the Visual C++ 2010 redistributable package is installed
  with WiX'
url: /blog/2010/06/30/how-to-detect-if-the-visual-c-2010-redistributable-package-is-installed-with-wix-2/
aliases: /2010/06/30/how-to-detect-if-the-visual-c-2010-redistributable-package-is-installed-with-wix-2/
---

As noted by Aaron Stebner, there is now a registry key you can search for to detect if the Visual C++ 2010 redistributable package is installed a machine, when installing your application.

There are 3 different (but very similar) registry keys for each of the 3 platform packages. Each key has a `DWORD` value called `Installed` with a value of `1`:

* `HKLM\SOFTWARE\Microsoft\VisualStudio10.0\VC\VCRedistx86`
* `HKLM\SOFTWARE\Microsoft\VisualStudio10.0\VC\VCRedistx64`
* `HKLM\SOFTWARE\Microsoft\VisualStudio10.0\VC\VCRedistia64`

Here's an example of using this in WiX, detecting the presence of the x86 version of the redistributable:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Include>
    <!-- Visual C++ 2010 x86 -->
    <Property Id="HASVCPP2010">
        <RegistrySearch Id="HasVCPP2010Search" Root="HKLM" Key="SOFTWARE\Microsoft\VisualStudio10.0\VC\VCRedistx86" Name="Installed" Type="raw" />
    </Property>    
    <Condition Message="This application requires Microsoft Visual C++ 2010 Redistributable Package (x86).">Installed OR (HASVCPP2010)</Condition>
</Include>
```

When someone runs your installer and they don’t have this package installed, they will get something like this message box when the installer initializes:

![This application requires Microsoft Visual C++ 2010 Redistributable Package (x86)](/wp-content/uploads/2010/06/image.png)

It’s a good idea to have a setup bootstrapper that automatically installs this package if it’s missing, but this WiX snippet is a good safe-guard for if someone directly runs your MSI.

**Reference**: <https://blogs.msdn.com/b/astebner/archive/2010/05/05/10008146.aspx>
