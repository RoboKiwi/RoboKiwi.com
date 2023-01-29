---
title: User Secrets in .NET for local development
date: 2021-01-27T20:00:00+12:00
categories:
- development
tags:
- .net
- secrets
- config
author: David
description: Using User Secrets
url: /blog/2021/01/27/user-secrets/
---
## Local development and user secrets

Ever had to constantly edit your appsettings.json for local development and debugging purposes?

Ever accidentally checked in your appsettings.json and broken an environment?

Do you get sick of having to stash and un-stash your configuration changes as you switch branches?

Then you might want to start using User Secrets.

User Secrets are a bit of a misnomer; their main intention was to avoid developers deliberately or accidentally putting sensitive configuration like database connection string and API keys into the application settings.

Perhaps a better way of considering User Secrets is *Per-User, Local Project Settings*.

Get one developer to enable user secrets for a project, and check in the project file.

Now developers can manage their "secrets" but that can be any setting:

* Configure custom logging levels for your local development
* Configure connection strings, api keys and api URLs to point to your local development environment

The settings are persisted in your user profile, and are linked to the project. Your settings will remain even if you switch branches or re-clone the repo.

Settings in Environment variables take precedence, but remember they're environment-wide: if you set generic "ConnectionStrings__Default" for example,
then the same connection string will be used for all of your projects on your machine.
