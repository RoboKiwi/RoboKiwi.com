---
author: David
categories:
- Applications
date: 2009-06-08T19:47:16Z
excerpt: |2

  <![CDATA[]]>
guid: https://www.davidmoore.info/?p=193
id: 193
tags:
- dota
- frozen throne
- uac
- user account control
- warcraft
- wc3banlist
- win7
- windows 7
- winpcap
title: Installing WinPcap & WC3Banlist on Windows 7
url: /blog/2009/06/08/installing-winpcap-wc3banlist-on-windows-7/
aliases: /2009/06/08/installing-winpcap-wc3banlist-on-windows-7/
---

I had a bit of trouble getting WC3Banlist (mainly due to its dependency on WinPcap) on Windows 7.

This is working on Windows 7 RC1, with User Acount Control (UAC) on (set to Default).

I did quite a few things when troubleshooting so it's hard to replicate the exact steps, but here's some instructions on how I have it set up now:

## Install WinPcap

1. [Download WinPcap 4.1](https://www.winpcap.org/install/bin/WinPcap\_4\_1\_beta5.exe)
1. Right click the downloaded installer exe and choose **Properties**
1. Go to the **Compatibility** tab
1. In *Compatibility mode*, Tick **Run this program in compatibility mode for:** and choose **Windows Vista (Service Pack 2)** from the drop-down
1. Tick **Run this program as an administrator** in Privilege Level
1. Hit **OK**
1. Run the exe and go through the normal installation

## Install WC3Banlist

1. [Download WC3Banlist](https://www.wc3banlist.de/downloads.php)
1. Right click the installation file and choose **Run as administrator**
1. Untick **Install WinPcap 3.1 (required)** when you get to the *Select additional tasks* step and proceed as normal

## Run Wc3Banlist

1. Browse to where you installed banlist and open up File Properties for `wc3banlist.exe`
1. Go to the *Compatibility* tab and tick **Run as an administrator**
1. Click **OK**
1. Run wc3banlist.exe

## Verify

1. Go to the *Preferences* tab in Wc3Banlist
1. Select *Network* in the list on the left navigation pane
1. Ensure your network card(s) are listed in the drop-down
1. You can click **Diagnostics** to verify banlist can receive TCP packets

## Troubleshooting

If this still isn't working, I would recommend turning off UAC and trying again.

1. To turn off UAC, open the Control Panel (**Start** > **Control Panel**)
1. Click **System and Security**
1. Under *Action Center*, click **Change User Account Control settings**
1. Drag the slider down to **Never Notify**
1. Click **OK**
1. You have to **restart** for this to take effect
