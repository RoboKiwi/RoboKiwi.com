---
title: Fresh Install
description: Notes on install order and apps after a fresh install of Windows
guid: "8c7d9ba5-3073-421d-8ec1-9019f3dd6426"
---

## Fresh

## Windows Configuration

* Set the Network to Private if it isn't already

## Essentials

* Download and install Firefox, logging in to sync
* Voidtools Everything
* Download and install KeePassXC
* 7-Zip

## Management & Scripting

* Install latest PowerShell
* Install latest Cascadia Code
* Install Chocolatey
* Install PowerShell modules:

```powershell
Install-Module -Name PSReadLine -AllowPrerelease -Scope CurrentUser -Force -SkipPublisherCheck
Install-Module posh-git -Scope CurrentUser -Force
Install-Module oh-my-posh -Scope CurrentUser -Force
```

* Set your PowerShell profile `notepad $profile`

```powershell
if ($host.Name -eq 'ConsoleHost')
{
    Import-Module PSReadLine
}

Import-Module posh-git
Import-Module oh-my-posh
Set-PoshPrompt -Theme paradox

# dotnet cli parameter completion
Register-ArgumentCompleter -Native -CommandName dotnet -ScriptBlock {
    param($commandName, $wordToComplete, $cursorPosition)
        dotnet complete --position $cursorPosition "$wordToComplete" | ForEach-Object {
          [System.Management.Automation.CompletionResult]::new($_, $_, 'ParameterValue', $_)
        }
}

# winget parameter completion
Register-ArgumentCompleter -Native -CommandName winget -ScriptBlock {
    param($wordToComplete, $commandAst, $cursorPosition)
        [Console]::InputEncoding = [Console]::OutputEncoding = $OutputEncoding = [System.Text.Utf8Encoding]::new()
        $Local:word = $wordToComplete.Replace('"', '""')
        $Local:ast = $commandAst.ToString().Replace('"', '""')
        winget complete --word="$Local:word" --commandline "$Local:ast" --position $cursorPosition | ForEach-Object {
            [System.Management.Automation.CompletionResult]::new($_, $_, 'ParameterValue', $_)
        }
}
```

* In Windows Terminal, go to Settings
  * Startup: Default Profile should be PowerShell
  * Go to the PowerShell profile, in Appearance set the Font face to Cascadia Mono PL so that the posh modules display properly
  * Go to the Windows PowerShell profile, scroll down in the General tab and toggle to Hide profile from dropdown

## From Elevated Command prompt

* Install Chocolatey

```powershell
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
```

* Disable CAPS LOCK: `reg add 'HKLM\SYSTEM\CurrentControlSet\Control\Keyboard Layout' /f /t REG_BINARY /v 'Scancode Map' /d '00000000000000000200000000003A0000000000'`

## Coding & Development

* VS Code (user installer)
  * Sync with GitHub / Microsoft account
  * Extensions:
    * Docs Authoring Pack (Microsoft)
* Git for Windows (set VS Code as the default editor)
  * Cross-platform settings (check in LF, don't modify on checkout)
    `git config --global core.autocrlf input`
* Visual Studio 2022
  * Sync with GitHub / Microsoft account
* JetBrains Resharper Ultimate (including Rider)
* Install Hugo `choco install hugo`
* Install Araxis Merge
  * Integrate with git: `code $home/.gitconfig`

```ini

[diff]
  tool = araxis

[difftool "araxis"]
  path = \"C:\\Program Files\\Araxis\\Araxis Merge\\compare.exe\"

[merge]
  tool = araxis

[mergetool "araxis"]
  path = \"C:\\Program Files\\Araxis\\Araxis Merge\\compare.exe\"

[alias]
  ad = difftool --tool=araxis --dir-diff
  ads = difftool --tool=araxis --dir-diff --staged
```

* Correct `nuget.config` if necessary: `code $env:APPDATA\NuGet\nuget.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>
```

* Install NodeJS LTS
  * Add `C:\Program Files\nodejs\node_modules\npm\bin` (npm) to the system (or at least user) `%PATH%`
* Install Docker Desktop

## Linux, Cross-Platform, WSL

`wsl --install` should be enough with newer versions of Windows.

### Manual installation

* Enable WSL Feature `Enable-WindowsOptionalFeature -Online -NoRestart -All -FeatureName Microsoft-Windows-Subsystem-Linux`
* Enable Virtual Machine Platform `Enable-WindowsOptionalFeature -Online -NoRestart -All -FeatureName VirtualMachinePlatform`
* Restart

Configure WSL:

`wsl --set-default-version 2`
`wsl --update`
`wsl --shutdown`

Install a Distro. Get a list of available options:

`wsl --list --online` or `wsl -l -o`

Then install from Windows Store or `wsl --install -d <distro>` e.g. `wsl --install -d Ubuntu-20.04`

### Configure WSL

It's a good idea to set global limits on WSL memory usage and CPU usage

```powershell
code ~/.wslconfig
```

```ini
[wsl2]

# Limits VM memory to 4 GB
memory=4GB

# Sets the VM to use two virtual processors
processors=2

# Limits swap file disk usage to 8GB
swap=8GB
```

Save and exit.

## Configure Git on WSL

1. `git config --global user.name "Your Name"`
1. `git config --global user.email "youremail@domain.com"`
1. Enable credential manager: `git config --global credential.helper "/mnt/c/Program\ Files/Git/mingw64/libexec/git-core/git-credential-manager-core.exe"`
1. Enable Azure Repos: `git config --global credential.https://dev.azure.com.useHttpPath true`

## Games

* Disable CAPS LOCK
* Install NVIDIA GeForce Experience and latest drivers
* Install Battle.net Desktop

## Internet

* `choco install youtube-dl`
* qBitTorrent
* Discord
