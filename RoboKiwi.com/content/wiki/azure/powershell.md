---
title: Azure PowerShell Module
guid: "55c565a4-a9f4-40b9-900a-601d8c746cd3"
---

## Installation and updating

### Remove the deprecated Azure PowerShell

The old Azure PowerShell modules aka AzureRM module can conflict with the new module, so you should remove it if you have it.

If you don't remember installing them manually, run the Visual Studio Install, Modify your installation, and in the Individual Components, search for `Azure PowerShell` and un-tick it, and click Modify.

Otherwise, try go to Add / Remove Programs and look for `Microsoft Azure PowerShell - <month> <year>` and uninstall manually.

### Install Az PowerShell module

```powershell
Install-Module -Name Az -Scope CurrentUser -Repository PSGallery -Force
```

Now you can sign in:

```powershell
Connect-AzAccount
```

## Updating Az PowerShell module

You can update the Az module:

```powershell
Update-Module -Name Az -Scope CurrentUser -Force
```

## Get Started

Authenticate your session (this will open a browser tab)

```powershell
Connect-AzAccount
```
