---
title: Git
description: Git recipes and tricks
---
# Git

## Create and checkout a new branch

```bash
git checkout -b <branch>
```

## Delete branch locally and remotely

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

## Git policies

GitFlow: https://nvie.com/posts/a-successful-git-branching-model/

GitHub Flow: https://guides.github.com/introduction/flow/

OneFlow: https://www.endoflineblog.com/oneflow-a-git-branching-model-and-workflow (https://www.endoflineblog.com/gitflow-considered-harmful)

Azure SDK policies: https://azure.github.io/azure-sdk/policies_repobranching.html#release-branches