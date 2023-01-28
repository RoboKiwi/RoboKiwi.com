---
title: Git
description: Git recipes and tricks
guid: "742d447d-7989-417a-892d-be575dd3768f"
---

## Enforce `LF` for line endings

Using `LF` for line endings, even if you might develop on Windows, will support easy cross-platform, cross-IDE development. This can help in particular when you're using WSL on Windows.

Global git setting `git config --global core.autocrlf input` or edit `%USERPROFILE%\.gifconfig`:

```ini
[core]
    autocrlf = input
```

If you want to force LF line endings for other users of your repository, create a `.gitattributes` file in your repository base:

```ini
* text=auto eol=lf
```

As a one-time conversion, you can normalize line endings for files that have already been committed previously:

```bash
git add --renormalize .
git commit -m "Normalize all the line endings"
```

Optional: Create an `.editorconfig` file:

```ini
[*]
end_of_line = lf
insert_final_newline = true
```

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

## Rename `master` to `main`

```bash
git branch -M main
```

## Import local git repository to GitHub

Create new, empty repository in GitHub e.g. `https://github.com/username/Repo.git`

> Line 2 simultaneously renames `master` to `main` to follow new best practices

```bash
git remote add origin https://github.com/username/Repo.git
git branch -M main
git push -u origin main
```

## Reset last commit

```bash
git reset --hard HEAD^
```

## Create a tag

```bash
git tag -a v1.5 -m "Tag description"
```

## Push tags

When you create a local tag, it isn't pushed by default, so use `--tags`:

```bash
git push origin --tags
```

## Fetch latest version of branch without checking out

```bash
git fetch <upstream> <branch>:<branch>
```

## Fetching submodules

```bash
git submodule init
git submodule update
```

## Ownership

You might get this warning from git, and the Git panel in Visual Studio may not show that you're in a Git repository:

```bash
fatal: unsafe repository ('C:/path/to/repo' is owned by someone else)
```

This could happen if the repository was checked out by a different user on your machine. You can take ownership (from an elevated Administrator terminal)

```cmd
takeown /R /F 'C:/path/to/repo'
```

Alternatively, you can trust the repo:

```bash
git config --global --add safe.directory C:/path/to/repo
```

## Git policies

* [GitFlow](https://nvie.com/posts/a-successful-git-branching-model/)
* [GitHub Flow](https://guides.github.com/introduction/flow/)
* [OneFlow](https://www.endoflineblog.com/oneflow-a-git-branching-model-and-workflow) and (https://www.endoflineblog.com/gitflow-considered-harmful)
* [Azure SDK policies](https://azure.github.io/azure-sdk/policies_repobranching.html#release-branches)
