---
title: Windows Subsystem for Linux (WSL)
---
## Setup

Open an elevated terminal / command prompt

### Enable WSL Feature

Enable WSL:

DISM: `dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart`
PowerShell: `Enable-WindowsOptionalFeature -Online -NoRestart -All -FeatureName Microsoft-Windows-Subsystem-Linux`

> You can query if the state of the optional WSL feature is enabled or disabled:
> DISM: `dism.exe /online /get-featureinfo /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart`
> PowerShell: `Get-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux`

### Enable Virtual Machine Platform

PowerShell: `Enable-WindowsOptionalFeature -Online -NoRestart -All -FeatureName VirtualMachinePlatform`
DISM: `dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart`

> PowerShell: `Get-WindowsOptionalFeature -Online -FeatureName VirtualMachinePlatform`

## Restart

If you saw restart was needed when you ran either of the previous commands, restart the machine, or do so now to be safe.

### Update WSL2

```txt
❯ wsl --update
Checking for updates...
Downloading updates...
Installing updates...
This change will take effect on the next full restart of WSL. To force a restart, please run 'wsl --shutdown'.
Kernel version: 5.10.60.1
❯ wsl --shutdown
```

wsl --set-default-version 2

## Install Ubuntu

Install [Ubuntu from the Microsoft Store](https://www.microsoft.com/store/productId/9NBLGGH4MSV6)

Launch Ubuntu to finish installing, and create your username and password.

## Verify

```powershell
❯ wsl --list --verbose
  NAME      STATE           VERSION
* Ubuntu    Stopped         2
```

## Converting from WSL1 to WSL2

If you installed Ubuntu before WSL2 was installed or set as the default subsystem, then you will need to convert your distribution to WSL2.

```powershell
❯ wsl --set-default-version 2 
❯ wsl --set-version Ubuntu 2 

Conversion in progress, this may take a few minutes... 

For information on key differences with WSL 2 please visit https://aka.ms/wsl2 

Conversion complete.
```
