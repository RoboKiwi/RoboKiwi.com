---
title: Git
description: Git recipes and tricks
---
# Git

## Delete branch

```bash
git branch -d <branch>
git push -d <remote> <branch>
```

## Create an empty branch

```bash
git checkout --orphan <branch>
git reset --hard
```

## Clear the working directory

```bash
git rm --cached -rf .
```