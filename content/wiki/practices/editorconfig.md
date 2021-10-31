---
title: .editorconfig
description: How to use .editorconfig
---

## Overview

`.editorconfig` files are standard ini-style text files that allow you to enforce coding styles and standards across IDEs, editors and team members.

## Standard Values

| Value  | Description |
|----------|---------|
| `indent_style` | `tab` or `space` |
| `indent_size` | The number of characters that indentation will use |
| `tab_width` | Defaults to `indent_size` and isn't necessary |
| `end_of_line` | `lf`, `cr`, or `crlf` |
| `charset` | `latin1`, `utf-8`, `utf-8-bom`, `utf-16be` or `utf-16le`. Use of utf-8-bom is discouraged. |
| `trim_trailing_whitespace` | `true` or `false` |
| `insert_final_newline`     | `true` or `false` |
| `root` | Must be specified in the preamble. Set to `true` to stop the `.editorconfig` file search on the current file. |

## Further Reading

[EditorConfig.org](https://editorconfig.org/)