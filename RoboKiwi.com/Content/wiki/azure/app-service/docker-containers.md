---
title: Azure App Service Docker Containers
---

## Overview

The Azure App Service for Linux runs your applications in a Docker container, even when you're deploying an application package.

Managed Service Identity (MSI) also runs in an adjacent container, and is what the service talks to for acquiring identity tokens.

Creating a Docker container for your application and deploying this can speed up both your deployment time and startup time.

## Base images

The images used to build the hosting containers are built using scripts from the [Azure App Service ImageBuilder repo](https://github.com/Azure-App-Service/ImageBuilder).

The default dotnetcore image [source Dockerfile and scripts](https://github.com/Azure-App-Service/ImageBuilder/tree/master/GenerateDockerFiles/dotnetcore/debian-9) are based on the Debian 9 image.

The default image contains the sample placeholder application that you see if you haven't deployed an application yet into your app service.

When the container starts, the entry point is [init-container.sh](https://github.com/Azure-App-Service/ImageBuilder/blob/master/GenerateDockerFiles/dotnetcore/debian-9/init_container.sh), which initializes
the sshd, the crash dump diagnostic server and cron jobs for the diagnostics and monitor.

It then runs [Oryx](https://github.com/microsoft/oryx) to auto-generate a startup script.

This Oryx step actually has a significant impact on the application startup time, which in turn will delay things like slot swaps and CI/CD deployments as they wait for the application to warm up.

For example:

```text
2022-04-11T11:04:59.907782638Z   _____                               
2022-04-11T11:04:59.907835238Z   /  _  \ __________ _________   ____  
2022-04-11T11:04:59.907842138Z  /  /_\  \___   /  |  \_  __ \_/ __ \ 
2022-04-11T11:04:59.907848138Z /    |    \/    /|  |  /|  | \/\  ___/ 
2022-04-11T11:04:59.907853538Z \____|__  /_____ \____/ |__|    \___  >
2022-04-11T11:04:59.907858938Z         \/      \/                  \/ 
2022-04-11T11:04:59.907863839Z A P P   S E R V I C E   O N   L I N U X
2022-04-11T11:04:59.907868539Z 
2022-04-11T11:04:59.907872939Z Documentation: http://aka.ms/webapp-linux
2022-04-11T11:04:59.907877539Z Dotnet quickstart: https://aka.ms/dotnet-qs
2022-04-11T11:04:59.907882539Z ASP .NETCore Version: 6.0.0
2022-04-11T11:04:59.907887339Z Note: Any data outside '/home' is not persisted
2022-04-11T11:04:59.976714358Z Running oryx create-script -appPath /home/site/wwwroot -output /opt/startup/startup.sh -defaultAppFilePath /defaulthome/hostingstart/hostingstart.dll     -bindPort 8080 -userStartupCommand '' 
2022-04-11T11:04:59.992464677Z Cound not find build manifest file at '/home/site/wwwroot/oryx-manifest.toml'
2022-04-11T11:04:59.992486678Z Could not find operation ID in manifest. Generating an operation id...
2022-04-11T11:04:59.992491378Z Build Operation ID: 5253aa0a-6dce-490b-a709-1234d9d8f378
2022-04-11T11:05:01.820111786Z 
2022-04-11T11:05:01.820162986Z Agent extension 
2022-04-11T11:05:01.820171086Z Before if loop >> DotNet Runtime 
2022-04-11T11:05:01.826588935Z DotNet Runtime 6.0WARNING: Unable to find the startup DLL name. Could not find any files with extension '.runtimeconfig.json'
2022-04-11T11:05:02.592589725Z Writing output script to '/opt/startup/startup.sh'
2022-04-11T11:05:03.194987580Z Trying to find the startup DLL name...
2022-04-11T11:05:03.195072380Z Running the default app using command: dotnet "/defaulthome/hostingstart/hostingstart.dll"
2022-04-11T11:05:03.455414449Z Hosting environment: Development
2022-04-11T11:05:03.459829182Z Content root path: /defaulthome/hostingstart/
2022-04-11T11:05:03.463231108Z Now listening on: http://[::]:8080
2022-04-11T11:05:03.465210023Z Application started. Press Ctrl+C to shut down.
```
