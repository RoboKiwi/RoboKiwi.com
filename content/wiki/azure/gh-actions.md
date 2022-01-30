---
title: GitHub Actions for Azure
guid: "6c153bc9-d6e9-4785-91e2-d9ce91fe70f2"
---

## Overview

There are a number of official and community GitHub Actions for Azure.

## List of actions

|Name  |Description  | URL  | Source  |
|---------|---------|---------|---------|
|azure/login|         |         | [@azure/login](https://github.com/Azure/login) |
|Row2     |         |         |         |
|Row3     |         |         |         |
|Row4     |         |         |         |

## GitHub hosted runner software

The [GitHub Hosted Runners](https://docs.github.com/en/actions/using-github-hosted-runners/about-github-hosted-runners) currently come with
the `az cli` and `Az PowerShell` modules, so you can easily script your workflows and actions using those and the other software included.

## azure/login

You can use this action to log in, which uses `az cli` behind the scenes and optionally `Az PowerShell` if you specify `enable-AzPSSession: true`.
