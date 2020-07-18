---
author: David
categories:
- Applications
- How To
date: 2009-12-02T11:16:27Z
excerpt: |2

  <![CDATA[]]>
guid: https://www.davidmoore.info/?p=234
id: 234
tags:
- "64"
- 64bit
- mstsc
- mstsc.exe
- remote desktop
- system32
- syswow64
- terminal services
- vista
- windows
- windows 7
- windows7
- x64
- x86
title: Running 32-bit Remote Desktop Connection on Windows 64 bit
url: /blog/2009/12/02/running-32-bit-remote-desktop-connection-on-windows-64-bit/
aliases: /2009/12/02/running-32-bit-remote-desktop-connection-on-windows-64-bit/
---

On Windows Vista 64 and Windows 7 64, there is a 32 bit version of Remote Desktop Connection (Microsoft Terminal Services Client, mstsc.exe) in %SystemRoot%SysWOW64. Running this mstsc.exe will launch the 32 bit process but it will instantly launch the 64-bit mstsc.exe from System32 and shut itself down. This makes it impossible to run Remote Desktop Connection 32 bit. This is a problem when you have 32 bit Terminal Services add-ins (which won't run under 64 bit). 

**Solution: Rename the 64-bit mstsc.exe** from System32 to prevent it from replacing the 32-bit process.

Open an elevated command prompt or PowerShell session and run the following:

```cmd
takeown /F "%SystemRoot%System32\mstsc.exe"

```

This is simple if you have rights to rename that file. If you're on NTFS you may get a "**You require permission from TrustedInstaller to make changes to this file**" error. To get by this error, you can take Ownership of the file and give yourself full permissions.



1. Browse to **%SystemRoot%System32**
2. Right click mstsc.exe and choose **Properties**
3. Go to the **Security** tab
4. Click **Advanced**
5. Go to the **Owner** tab
6. Click **Edit**
7. From the **"Change owner to:"** list, choose your user name
8. Click **OK**
9. Go to the **Permissions** tab
10. Click **Change Permissions**
11. Click **Add**
12. Enter your user name and click **OK**
13. Tick the box in the **Allow** column for **Full control**
14. Click **OK**
15. Click **OK**
16. A Windows Security warning will come up; click **Yes** to proceed
17. Click **OK**

Now, you can rename the file mstsc.exe to something like mstsc.exe.bak Then, you can launch mstsc.exe from %SystemRoot%SysWOW64 and you will have 32-bit Remote Desktop Connection running.