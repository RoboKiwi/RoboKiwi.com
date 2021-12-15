---
author: David
categories:
- Entertainment
date: 2011-06-20T11:23:47Z
guid: https://www.davidmoore.info/2011/06/20/how-to-check-if-the-current-user-is-an-administrator-even-if-uac-is-on/
id: 415
title: How to check if the current user is an Administrator (even if UAC is on)
url: /blog/2011/06/20/how-to-check-if-the-current-user-is-an-administrator-even-if-uac-is-on/
aliases: /2011/06/20/how-to-check-if-the-current-user-is-an-administrator-even-if-uac-is-on/
---

There may be a scenario where you want to determine from code if the current user is an Administrator.

One example of this which I have had to deal with is checking for software updates.

Say your application contacts a service to see if there is a newer version of the application available; if so, you can download and run the installer.

Imagine that the installer requires admin privileges; you don’t want to run the installer if the current user does not have administrative privileges.

So how can we check if the user is an admin or not?

## In VB6, C++ etc

There is a Windows API function you can use very easily to see if the current user is an admin: [IsUserAnAdmin](https://msdn.microsoft.com/library/bb776463(v=vs.85).aspx).

> BOOL IsUserAnAdmin(void);

### Visual Basic 6 Declaration

> <span style="font-family: &amp;quot;"><span style="line-height: 12pt;"><span style="color: #00007f;"><span style="font-size: 10pt;">Private</span></span></span><span style="font-size: 10pt;"><span style="line-height: 12pt;"> </span><span style="line-height: 12pt;"><span style="color: #00007f;">Declare</span></span><span style="line-height: 12pt;"> </span><span style="line-height: 12pt;"><span style="color: #00007f;">Function</span></span><span style="line-height: 12pt;"> IsUserAnAdmin </span><span style="line-height: 12pt;"><span style="color: #00007f;">Lib</span></span><span style="line-height: 12pt;"> </span><span style="line-height: 12pt;"><span style="color: #7f007f;">"Shell32&#8243;</span></span><span style="line-height: 12pt;"> </span><span style="line-height: 12pt;"><span style="color: #00007f;">Alias</span></span><span style="line-height: 12pt;"> </span><span style="line-height: 12pt;"><span style="color: #7f007f;">"#680&#8243;</span></span><span style="line-height: 12pt;"> () </span><span style="line-height: 12pt;"><span style="color: #00007f;">As</span></span><span style="line-height: 12pt;"> </span></span><span style="line-height: 12pt;"><span style="font-size: 10pt; color: #00007f;">Integer</span></span></span>

While you can still use this, it is actually deprecated, and the documentation recommends you call the [CheckTokenMembership](https://msdn.microsoft.com/library/aa376389(v=vs.85).aspx) function instead (which IsUserAnAdmin is a wrapper for).

## .NET

### C#.NET

```csharp
using System;
using System.Security.Principal;

var identity = WindowsIdentity.GetCurrent();
if (identity == null) throw new InvalidOperationException("Couldn't get the current user identity");
var principal = new WindowsPrincipal(identity);
return principal.IsInRole(WindowsBuiltInRole.Administrator);
```

## User Account Control (UAC)

A problem arises when you use any of the above code on a machine that has UAC enabled, and the process is not elevated.

While the user may be an administrator, when the process is not elevated yet, the user has a split token &#8211; which doesn’t have the administrator privileges.

A way around this is to use the [GetTokenInformation](https://msdn.microsoft.com/library/aa446671(v=VS.85).aspx) API call to inspect the token to see if it’s a split token. In _most_ cases this will mean that UAC is on and the current user is an administrator.

_This is not 100% reliable_ (see References) but it’s probably the best we can do for now.

### C#.NET

This code is slightly easier in .NET, as there’s already a fair amount of code we don’t have to write to get the current process’s token.

First, we’ll need some code to support the GetTokenInformation API call:

```csharp
[DllImport("advapi32.dll", SetLastError = true)]
static extern bool GetTokenInformation(IntPtr tokenHandle, TokenInformationClass tokenInformationClass, IntPtr tokenInformation, int tokenInformationLength, out int returnLength);

/// <summary>
/// Passed to <see cref="GetTokenInformation"/> to specify what
/// information about the token to return.
/// </summary>
enum TokenInformationClass
{
     TokenUser = 1,
     TokenGroups,
     TokenPrivileges,
     TokenOwner,
     TokenPrimaryGroup,
     TokenDefaultDacl,
     TokenSource,
     TokenType,
     TokenImpersonationLevel,
     TokenStatistics,
     TokenRestrictedSids,
     TokenSessionId,
     TokenGroupsAndPrivileges,
     TokenSessionReference,
     TokenSandBoxInert,
     TokenAuditPolicy,
     TokenOrigin,
     TokenElevationType,
     TokenLinkedToken,
     TokenElevation,
     TokenHasRestrictions,
     TokenAccessInformation,
     TokenVirtualizationAllowed,
     TokenVirtualizationEnabled,
     TokenIntegrityLevel,
     TokenUiAccess,
     TokenMandatoryPolicy,
     TokenLogonSid,
     MaxTokenInfoClass
}

/// <summary>
/// The elevation type for a user token.
/// </summary>
enum TokenElevationType
{
    TokenElevationTypeDefault = 1,
    TokenElevationTypeFull,
    TokenElevationTypeLimited
}
```

Then, the actual code to detect if the user is an Administrator (returning true if they are, otherwise false).

```csharp
var identity = WindowsIdentity.GetCurrent();
if (identity == null) throw new InvalidOperationException("Couldn't get the current user identity");
var principal = new WindowsPrincipal(identity);

// Check if this user has the Administrator role. If they do, return immediately.
// If UAC is on, and the process is not elevated, then this will actually return false.
if (principal.IsInRole(WindowsBuiltInRole.Administrator)) return true;

// If we're not running in Vista onwards, we don't have to worry about checking for UAC.
if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 6)
{
     // Operating system does not support UAC; skipping elevation check.
     return false;
}

int tokenInfLength = Marshal.SizeOf(typeof(int));
IntPtr tokenInformation = Marshal.AllocHGlobal(tokenInfLength);

try
{
    var token = identity.Token;
    var result = GetTokenInformation(token, TokenInformationClass.TokenElevationType, tokenInformation, tokenInfLength, out tokenInfLength);

    if (!result)
    {
        var exception = Marshal.GetExceptionForHR( Marshal.GetHRForLastWin32Error() );
        throw new InvalidOperationException("Couldn't get token information", exception);
    }

    var elevationType = (TokenElevationType)Marshal.ReadInt32(tokenInformation);

    switch (elevationType)
    {
        case TokenElevationType.TokenElevationTypeDefault:
            // TokenElevationTypeDefault - User is not using a split token, so they cannot elevate.
            return false;
        case TokenElevationType.TokenElevationTypeFull:
            // TokenElevationTypeFull - User has a split token, and the process is running elevated. Assuming they're an administrator.
            return true;
        case TokenElevationType.TokenElevationTypeLimited:
            // TokenElevationTypeLimited - User has a split token, but the process is not running elevated. Assuming they're an administrator.
            return true;
        default:
            // Unknown token elevation type.
            return false;
     }
}
finally
{    
    if (tokenInformation != IntPtr.Zero) Marshal.FreeHGlobal(tokenInformation);
}
```

### Visual Basic 6 (VB6)

For Visual Basic 6, there’s some additional code, as we need to get the token for the current process, and use more calls to also get the operating system version.

```vb
Type OSVERSIONINFO
  dwOSVersionInfoSize As Long
  dwMajorVersion As Long
  dwMinorVersion As Long
  dwBuildNumber As Long
  dwPlatformId As Long
  szCSDVersion As String * 128
End Type

' dwPlatformId values
Public Const VER_PLATFORM_WIN32s =
Public Const VER_PLATFORM_WIN32_WINDOWS = 1
Public Const VER_PLATFORM_WIN32_NT = 2
Public Declare Function GetVersionEx Lib "kernel32″ Alias "GetVersionExA" (ByRef lpVersionInformation As OSVERSIONINFO) As Long
' These functions are for getting the process token information, which IsUserAnAdministrator uses to
' handle detecting an administrator that's running in a non-elevated process under UAC.
Private Const TOKEN_READ As Long = &H20008
Private Const TOKEN_ELEVATION_TYPE As Long = 18
Private Declare Function IsUserAnAdmin Lib "Shell32″ Alias "#680″ () As Integer
Private Declare Function CloseHandle Lib "kernel32″ (ByVal hObject As Long) As Long
Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Long, ByVal DesiredAccess As Long, TokenHandle As Long) As Long
Private Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As Long, ByVal TokenInformationClass As Long, TokenInformation As Any,_ ByVal TokenInformationLength As Long, ReturnLength As Long) As Long

Public Function IsUserAnAdministrator() As Boolean
  On Error GoTo IsUserAnAdministratorError
  IsUserAnAdministrator = False
  If IsUserAnAdmin() Then
    IsUserAnAdministrator = True
    Exit Function
  End If

  ' If we're on Vista onwards, check for UAC elevation token
  ' as we may be an admin but we're not elevated yet, so the
  ' IsUserAnAdmin() function will return false
  Dim osVersion As OSVERSIONINFO
  osVersion.dwOSVersionInfoSize = Len(osVersion)

  If GetVersionEx(osVersion) = Then
    Exit Function
  End If

  If osVersion.dwPlatformId <> VER_PLATFORM_WIN32_NT Or osVersion.dwMajorVersion < 6 Then
    ' If the user is not on Vista or greater, then there's no UAC, so don't bother checking.
    Exit Function
  End If

Dim result As Long

Dim hProcessID As Long

Dim hToken As Long

Dim lReturnLength As Long

Dim tokenElevationType As Long

' We need to get the token for the current process

hProcessID = GetCurrentProcess()

If hProcessID <> Then

If OpenProcessToken(hProcessID, TOKEN_READ, hToken) = 1 Then

result = GetTokenInformation(hToken, TOKEN_ELEVATION_TYPE, tokenElevationType, 4, lReturnLength)

If result = Then

' Couldn't get token information

Exit Function

End If

If tokenElevationType <> 1 Then

IsUserAnAdministrator = True

End If

CloseHandle hToken

End If

CloseHandle hProcessID

End If

Exit Function

IsUserAnAdministratorError:

' Handle errors

End Function 
```
    
## References

[Blog Post by Chris Jackson: How to Determine if a User is a Member of the Administrators Group with UAC Enabled on Windows Vista](https://blogs.msdn.com/b/cjacks/archive/2006/10/09/how-to-determine-if-a-user-is-a-member-of-the-administrators-group-with-uac-enabled-on-windows-vista.aspx)