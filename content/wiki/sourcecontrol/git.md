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
git branch -d <branch> # Delete local branch
git push -d <remote> <branch> # Delete remote branch
git branch -dr <remote>/<branch> # Delete local remote-tracking branch

git fetch <remote> -p # Delete all local remote-tracking branches (--prune)
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

# Import local git repository to GitHub

* Create new empty repository in GitHub, simultaneously renaming `master` to `main`

```bash
git remote add origin https://github.com/username/Repo.git
git branch -M main
git push -u origin main
```

## Git policies

GitFlow: https://nvie.com/posts/a-successful-git-branching-model/

GitHub Flow: https://guides.github.com/introduction/flow/

OneFlow: https://www.endoflineblog.com/oneflow-a-git-branching-model-and-workflow (https://www.endoflineblog.com/gitflow-considered-harmful)

Azure SDK policies: https://azure.github.io/azure-sdk/policies_repobranching.html#release-branches