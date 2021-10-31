---
title: Windows Subsystem for Linux (WSL)
---
## Setup

Open an elevated terminal / command prompt

### Enable WSL Feature

`dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart`

### Enable Virtual Machine Platform

`dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart`

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
