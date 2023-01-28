---
title: Gaming Performance
description: Settings and tweaks for gaming performance
guid: "d1237708-4f1c-4f62-b627-fb7898de8542"
---

## Overview

These settings are aimed at reducing latency and increasing performance at the cost of sacrificing image and rendering quality.

Depending on the game you're playing, you may be able to increase the image and rendering quality without introducing latency or letting your framerate drop too much.

## NVIDIA & G-SYNC

### 3D Settings > Manage 3D Settings

* Ambient Occlusion: Off
* Anisotropic filtering: Application-controlled or Off
* Anti-aliasing - FXAA: Off
* Anti-aliasing - Gamma correction: On/Off
* Anti-aliasing - Mode: Application-controlled or Off
* Anti-aliasing - Transparency: Off
* DSR - Factors: Off
* Low Latency Mode: Ultra
* Max Frame Rate: Limit to 3 frames below the max refresh rate of your monitor
* Monitor Technology: G-SYNC (if available)
* Multi-Frame Sampled AA (MFAA): Off
* Preferred refresh rate: Highest available
* Texture filtering - Anisotropic sample optimization: On (set to Off if you see "shimmering on objects")
* Texture filtering - Negative LOD bias: Allow
* Texture filtering - Quality: Performance
* Texture filtering - Trilinear optimisation: On
* Threaded optimisation: Auto
* Triple buffering: Off
* Vertical sync: Off
* Virtual Reality pre-rendered frames: 1 (minimum). If CPU seems to be struggling, try setting to 2 and see if framerate is smoother without introducing input lag.

If you own a G-SYNC monitor, you can set the following so you don't see any tearing but at the cost of introducing some latency (not recommended):

* Vertical sync: On
* Lower Latency Mode: Ultra
* In-game if available: NVIDIA Reflex Low Latency to On

### Display > Change Resolution

* Ensure the `Refresh rate:` is set to your highest possible Hz

### Display > Set up G-SYNC

* Tick `Enable G-SYNC, G-SYNC Compatible`
* Then, choose either `Enable for windowed and full screen mode` (recommended) or `Enable for full screen mode`

## Power and Sleep settings

* Use the High Performance plan if possible
* Check Processor Power Management
  * Minimum processor state: **100%**
  * System cooling policy: **Active**
  * Maximum processor state: **100%**

If you want to use more efficient power settings, you can disable the core parking by using an application such as Park Control

## In-Game

* If your game supports it, set NVIDIA Reflex Low Latency to On / On + Boost. This is even more effective setting NVIDIA Ultra Low Latency (NULL) in the NVIDIA Control Panel, and will take precedence if enabled.
* Always choose Fullscreen mode over Windows Full Screen or Windowed; windowed modes increase latency, sometimes over double the latency of full screen mode on lower refresh rates.
* Disable Vertical Sync

## Windows

* Enable Gaming Mode by going to Settings > Gaming > Game Mode (this is normally on by default)
* In System > Display > Graphics, you can ensure that your games are set to High Performance GPU mode.

## Mouse

In Logitech G-HUB:

* Set max polling rate `Report Rate (Per Second)` to `1000 Hz` to reduce latency
* Set the DPI to a feel that you like (I prefer a lower DPI like 750-800). This affects mouse sensitivity and is a personal preference, and does not impact system performance.

In Windows:

* Bluetooth & Devices > Mouse
* Consider leaving the Mouse pointer speed default to 10
* Click Additional Mouse Settings to open the Mouse control panel
* Go to Pointer Options and un-check `Enhance pointer precision`

## Keyboard

If you wish to remap keys or shortcuts, you can use Keyboard Manager from [Microsoft PowerToys](https://github.com/microsoft/PowerToys)

### Disable CAPS LOCK

You can manually disable the `CAPS LOCK` key with a registry command, run from an elevated terminal:

```bash
reg add 'HKLM\SYSTEM\CurrentControlSet\Control\Keyboard Layout' /f /t REG_BINARY /v 'Scancode Map' /d '00000000000000000200000000003A0000000000'
```

Or you can manually open `regedit` and edit the `Scancode Map` value under `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Keyboard Layout`:

```txt
00 00 00 00 00 00 00 00
02 00 00 00 00 00 3A 00
00 00 00 00
```

Alternatively if you're uncomfortable with editing the registry, you can use [SharpKeys](https://github.com/randyrants/sharpkeys) to remap through the UI.

* `Special: Caps Lock` map to `Turn Key off` then click `Write to Registry`

## References & Further Reading

* <https://www.blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/>
* [Mouse Click Latencies](https://mousespecs.org/mouse-click-latencies/)
