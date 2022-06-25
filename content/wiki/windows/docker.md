---
title: Docker on Windows
---

## Overview

With Docker Desktop for Windows changing licensing, you might want to get up and running using Docker on your Windows machine without it.

## Install Windows Subsystem for Linux (WSL)

Windows 10 build 2004 or higher (including Windows 11) it's now super easy to configure WSL:

```bash
wsl --install
```

## Install Docker on Ubuntu WSL

### Set up the official Docker repository

```bash

# Update the package indexes
sudo apt-get update

# Pre-requisite packages
sudo apt-get install ca-certificates curl gnupg lsb-release

# Add Docker’s official GPG key
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

# Add the stable Docker repository
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

### Install Docker Engine

```bash
sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io
```

## Verify

```bash
sudo docker run hello-world
```
