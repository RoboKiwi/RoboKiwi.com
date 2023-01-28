---
title: Windows Security
guid: "668e6489-3fa9-4ec6-8127-ed10a9c70bcb"
---

## Wiping unused drives

Quick-format the drive

Wipe the free space:

```cmd
cipher w:<drive letter>
```

Example:

```
To remove as much data as possible, please close all other applications while
running CIPHER /W.
Writing 0x00
.........................................................................................................
Writing 0xFF
.........................................................................................................
Writing Random Numbers
.........................................................................................................
```

There'll be a folder left behind called EFSTMPWP which you can delete once it's done.

You can also run SDelete:

```cmd
sdelete -p <passes> -c <drive letter>
```

Or use it just to clean a directory:

```cmd
sdelete -p <passes> -s <path>
```

## Hardening

Remove SMB 1.0 that has vulnerabilities (SMB 3.0 is still fine)

`Remove-WindowsFeature -Name FS-SMB1`

## Service Accounts

You can create per-service security identifiers (SID) for each service, so you can configure service rights on a per-service basis, as opposed to using shared accounts that give all services that use them the same privileges, or the overhead of  manually creating service accounts for each service.

Query SID type: `sc qsidtype <service>`
View privileges for a service: `sc qprivs <service>`

View the SID for a service: `sc showsid <service>`

Set the SID type to Restricted:

`sc sidtype <service> <none|unrestricted|restricted>`
