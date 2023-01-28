---
title: Versioning
description: Versioning recommendations
guid: "f060a21b-5058-4345-8417-e042e28adade"
---

## Windows Installer

Product Version only allows 3 digits

* 1st and 2nd digits are actually limited from 0 to 255
* 3rd digit allows up to 65535

## Date versioning

It's easy to use date and time as the version number, as this is deterministic and not depending on maintaining some kind of state that is necessary when using an auto-incrementing number.

`YY.MM.hhmm` would work including MSI installers that couldn't support full year (being limited to 255). Alternatively, you could use YYYY in all places but the installer (installer "truncated" to the last two digits.)

Of course if you're generating more than one build of a product within a minute then you could get collisions, at least in continuous builds, obviously not going to happen in release. So you need to decide if this is even going to be an issue.

If you're not using MSI, then this opens up more options for more exact and less likely to collide builds:

`YYMM`.`HHmm`.`ssmmm`
