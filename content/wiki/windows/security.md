---
title: Windows Security
---

# Wiping unused drives

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