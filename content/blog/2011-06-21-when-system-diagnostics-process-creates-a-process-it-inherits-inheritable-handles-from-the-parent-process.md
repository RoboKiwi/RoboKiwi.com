---
author: David
categories:
- Entertainment
date: 2011-06-21T16:06:13Z
guid: https://www.davidmoore.info/?p=421
id: 421
title: When System.Diagnostics.Process creates a process, it inherits inheritable
  handles from the parent process.
url: /blog/2011/06/21/when-system-diagnostics-process-creates-a-process-it-inherits-inheritable-handles-from-the-parent-process/
aliases: /2011/06/21/when-system-diagnostics-process-creates-a-process-it-inherits-inheritable-handles-from-the-parent-process/
---

This post covers the cause of a bug I ran into at work.

Our application would check for available updates when it started, and if they were found, it would launch the installer directly and exit the application immediately, so that the installer could run without encountering file locks.

The installer was complaining our executable was still locked, meaning it had to schedule the overwrite of the old file with the new one after a reboot.

After quite a bit of troubleshooting, it looked like while the application was launching msiexec and closing down successfully, msiexec was still grabbing the same lock handle to the exe for no good reason.

So what was happening?

## System.Diagnostics.Process

When you create a new process from a .NET application, you would use the classes in the [System.Diagnostics](https://msdn.microsoft.com/library/system.diagnostics.aspx) namespace. Specifically, [Process](https://msdn.microsoft.com/library/system.diagnostics.process.aspx) and [ProcessStartInfo](https://msdn.microsoft.com/library/system.diagnostics.processstartinfo.aspx). As we were in this case.

These wrap the Windows API function [CreateProcess](https://msdn.microsoft.com/library/ms682425(v=vs.85).aspx) and its alternatives and supporting types.

If we look at the `CreateProcess` definition, there’s a boolean argument in there called `bInheritHandles`:

```c
BOOL CreateProcessA(
  LPCSTR                lpApplicationName,
  LPSTR                 lpCommandLine,
  LPSECURITY_ATTRIBUTES lpProcessAttributes,
  LPSECURITY_ATTRIBUTES lpThreadAttributes,
  BOOL                  bInheritHandles,
  DWORD                 dwCreationFlags,
  LPVOID                lpEnvironment,
  LPCSTR                lpCurrentDirectory,
  LPSTARTUPINFOA        lpStartupInfo,
  LPPROCESS_INFORMATION lpProcessInformation
);
```

What does the Windows API documentation say about this?

> If this parameter is TRUE, each inheritable handle in the calling process is inherited by the new process. If the parameter is FALSE, the handles are not inherited. Note that inherited handles have the same value and access rights as the original handles.

Inheritable handles?

> [Handle Inheritance](https://msdn.microsoft.com/library/ms724466(v=vs.85).aspx "Handle Inheritance")
> 
> A child process can inherit handles from its parent process. An inherited handle is valid only in the context of the child process. To enable a child process to inherit open handles from its parent process, use the following steps.
> 
>   1. Create the handle with the `bInheritHandle` member of the `SECURITY_ATTRIBUTES` structure set to `TRUE`.
>   2. Create the child process using the `CreateProcess` function, with the `bInheritHandles` parameter set to `TRUE`.

Well if we look at the internals of .NET, and look at how the `Process` class is calling `CreateProcess`, we find that it’s passing **`bInheritHandles`** as **`TRUE`**.

So this means if we start a process using the `System.Diagnostics` classes, the child process will inherit our inheritable handles.

What was happening is the locked file handle to wsClient.exe was being inherited from the parent process to the Windows Installer, so the executable was remaining locked even when the parent process exited.

## Solution

One solution is to avoid `System.Diagnostics` in this particular instance, and use `CreateProcess` manually when we launch our child process, to ensure it doesn’t inherit handles.

I would recommend you use the `System.Diagnostics` classes in any other case.

## Code

```csharp
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace CreateProcessTest
{
    [StructLayout(LayoutKind.Sequential)]
    internal class ProcessInformation
    {
         public IntPtr hProcess = IntPtr.Zero;
         public IntPtr hThread = IntPtr.Zero;
         public int dwProcessId;
         public int dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class StartupInfo
    {
         public int cb;
         public IntPtr lpReserved = IntPtr.Zero;
         public IntPtr lpDesktop = IntPtr.Zero;
         public IntPtr lpTitle = IntPtr.Zero;
         public int dwX;
         public int dwY;
         public int dwXSize;
         public int dwYSize;
         public int dwXCountChars;
         public int dwYCountChars;
         public int dwFillAttribute;
         public int dwFlags;
         public short wShowWindow;
         public short cbReserved2;
         public IntPtr lpReserved2 = IntPtr.Zero;
         public SafeFileHandle hStdInput = new SafeFileHandle(IntPtr.Zero, false);
         public SafeFileHandle hStdOutput = new SafeFileHandle(IntPtr.Zero, false);
         public SafeFileHandle hStdError = new SafeFileHandle(IntPtr.Zero, false);

         public StartupInfo()
         {
             dwY = ;
             cb = Marshal.SizeOf(this);
         }

         public void Dispose()
         {
             // close the handles created for child process
             if (hStdInput != null && !hStdInput.IsInvalid)
             {
                 hStdInput.Close();
                 hStdInput = null;
             }

             if (hStdOutput != null && !hStdOutput.IsInvalid)
             {
                 hStdOutput.Close();
                 hStdOutput = null;
             }

             if (hStdError == null || hStdError.IsInvalid) return;

             hStdError.Close();
             hStdError = null;
         }
     }

     [StructLayout(LayoutKind.Sequential)]
     internal class SecurityAttributes
     {
         public int nLength = 12;
         public SafeLocalMemHandle lpSecurityDescriptor = new SafeLocalMemHandle(IntPtr.Zero, false);
         public bool bInheritHandle;
     }

     [SuppressUnmanagedCodeSecurity, HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
     internal sealed class SafeLocalMemHandle : SafeHandleZeroOrMinusOneIsInvalid
     {
         internal SafeLocalMemHandle() : base(true) { }

         [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
         internal SafeLocalMemHandle(IntPtr existingHandle, bool ownsHandle) : base(ownsHandle)
         {
             SetHandle(existingHandle);
         }

         [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
         internal static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(string stringSecurityDescriptor,
             int stringSDRevision, out SafeLocalMemHandle pSecurityDescriptor, IntPtr securityDescriptorSize);

         [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), DllImport("kernel32.dll")]
         private static extern IntPtr LocalFree(IntPtr hMem);

         protected override bool ReleaseHandle()
         {
             return (LocalFree(handle) == IntPtr.Zero);
         }
     }

     public static class Test
     {
         const int normalPriorityClass = 0x0020;

         [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
         internal static extern bool CreateProcess(
             [MarshalAs(UnmanagedType.LPTStr)]string applicationName,
             StringBuilder commandLine,
             SecurityAttributes processAttributes,
             SecurityAttributes threadAttributes,
             bool inheritHandles,
             int creationFlags,
             IntPtr environment,
             [MarshalAs(UnmanagedType.LPTStr)]string currentDirectory,
             StartupInfo startupInfo,
             ProcessInformation processInformation
        );

        public static void Main(string[] args)
        {
            // We can use the string builder to build up our full command line, including arguments
            var sb = new StringBuilder("notepad.exe");
            var processInformation = new ProcessInformation();
            var startupInfo = new StartupInfo();
            var processSecurity = new SecurityAttributes();
            var threadSecurity = new SecurityAttributes();

            processSecurity.nLength = Marshal.SizeOf(processSecurity);
            threadSecurity.nLength = Marshal.SizeOf(threadSecurity);

            if (CreateProcess(null, sb, processSecurity, threadSecurity, false, normalPriorityClass,
                 IntPtr.Zero, null, startupInfo, processInformation))
            {
                // Process was created successfully
                return;
            }

            // We couldn't create the process, so raise an exception with the details.
            throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
         }
     }
}
```

## Utilities

I used the invaluable [SysInternals](https://technet.microsoft.com/en-nz/sysinternals/bb842062) tools **Process Monitor**, **Process Explorer** and **Handle** to diagnose what was going on.

## References

* [Handle Inheritance @ MSDN](https://msdn.microsoft.com/library/ms724466(v=vs.85).aspx)
* [Child process keeps parent’s socket open – Diagnostics.Process and Net.TcpListene @ social.msdn.microsoft.com](https://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/94ba760c-7080-4614-8a56-15582c48f900/)