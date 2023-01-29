---
title: BACPAC files
---

## Overview

A BACPAC file extends the DACPAC (Data-Tier Application) format to also include data.

The .bacpac file is a zip package containing the database schema and metadata such as logins, as well as batches of BCP-format exports of the table data.

You can inspect a .bacpac file yourself by opening it up in an archive file manager like 7-Zip.

This uses the [Open Packaging Conventions](https://en.wikipedia.org/wiki/Open_Packaging_Conventions) format that's part of the Office Open XML specification. It's also known as the Packaging API in modern versions of Windows, and exposed in the `System.IO.Packaging` in .NET.

## General structure

```text
_rels/
Data/
  dbo.OrderItems
    TableData-001-00000.BCP
    TableData-002-00000.BCP
  dbo.Orders
    TableData-001-00000.BCP
    TableData-002-00000.BCP
[Content_Types].xml
DacMetadata.xml
model.xml
Origin.xml
```
