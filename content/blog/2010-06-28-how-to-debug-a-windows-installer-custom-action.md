---
author: David
categories:
- .net
- Software Development
- WiX
date: 2010-06-28T10:53:58Z
guid: https://www.davidmoore.info/?p=293
id: 293
tags:
- custom action
- debug
- installer
- msi
- msibreak
title: 'HOW TO: Debug a Windows Installer custom action'
url: /blog/2010/06/28/how-to-debug-a-windows-installer-custom-action/
aliases: /2010/06/28/how-to-debug-a-windows-installer-custom-action/
---

## Prerequisites:

  * Determine the name of the custom action you want to debug
  * Ensure you have the source code and debug symbols for your custom action

## Steps

1. Set the MsiBreak environment variable (user or system) to the name of the custom action. For example, if your custom action is called MyCustomAction:
  
```cmd
    Setx MsiBreak MyCustomAction
```

2. Run your installer and wait for the message box prompt

### Details

When your custom action is about to execute, you should get this message box prompt:

![Debugging Custom Actions](/wp-content/uploads/2010/06/debugging-custom-actions.png)

Now you can use Visual Studio or another debugger such as WinDBG to attach to the specified process.

## References:

[Debugging Custom Actions](https://msdn.microsoft.com/en-us/library/aa368264(VS.85).aspx) @ msdn.microsoft.com