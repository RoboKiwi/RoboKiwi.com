---
author: David
categories:
- How To
- Software Development
date: 2009-08-19T16:10:40Z
excerpt: |2

  <![CDATA[]]>
guid: "a420bd26-33b7-471d-aa5a-03c6caae63cb"
id: 200
tags:
- argument
- batch
- cmd
- dir
- dos
- drive
- expand
- extension
- filename
- modifier
- name
- parameter
- path
- variable
title: Using batch parameter modifiers to get specific portions or file info from
  a full filename
url: /blog/2009/08/19/using-batch-parameter-modifiers-to-get-specific-portions-or-file-info-from-a-full-filename/
aliases: /2009/08/19/using-batch-parameter-modifiers-to-get-specific-portions-or-file-info-from-a-full-filename/
---

From: [Using batch parameters](https://www.microsoft.com/resources/documentation/windows/xp/all/proddocs/en-us/percent.mspx?mfr=true)

> Cmd.exe provides the batch parameter expansion variables %0 through %9. When you use batch parameters in a batch file, %0 is replaced by the batch file name, and %1 through %9 are replaced by the corresponding arguments that you type at the command line

These batch parameter modifiers variable arguments are immensely useful. That document is missing some examples, so I've put together a simple table:

| Modifier | Description                                                  | Example                                         |
|----------|--------------------------------------------------------------|-------------------------------------------------|
| %1       | Original argument                                            | `"C:\Users\DMoore\Documents\Document Name.txt"` |
| %~1      | Expands %1 and removes any surrounding quotation marks (""). | `C:\Users\DMoore\Documents\Document Name.txt`   |
| %~f1     | Expands %1 to a fully qualified path name.                   | `C:\Users\DMoore\Documents\Document Name.txt`   |
| %~d1     | Expands %1 to a drive letter.                                | `C:`                                            |
| %~p1     | Expands %1 to a path, with a trailing slash.                 | `Users\DMoore\Documents\`                       |
| %~n1     | Expands %1 to a file name.                                   | `Document Name`                                 |
| %~x1     | Expands %1 to a file extension.                              | `.txt`                                          |
| %~s1     | Expanded path contains short names only.                     | `C:\Users\DMoore\DOCUME~1\DOCUME~1.TXT`         |
| %~a1     | Expands %1 to file attributes.                               | `–a——`                                          |
| %~t1     | Expands %1 to date and time of file.                         | `19/08/2009 02:53 p.m.`                         |
| %~z1     | Expands %1 to size of file. (bytes)                          | `9`                                             |
