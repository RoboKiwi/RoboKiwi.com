---
title: GitHub Actions Recipes
guid: "6c153bc9-d6e9-4785-91e2-d9ce91fe70f2"
description: A cookbook of GitHub Actions recipes
---

# Overview

GitHub Actions are incredibly powerful, but have something of a learning curve, and most things past the most basic workflows
require some scripting or less discoverable features.

# Shell types

I recommend you simply use Bash as your default shell, making your scripts more portable and easier to share.

# String Manipulation

## Uppercase / Lowercase

```bash
TESTVALUE="Hello World"
UPPERCASED=${TESTVALUE^^}    # HELLO WORLD
TITLECASED=${TESTVALUE^}     # Hello world
LOWERCASED=${TESTVALUE,,}    # hello world
FIRSTCHARLOWER=${TESTVALUE,} # hello World
```

Newer version of Bash has these operators:

```bash
TESTVALUE="Hello World"
UPPERCASED=${TESTVALUE@U}    # HELLO WORLD
TITLECASED=${TESTVALUE@u}     # Hello world
LOWERCASED=${TESTVALUE@L}    # hello world
```