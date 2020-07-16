---
author: David
categories:
- Applications
- How To
date: 2009-08-19T20:45:01Z
guid: https://www.davidmoore.info/?p=206
id: 206
tags:
- '{B8DA6310-E19B-11D0-933C-00A0C90DCAA9}'
- '{C90250F3-4D7D-4991-9B69-A5C5BC1C2AE6}'
- actxprxy.dll
- explorer
- ieproxy.dll
- iserviceprovider
- management studio
- no such interface supported
- open each folder
- same window
- sourcegear
- sql server
- team explorer
- vault
title: 'Solution: Explorer open each folder in same window error and SQL Management
  Studio, IE and Team Explorer errors'
url: /blog/2009/08/19/solution-explorer-open-each-folder-in-same-window-error-and-sql-management-studio-ie-and-team-explorer-errors/
aliases: /2009/08/19/solution-explorer-open-each-folder-in-same-window-error-and-sql-management-studio-ie-and-team-explorer-errors/
---

## Problem(s):

 * When attempting to open a folder in **Windows Explorer**, the folder opens in a new window, even if **"Open each folder in the same window"** is selected in Folder Options.
 * Some links in **Internet Explorer** don't open correctly
 * **Microsoft SQL Server Management Studio**: An error with a message like "Unable to cast COM object of type 'System._\_ComObject' to interface type 'Microsoft.VisualStudio.OLE.Interop.**IServiceProvider**'. This operation failed because the QueryInterface call on the COM component for the interface with IID '{6D5140C1-7436-11CE-8034-00AA006009FA}' failed due to the following error: **No such interface supported** (Exception from HRESULT: 0x80004002 (E\_NOINTERFACE)). (Microsoft.VisualStudio.OLE.Interop)
 * **Visual Studio Team Explorer**: When browsing using the Team Explorer window, you may get COM errors similar to those in the SQL Management Studio error above

# Solution

## Solution 1

[Download RegisterActxprxyAndIeproxy.cmd](/wp-content/uploads/2009/12/RegisterActxprxyAndIeproxy.zip)

Download the zip file, run the batch file inside **as Administrator**, then **reboot**, to fix the problem.

{{<video "/wp-content/uploads/2009/12/RegisterActxPrxy.mp4">}}

If you don't run the batch file as an administrator, you will get an error as pictured:

![ActxPrxy registration error](/wp-content/uploads/2009/08/ActxprxyRegisterError.png)

### Details

The contents of the batch file is as follows:

```cmd
@echo off

:: 32 bit and 64 bit
IF EXIST "%SystemRoot%\System32\actxprxy.dll" "%SystemRoot%\System32\regsvr32.exe" "%SystemRoot%\System32\actxprxy.dll"
IF EXIST "%ProgramFiles%\Internet Explorer\ieproxy.dll" "%SystemRoot%\System32\regsvr32.exe" "%ProgramFiles%\Internet Explorer\ieproxy.dll"

:: 64 bit only (32bit on 64 bit)
IF EXIST "%WinDir%\SysWOW64\actxprxy.dll" "%WinDir%\SysWOW64\regsvr32.exe" "%WinDir%\SysWOW64\actxprxy.dll"
IF EXIST "%ProgramFiles(x86)%\Internet Explorer\ieproxy.dll" "%WinDir%\SysWOW64\regsvr32.exe" "%ProgramFiles(x86)%\Internet Explorer\ieproxy.dll"
```

**Don't forget to reboot** after re-registering the DLLs!

Edit: The script has been updated to support 64-bit Windows

## Solution 2

Some people have reported that the following command may fix the problem when Solution 1 does not work (first mentioned by snir in the comments):

* Open up a Command Prompt (presumably in Administrator mode) **Start** > **Programs** > **Accessories** > **Command Prompt**
* Type in `sfc /scannow` and hit **Enter**

For those for which this solution works, I'd like for someone to find what file(s) were affected and repaired, so we can get a more specific solution and see if it's related to Solution 1. This solution was one I looked at before I made this post which did *not* work for me.

## Explanation:

I'm not sure of the exact details, but this is what I think I've found. Perhaps someone at Microsoft would correct or elaborate on this. Previously, **actxprxy.dll** (ActiveX Interface Marshaling Library) was used as the proxy for a multitude of system interfaces, such as IShellFolder and IServiceProvider. 

In Windows 7 (and probably Vista also), the GUID of this library has changed from {B8DA6310-E19B-11D0-933C-00A0C90DCAA9} to {C90250F3-4D7D-4991-9B69-A5C5BC1C2AE6}

Secondly, there is also a new Proxy/Stub provider found in **ieproxy.dll** of Internet Explorer (IE ActiveX Interface Marshaling Library). Some interfaces that previously used actxprxy.dll are now registered to use ieproxy.dll. Now various problematic software (such as Vault 3.x) will try to register against actxproxy using the old GUID, and for interfaces now proxied by ieproxy.dll.