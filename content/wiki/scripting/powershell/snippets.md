---
title: Powershell Snippets
---

# List all environment variables

```powershell
dir env:
```

# Stop and disable services in one line

```powershell
Get-Service -name "*SERVICENAME*" | Stop-Service -passthru | Set-Service -startmode disabled
```
