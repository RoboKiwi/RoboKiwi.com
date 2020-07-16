---
author: David
categories:
- .net
- How To
date: 2010-12-17T09:38:31Z
guid: https://www.davidmoore.info/?p=381
id: 391
tags:
- .net
- config
- NetFx40_LegacySecurityPolicy
- startup
- supportRuntime
- useLegacyV2RuntimeActivationPolicy
title: Running .NET 2 Runtime applications under the .NET 4 Runtime
url: /blog/2010/12/17/running-net-2-runtime-applications-under-the-net-4-runtime/
aliases: /2010/12/17/running-net-2-runtime-applications-under-the-net-4-runtime/
---

In some situations, you might want to run a .NET 2 Runtime application (.NET 2, 3.0, 3.5 SP1 etc) under the .NET 4 Runtime &#8211; without recompiling.

You can configure your app to execute under the .NET 4 runtime by adding these lines to the executable's configuration file, under the root ``<configuration>`` element:

```xml
<startup>
  <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
</startup>
```

Because the security framework has changed in the .NET 4 runtime, you will likely encounter some exceptions containing a message similar to:

> System.NotSupportedException: This method explicitly uses CAS policy, which has been obsoleted by the .NET Framework. In order to enable CAS policy for compatibility reasons, please use the NetFx40_LegacySecurityPolicy configuration switch. Please see https://go.microsoft.com/fwlink/?LinkID=155570 for more information.

To avoid this, you have to enable the legacy support by adding a runtime element :

```xml {hl_lines=[4,5,6]}
<startup>
  <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
</startup>
<runtime>
  <NetFx40_LegacySecurityPolicy enabled="true"/>
</runtime>
```

If your application also uses mixed assemblies that contain both managed and native code (such as System.Data.SQLite.dll), you'll see an error message like this:

> Mixed mode assembly is built against version 'v2.0.50727' of the runtime and cannot be loaded in the 4.0 runtime without additional configuration information.

You will need to enable the legacy activation to allow these to be loaded also:

```xml {hl_lines=[1]}
<startup useLegacyV2RuntimeActivationPolicy="true">
  <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
</startup>
<runtime>
  <NetFx40_LegacySecurityPolicy enabled="true"/>
</runtime>
```

### References

* [`<startup>` Element @ MSDN](https://msdn.microsoft.com/en-us/library/bbx34a2h.aspx)
* [`<supportedRuntime>` Element @ MSDN](https://msdn.microsoft.com/en-us/library/w4atty68.aspx)
* [`<NetFx40_LegacySecurityPolicy>` Element @ MSDN](https://msdn.microsoft.com/en-us/library/dd409253.aspx)