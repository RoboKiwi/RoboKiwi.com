---
author: David
categories:
- C++
date: 2013-05-27T13:26:20Z
guid: https://www.sadrobot.co.nz/?p=641
id: 641
tags:
- visual studio
title: "Error C2039: 'SetDefaultDllDirectories' when targetting Visual Studio 2012 Windows XP C++ Runtime"
url: /blog/2013/05/27/error-c2039-setdefaultdlldirectories-when-targetting-visual-studio-2012-windows-xp-c-runtime/
aliases: /2013/05/27/error-c2039-setdefaultdlldirectories-when-targetting-visual-studio-2012-windows-xp-c-runtime/
---

We’re switching our legacy C++ projects from Visual C++ 2010 to the Visual C++ 2012 Runtime, now that Microsoft allows you to [target Windows XP for C++ in 2012](https://blogs.msdn.com/b/vcblog/archive/2012/10/08/10357555.aspx "target Windows XP for C++ in 2012") (available in Visual Studio Update 1).

So that involves switching the Platform Target from v100:

![](/wp-content/uploads/2013/05/image.png)

To v110_xp:

![](/wp-content/uploads/2013/05/image1.png)

Well upon compilation, I saw these errors for one particular project:

![](/wp-content/uploads/2013/05/image2.png)

The key error being **Error C2039: 'SetDefaultDllDirectories' : is not a member of '\`global namespace"** from line **638** in **atlcore.h**

Well if we jump into that code, we see this:

```cpp
#ifndef _USING_V110_SDK71_
	// the LOAD_LIBRARY_SEARCH_SYSTEM32 flag for LoadLibraryExW is only supported if the DLL-preload fixes are installed, so
	// use LoadLibraryExW only if SetDefaultDllDirectories is available (only on Win8, or with KB2533623 on Vista and Win7)...
	IFDYNAMICGETCACHEDFUNCTION(L"kernel32.dll", SetDefaultDllDirectories, pfSetDefaultDllDirectories)
	{
		return(::LoadLibraryExW(pszLibrary, NULL, LOAD_LIBRARY_SEARCH_SYSTEM32));
	}
#endif
```

It looks like that define **should** exist, as we’re targeting “V110\_SDK71” (aka v110\_xp).

Well, with a little digging, that define is getting created by the C++ MSBuild files in **C:\Program Files (x86)\MSBuild\Microsoft.Cpp\v4.0\V110\Platforms\Win32\PlatformToolsets\v110\_xp\Microsoft.Cpp.Win32.v110\_xp.props**:

```xml
<ItemDefinitionGroup>
    <ClCompile>
      <!-- Add /D_USING_V110_SDK71_ when targeting XP -->
      <PreprocessorDefinitions>_USING_V110_SDK71_;%(PreprocessorDefinitions)</PreprocessorDefinitions>
    </ClCompile>
```

But was getting blown away in my project file:

```xml
<ItemDefinitionGroup Condition="'$(Configuration)|$(Platform)'=='Release|Win32'">
	<ClCompile>
		<SuppressStartupBanner>true</SuppressStartupBanner>
		<WarningLevel>Level3</WarningLevel>
		...
		<PreprocessorDefinitions></PreprocessorDefinitions>
		...
	</ClCompile>
</ItemDefinitionGroup>
```

So the fix is to include any existing pre-processor definitions (i.e. the Microsoft one) before defining our own (don’t forget to do this for all configurations and platforms in your project file):

```xml
		<PreprocessorDefinitions>%(PreprocessorDefinitions)</PreprocessorDefinitions>
```

Otherwise, you can simply remove the PreprocessorDefinition element itself (if you have no defines of your own), or choose to inherit from the parent or project defaults from the project properties (which will essentially do the same thing):

![](/wp-content/uploads/2013/05/image_thumb3.png)

And now we recompile fine.