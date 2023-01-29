---
title: Setting up PowerShell environment
guid: "43ed8be2-e1b0-48e8-a7c3-d2b10b4ab779"
---

## Terminal: Windows Terminal

[Install from Windows Store](https://www.microsoft.com/p/windows-terminal/9n0dx20hk701) (auto-updating) or:

```powershell
winget install --id=Microsoft.WindowsTerminal -e
```

## Shell: PowerShell Core

* Install latest stable PowerShell Core from <https://github.com/PowerShell/PowerShell/releases/latest>
* Configure PowerShell Core as your default shell in Windows Terminal by going to `Settings` or hitting `ctrl+,` and ensure the default profile GUID matches the PowerShell GUID (not the *Windows* PowerShell GUID)

## Font: Cascadia Code

This is a new monospaced font from Microsoft developed for Windows Terminal, to be used for command-line apps and text editors.

> If you have Windows 11 or Visual Studio 2022, Cascadia Code will be installed, but you may still want to download the latest full release which includes Powerline and ligatures

* Download latest release from <https://github.com/microsoft/cascadia-code/releases>
* Extract the zip, right click the TTF files and choose *Install for all users*
* Recommended: Cascadia Code PL (includes ligatures, and Powerline symbols)

Set this in your Windows Terminal settings e.g.

```json
    "profiles":
    [
        {
            "guid": "{574e775e-4f2a-5b96-ac1e-a2962a402336}",
            "hidden": false,
            "name": "PowerShell",
            "source": "Windows.Terminal.PowerShellCore",
            "fontFace": "Cascadia Code PL"
        }
    ],
```

### Visual Studio Code (VSCode)

In `File` > `Preferences` > `Settings`, search for `terminal font` and set `Terminal` > `Integrated:` `Font Family` to `Cascadia Code PL`

### Visual Studio

1. Open `Tools` > `Options`
2. `Environment` > `Font and Colors`
3. Under the `Show settings for:` dropdown, select `Terminal`
4. Set the `Font` to `Cascadia Code PL`

## PSReadLine

Install the official [improved PowerShell command-line editing](https://github.com/PowerShell/PSReadLine)

```powershell
Install-Module -Name PSReadLine -AllowPrerelease -Scope CurrentUser -Force -SkipPublisherCheck
```

Add to your `$profile`:

```powershell
if ($host.Name -eq 'ConsoleHost')
{
    Import-Module PSReadLine
}
```

### Updating PSReadLine

If you need to update PSReadLine, you should close down all open PowerShell sessions and run from `Win+R`:

```powershell
pwsh.exe -noprofile -command "Install-Module PSReadLine -Force -SkipPublisherCheck -AllowPrerelease"
```

## PowerShell theming: Oh-My-Posh

* Install [Oh-My-Posh](https://github.com/JanDeDobbeleer/oh-my-posh) for console theming and coloured indicators
* Install [Posh-Git]() for git status indicators

```powershell
Install-Module posh-git -Scope CurrentUser -Force
Install-Module oh-my-posh -Scope CurrentUser -Force
```

or to update:

```powershell
Update-Module posh-git
Update-Module oh-my-posh
```

Add to your `$profile`:

```powershell
Import-Module posh-git
Import-Module oh-my-posh
Set-PoshPrompt -Theme paradox
```

> Themes other than Paradox can be seen at https://github.com/JanDeDobbeleer/oh-my-posh#themes

## Auto-completion

### dotnet

Enable [dotnet auto-completion](https://docs.microsoft.com/dotnet/core/tools/enable-tab-autocomplete) by adding the following to your `$profile`:

```powershell
# PowerShell parameter completion shim for the dotnet CLI
Register-ArgumentCompleter -Native -CommandName dotnet -ScriptBlock {
     param($commandName, $wordToComplete, $cursorPosition)
         dotnet complete --position $cursorPosition "$wordToComplete" | ForEach-Object {
            [System.Management.Automation.CompletionResult]::new($_, $_, 'ParameterValue', $_)
         }
 }
```

### WinGet

Add [WinGet auto-completion](https://github.com/microsoft/winget-cli/blob/master/doc/Completion.md) by adding the following to your `$profile`:

```powershell
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

## References

[Tutorial: Set up Powerline in Windows Terminal](https://docs.microsoft.com/windows/terminal/tutorials/powerline-setup)
