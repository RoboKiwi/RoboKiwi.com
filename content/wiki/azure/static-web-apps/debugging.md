---
title: Debugging Azure Static Web Apps with Functions
---

## Overview

Launch the API server through Visual Studio Code or Visual Studio e.g. on port `7071`
Launch your website host e.g. Hugo on port `1313`
Launch the debugging proxy:

```bash
swa start http://localhost:1313 --api-location http://localhost:7071
```

Don't worry, it will wait for your backend services to be ready if they haven't started up, or you try to launch the Static Web Apps emulator first:

```bash
[swa] - Waiting for http://localhost:7071 to be ready
[swa] ✔ Connected to http://localhost:7071 successfully
[swa] 
[swa] Using dev server for static content:
[swa]     http://localhost:1313
[swa]
[swa] Using dev server for API:
[swa]     http://localhost:7071
[swa]
[swa]
[swa] This CLI is currently in preview and runs an emulator that may not match the
[swa] cloud environment exactly. Always deploy and test your app in Azure.
[swa]
[swa]
[swa] Azure Static Web Apps emulator started at http://localhost:4280. Press CTRL+C to exit.
[swa]
[swa]
```
