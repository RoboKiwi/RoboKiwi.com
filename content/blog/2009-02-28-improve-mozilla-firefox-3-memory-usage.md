---
author: David
categories:
- Applications
- How To
date: 2009-02-28T14:04:39Z
excerpt: |2

  <![CDATA[]]>
guid: https://davidmoore.info/?p=111
id: 1431
tags:
- firefox
- memory
- minimize
- ram
- usage
- virtual memory
title: Improve Mozilla Firefox 3 memory usage
url: /blog/2009/02/28/improve-mozilla-firefox-3-memory-usage/
aliases: /2009/02/28/improve-mozilla-firefox-3-memory-usage/
---

Firefox still tends to be a bit of a memory hog over *an extended period of time*, even though the latest major version (3) has made massive memory usage improvements over previous versions.

This tweak will make sure Firefox behaves more like standard Windows applications when it comes to being minimized, as in freeing up the memory it's using for Windows.

The down-side to this is that when restored or maximized again, there will be a delay as the application shifts chunks from slow virtual memory to system memory.

* Go to **about:config**
* Right click anywhere in the list and choose **New** > **Boolean**
* Call the entry **`config.trim_on_minimize`** and set it to **`true`**
* Restart Firefox
* You might want to go to about:config to confirm the change took effect
