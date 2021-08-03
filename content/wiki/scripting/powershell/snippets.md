---
title: Powershell Snippets
---

# Stop and disable services in one line

```powershell
Get-Service -name "*SERVICENAME*" | Stop-Service -passthru | Set-Service -startmode disabled
```