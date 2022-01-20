---
title: Uninstalling Adobe Creative Cloud
description: Help with removing Adobe bloatware
---

While trying to update Creative Cloud, I ran into a hell of a time trying to update applications.

Attempting to uninstall everything and start clean, I found I couldn't uninstall anything.

# Downloading and launching Creative Cloud Uninstaller

You can use the Creative Cloud Uninstaller from Adobe to get you pretty far.

1. Download the Creative Cloud Uninstaller.
2. Make sure you Unblock the zip file before you extract it.
3. Open an elevated command prompt or powershell
4. Run the exe

# Using the Creative Cloud Uninstaller

Choose your language, accept agreement

Choose to remove *All* or your particular products e.g. `Creative Cloud 2018, 2017, 2015, 2014, Creative Cloud & CS6 Products`

```
    Choose from one of the following options to clean up :
    1.  All
    2.  Adobe Flash Player 10.2
    3.  Creative Cloud 2018, 2017, 2015, 2014, Creative Cloud & CS6 Products
    4.  Creative Cloud 2018, 2017, 2015 only
    5.  Creative Cloud 2014 only
    6.  Creative Cloud only
    7.  CS6 only
    8.  CS5-CS5.5-CS6
    9.  CS5-CS5.5
    10. CS3, CS4
    11. Adobe Id credentials
    12. Fix Host File
    13. Quit
    Choice :>
```

You'll then get a list of found products, for example:

```
    Listing products for cleanup:
    2. KANC  ('Adobe Notification Client (32 Bit)',)
    3. SPRK  ('XD',)
    4. LIBS  ('CC Library (32 Bit)',)
    5. COSY  ('CoreSync (32 Bit)',)
    6. CCXP  ('CCX Process (32 Bit)',)
    7. All
 ```

You can just choose the number for `All`

Once the uninstaller is finished you'll be greeted with something like:

```
    LOG FILE SAVED TO: C:\Users\YourUsername\AppData\Local\Temp\Adobe Creative Cloud Cleaner Tool.log


    Adobe Creative Cloud Cleaner Tool completed successfully
```

Hit ENTER to exit the tool.

Then, you should run the tool repeatedly to ensure that the list of installed products is eliminated.

You may need to clean products one by one in a different order, as the cleaner doesn't seem to deal with dependencies well.

For example, I uninstalled the CCX Process, then the CC Library, before Adobe Notification Client could be removed.

The log file will get added to each time you run the tool, so once you've finished running it as many times as needed, you should make a backup of the log.

## Removing Windows Store apps

Applications such as Adobe XD (formerly Spark) are installed as Store / Windows / AppX apps.

Open up an elevated PowerShell and you can check using `Get-AppxPackage -AllUsers Adobe.*`:

```
PS C:\users\david\Downloads> Get-AppxPackage -AllUsers Adobe.*


Name                   : Adobe.CC.XD
Publisher              : CN=Adobe Systems Incorporated, OU=Adobe Systems, O=Adobe Systems Incorporated, L=San Jose,
                         S=California, C=US, SERIALNUMBER=2748129, OID.2.5.4.15=Private Organization,
                         OID.1.3.6.1.4.1.311.60.2.1.2=Delaware, OID.1.3.6.1.4.1.311.60.2.1.3=US
Architecture           : X64
ResourceId             :
Version                : 22.3.12.2
PackageFullName        : Adobe.CC.XD_22.3.12.2_x64__adky2gkssdxte
InstallLocation        : C:\Program Files\WindowsApps\Adobe.CC.XD_22.3.12.2_x64__adky2gkssdxte
IsFramework            : False
PackageFamilyName      : Adobe.CC.XD_adky2gkssdxte
PublisherId            : adky2gkssdxte
PackageUserInformation : {S-1-5-21-1461965896-1903698537-2016398235-1001 [david]: Installed}
IsResourcePackage      : False
IsBundle               : False
IsDevelopmentMode      : False
NonRemovable           : False
Dependencies           : {Microsoft.VCLibs.140.00_14.0.27810.0_x64__8wekyb3d8bbwe,
                         Microsoft.VCLibs.140.00.UWPDesktop_14.0.29016.0_x64__8wekyb3d8bbwe,
                         Microsoft.NET.Native.Framework.1.3_1.3.24211.0_x64__8wekyb3d8bbwe,
                         Microsoft.NET.Native.Runtime.1.4_1.4.24201.0_x64__8wekyb3d8bbwe}
IsPartiallyStaged      : False
SignatureKind          : Developer
Status                 : Ok
```

Now I can try removing this application manually:

```
Get-AppxPackage Adobe.* | Remove-AppxPackage -AllUsers -WhatIf
```



If this locks up or isn't working, you can try manually.

cd "C:\Program Files\WindowsApps\Adobe.CC.XD_22.3.12.2_x64__adky2gkssdxte"
takeown -F *.exe
del *.exe

Set permissions:

icacls *.exe /grant "David:(F)"




## Check tasks, services, registry

Run Autoruns:

You could go to All and put adobe in the filter

Check Scheduled Tasks
Check Logon
under Drivers, there may be an Adobe Type Manager down in the Font Drivers section

## Remove Directories

```
"C:\Program Files\Common Files\Adobe"
"C:\Program Files\Adobe"
"C:\Program Files (x86)\Common Files\Adobe"
"C:\Program Files (x86)\Adobe"
C:\ProgramData\Adobe

C:\adobetemp
C:\Users\david\AppData\Local\Adobe
C:\Users\david\AppData\Roaming\Adobe
C:\Users\Public\Documents\Adobe
C:\Users\Public\Documents\AdobeGCData
C:\Users\david\AppData\Local\Temp\CreativeCloud
C:\Users\david\AppData\Local\Temp\Adobe*.*

C:\Windows\Temp\CreativeCloud\
C:\Windows\Temp\Adobe*.*
C:\Windows\System32\config\systemprofile\AppData\Roaming\Adobe
```

Check there may be a C:\Temp folder e.g. "C:\temp\{8EE5A4CA-4CCF-4086-A994-E2D9ABA37908}\Adobe Desktop Common"
If there is only the one Adobe folder in there, you could delete it

