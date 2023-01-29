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
sudo mkdir -p /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg

# Add the stable Docker repository
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

### Install Docker Engine

```bash
sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin
```

### Start Docker

```bash
sudo service docker start
```

## Verify

```bash
sudo docker run hello-world
```

## Uninstall Docker Engine

This should remove Docker, including any older versions that had different names.

```bash
sudo apt-get remove docker docker-engine docker.io docker-ce docker-ce-cli containerd.io docker-compose-plugin
```

## References

[Install Docker Engine on Ubuntu](https://docs.docker.com/engine/install/ubuntu/)
