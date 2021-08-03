---
title: Azure Functions
---

# Azure Functions 4.x Early Preview

https://github.com/Azure/Azure-Functions/wiki/V4-early-preview

* Install the latest Azure Functions Tools

```bash
npm i -g azure-functions-core-tools@4 --unsafe-perm true
```

This will install func.exe in `%AppData%\npm\node_modules\azure-functions-core-tools\bin\func.exe`.

This might suffice for using Visual Studio code, but when you try to launch or debug your Function from Visual Studio, it will try to find the latest version of the tools for 3.x

Close down all instances of Visual Studio
Delete the `%UserProfile%\.templateengine` directory.