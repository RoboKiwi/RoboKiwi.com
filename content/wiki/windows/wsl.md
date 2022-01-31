---
title: Windows Subsystem for Linux (WSL)
guid: "cfceb74a-99c8-4ecf-b462-ece29ae7c164"
---
## Setup on Windows 10 version 2004 or higher, and Windows 11

The WSL setup on Windows 10 2004 upwards and Windows 11 is vastly improved. You just need one command:

```bash
wsl --install
```

## Setup on Windows 10 pre-version 2004

Open an elevated terminal / command prompt

### Enable WSL Feature

You can query if the state of the optional WSL feature is enabled or disabled, then enable / install it if it's not enabled. You can use DISM or PowerShell for the task.

DISM:

```batch
dism.exe /online /get-featureinfo /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart
dism.exe /online /enable-feature /featurename:Microsoft-Windows-Subsystem-Linux /all /norestart
```

PowerShell:

```powershell
Get-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux
Enable-WindowsOptionalFeature -Online -NoRestart -All -FeatureName Microsoft-Windows-Subsystem-Linux
```

### Enable Virtual Machine Platform

PowerShell:

`Enable-WindowsOptionalFeature -Online -NoRestart -All -FeatureName VirtualMachinePlatform`

DISM:

```cmd
dism.exe /online /enable-feature /featurename:VirtualMachinePlatform /all /norestart
```

To check the status:

PowerShell:

```powershell
Get-WindowsOptionalFeature -Online -FeatureName VirtualMachinePlatform
```

### Restart

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
❯ wsl --set-default-version 2
```

## Install Linux Distro

Now you can install a distro of your choice, such as Ubuntu:

Install [Ubuntu from the Microsoft Store](https://www.microsoft.com/store/productId/9NBLGGH4MSV6)

Launch Ubuntu to finish installing, and create your username and password.

## Verify

```powershell
❯ wsl --list --verbose
  NAME      STATE           VERSION
* Ubuntu    Stopped         2
```

### Converting from WSL1 to WSL2

If you installed Ubuntu before WSL2 was installed or set as the default subsystem, then you will need to convert your distribution to WSL2.

```powershell
❯ wsl --set-default-version 2 
❯ wsl --set-version Ubuntu 2 

Conversion in progress, this may take a few minutes... 

For information on key differences with WSL 2 please visit https://aka.ms/wsl2 

Conversion complete.
```
