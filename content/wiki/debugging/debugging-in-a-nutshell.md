---
title: Debugging in a nutshell
---

# Debugging tools

## General Setup

The `_NT_SYMBOL_PATH` environment variable is used by many IDEs, editors and debuggers for setting your symbol servers and caches for debugging third party code.

This value...

```
SRV*%TEMP%\SymbolCache*http://msdl.microsoft.com/download/symbols;SRV*%TEMP%\SymbolCache*https://nuget.smbsrc.net;SRV*%TEMP%\SymbolCache*http://referencesource.microsoft.com/symbols;SRV*%TEMP%\SymbolCache*https://dotnet.myget.org/F/dotnet-core/symbols
```

...will configure the following symbol servers:

* Microsoft Symbol Server (e.g. Windows OS symbols) (http://msdl.microsoft.com/download/symbols)
* Microsoft NuGet Symbol Server (https://nuget.smbsrc.net)
* Microsoft .NET Symbols (http://referencesource.microsoft.com/symbols)
* Microsoft .NET Core Symbols (https://dotnet.myget.org/F/dotnet-core/symbols)

... while making use of a SymbolCache directory under your user profile's `%TEMP%` directory.

Set using `cmd.exe`:

```
set _NT_SYMBOL_PATH=SRV*%TEMP%\SymbolCache*http://msdl.microsoft.com/download/symbols;SRV*%TEMP%\SymbolCache*https://nuget.smbsrc.net;SRV*%TEMP%\SymbolCache*http://referencesource.microsoft.com/symbols;SRV*%TEMP%\SymbolCache*https://dotnet.myget.org/F/dotnet-core/symbols
```

## PerfView

## WinDBG


## Debug Diagnostics

Debug Diagnostic Tool v2 Update 3.1 in the latest version, available to download from Microsoft:

https://www.microsoft.com/download/details.aspx?id=102635


# C/C++ Code

## Allow debugging optimized code

Debugging optimized Visual C++ code was difficult to impossible in some cases, even in the most simple of programs. This can be enabled with a previously undocumented and now official flag.

Visual C++ 2012: Undocumented compiler flag `/d2Zi+`

Visual Studio 2013 Update 3 or higher: Officially supported flag: `/Zo` (zed-oh) [^1]

# .NET

`CLI_DEBUG=1`


# Footnotes

[^1]: [Debugging Optimized Code–New in Visual Studio 2012](https://randomascii.wordpress.com/2013/09/11/debugging-optimized-codenew-in-visual-studio-2012/)