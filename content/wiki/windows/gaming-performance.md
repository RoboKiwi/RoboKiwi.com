---
title: Gaming Performance
description: Settings and tweaks for gaming performance
---

## NVIDIA & G-Sync

* Disable v-sync in NVIDIA Control Panel and In-game (reduce input lag)
* Enable G-Sync
* Limit in-game FPS to 3 frames below the max refresh rate of the monitor
* Max pre-rendered frames: 1 (minimum). If CPU seems to be struggling, try setting to 2 and see if framerate is smoother without introducing input lag.
* Disable triple-buffering

## Power and Sleep settings

* Use the High Performance plan if possible
* Check Processor Power Management
  * Minimum processor state: **100%**
  * System cooling policy: **Active**
  * Maximum processor state: **100%**

If you want to use more efficient power settings, you can disable the core parking by using an application such as Park Control

## Mouse

Logitech:

* Set max polling rate to 1000Hz

## Keyboard

If you wish to remap keys or shortcuts, you can use Keyboard Manager from [Microsoft PowerToys](https://github.com/microsoft/PowerToys)

### Disable CAPS LOCK

You can manually disable the `CAPS LOCK` key with a registry change:

```ini
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout]
"Scancode Map"=hex:00,00,00,00,00,00,00,00,02,00,00,00,00,00,3a,00,00,00,00,00

```

Or you can manually open `regedit` and edit the `Scancode Map` value under `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout`:

```
00 00 00 00 00 00 00 00
02 00 00 00 00 00 3A 00
00 00 00 00
```

Alternatively if you're uncomfortable with editing the registry, you can use [SharpKeys](https://github.com/randyrants/sharpkeys) to remap through the UI.

* `Special: Caps Lock` map to `Turn Key off` then click `Write to Registry`


## References

* <https://www.blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/>