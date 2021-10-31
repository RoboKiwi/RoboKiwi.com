---
title: Make a bootable Windows USB Drive
description: Turn your USB pen drive into a bootable Windows installer
---

## Format the USB drive and make it bootable

Replace ``?`` with the letter of your USB Drive below

```cmd
diskpart
list disk
select disk ?
clean
create partition primary
select partition 1
active
format fs=ntfs quick label="Windows Setup"
exit
```

## Make it boot Windows 11 Installer

### Copy over Windows 11 files

Use 7-zip to extract the contents of the .iso to the USB drive

### Update Bootcode on the USB Drive

Replace ``?`` with the drive letter of your USB Drive

```cmd
C:\> & bootsect.exe /nt60 ?:

Target volumes will be updated with BOOTMGR compatible bootcode.

?: (\\?\Volume{guid})

    Successfully updated NTFS filesystem bootcode.

Bootcode was successfully updated on all targeted volumes.
```

## Optional: Show an icon for your USB drive

You can configure an icon to display for your USB Drive to show in Windows Explorer, which can help easily identifying the drive.

Find an .ico file you'd like to use, or create one yourself.

Rename the file e.g. `autorun.ico`

In the root of the USB drive, create a new text file called autorun.inf and add the contents:

```ini
[AutoRun.Amd64]
icon=autorun.ico

[AutoRun]
icon=autorun.ico
```

Your drive should now display an icon in Windows Explorer.

## See Also

* Windows Media Creation Tool @ [Windows 11 Download](https://www.microsoft.com/software-download/windows11)
