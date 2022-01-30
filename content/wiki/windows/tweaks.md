---
title: Windows Tweaks
description: Tweaks and customisations
guid: "b3c3b3c8-46b1-43d5-9840-5db05599ebd6"
---

## Task Bar

### Verbose Startup / Shutdown / Login / Logout status messages

```powershell
reg add 'HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System' /f /t REG_DWORD /v 'VerboseStatus' /d '00000001'
```

```registry
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System]
"VerboseStatus"=dword:00000001


```

### Click to cycle through grouped windows

You normally have to `CTRL + Click` on grouped windows in the Task Bar to cycle through the open windows.

This registry hack allows you to just click to open the last active window, and cycle through them in last used order by

```powershell
reg add 'HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced' /f /t REG_DWORD /v 'LastActiveClick' /d '00000001'
```

```registry
Windows Registry Editor Version 5.00

[HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced]
"LastActiveClick"=dword:00000001


```
