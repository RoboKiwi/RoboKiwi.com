---
author: David
categories:
- .net
- MSBuild
- Software Development
- Visual Studio
date: 2014-07-19T17:15:50Z
guid: "069ae124-37a9-45e8-8fb9-3b8e278f1669"
id: 1081
title: 'Using MSBuild property functions and inline tasks: Example doing performance
  calculations'
url: /blog/2014/07/19/using-msbuild-property-functions-and-inline-tasks-example-doing-performance-calculations/
aliases: /2014/07/19/using-msbuild-property-functions-and-inline-tasks-example-doing-performance-calculations/
---

## The Problem

User RandDavis on Reddit <a href="https://www.reddit.com/r/dotnet/comments/2at6q3/msbuild_elapsed_time/" target="_blank">asked a question</a> about capturing elapsed time of tasks in MSBuild:

> I'm using MSBuild 4.0 (I also have MSBuild.Community.Tasks available). Note that I'm new to the syntax involved. All I'm trying to do is this: **store the current time** to a property, **run a process**, and **determine the time that has elapsed**. I've managed to write System.DateTime.Now to a property, but I don't know how to do a simple datediff or construct a TimeSpan, so that I can get at what I'm looking for. I'd be utterly shamed if I had to resort to string comparisons or writing a custom task.

## Options

The good thing is that he's using MSBuild 4.0, which means he can use any combination of <a title="MSBuild Property Functions" href="https://msdn.microsoft.com/library/dd633440.aspx" target="_blank">property functions</a> and <a title="MSBuild Inline Tasks" href="https://msdn.microsoft.com/library/dd722601.aspx" target="_blank">inline tasks</a> to achieve all he wants from within the MSBuild code, without having to compile and version custom MSBuild task assemblies, which can become a pain in the ass to move forwards with.

When benchmarking from .NET, the best practice is to use the **Stopwatch** class in **System.Diagnostics**. Using DateTime functions is the natural but more inaccurate and naive way to benchmark. Eric Lippert of C# compiler fame did a good series on benchmarking mistakes, with Stopwatch mentioned in [part 2](https://tech.pro/tutorial/1295/c-performance-benchmark-mistakes-part-two "C# Performance Benchmark Mistakes Part Two by Epic Lippert").

In MSBuild, you’re limited to what classes you can use for property functions, so this means we have to resort to using inline tasks if we want to use Stopwatch. And because MSBuild is an imperative, XML-based language, we can’t really use Stopwatch in the normal way (get an instance, start and stop it, etc).

Because Rand (I’m guessing that’s his name) doesn’t seem to require much precision, using DateTime might be more than enough, and it can also make for some simpler code.

### Solution 1: Using property functions and DateTime ticks

So here’s the first example, using property functions and DateTime:

{{<gist "3bcc180796c116f55a2b" "ElapsedTime.DateTime.proj">}}

Interestingly, the Stopwatch uses DateTime Ticks if it can’t use high precision time. So this is likely as good as we’re going to get using DateTime. If we run the project using MSBuild, Notepad should pop up. We can leave it open for a bit, then close it down, and see what measurement we got:

```cmd
    C:\>"C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" Test.DateTime.proj
    Microsoft (R) Build Engine version 12.0.30501.0
    [Microsoft .NET Framework, version 4.0.30319.18408]
    Copyright (C) Microsoft Corporation. All rights reserved.

    Build started 19/07/2014 5:07:25 p.m..
    Project "C:\Projects\Test.DateTime.proj" on node 1 (default targets).
    Test:
    Starting ticks: 635413432457421728
    notepad
    Elapsed time: 00:00:02.4999296
    Done Building Project "C:\Projects\Test.DateTime.proj" (default targets).

    Build succeeded.
    0 Warning(s)
    0 Error(s)

    Time Elapsed 00:00:02.51
```

OK, seems fair enough! The code is quite simple too, and could be fine if we’re not needing much precision.

### Solution 2: Using inline tasks and Stopwatch

OK, so how can we use inline tasks to leverage the power of the .NET framework?

{{<gist "3bcc180796c116f55a2b" "ElapsedTime.Stopwatch.proj">}}

Phew! Ok this code would be smaller and simpler if you removed some of my comments, and Microsoft had made some of the key methods and properties in the Stopwatch class available.

So here's how that looks:

```cmd
C:\Projects>"C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" Test.StopWatch.proj
Microsoft (R) Build Engine version 12.0.30501.0
[Microsoft .NET Framework, version 4.0.30319.18408]
Copyright (C) Microsoft Corporation. All rights reserved.

Build started 19/07/2014 5:52:59 p.m..
Project "C:\Projects\Test.StopWatch.proj" on node 1 (default targets).
Test:
  Starting timestamp: 5443820462687
  notepad
  Elapsed time: 00:00:02.7861944
Done Building Project "C:\Projects\Test.StopWatch.proj" (default targets).


Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:02.87
```

Would it be possible some way to return a Stopwatch object that could be started and stopped, as you would do in C# code? Possibly! But I think that wouldn’t fit well with the way MSBuild works.

### References

* [MSBuild Property Functions](https://msdn.microsoft.com/library/dd633440.aspx "MSBuild Property Functions Reference")
* [MSBuild Inline Tasks](https://msdn.microsoft.com/library/dd722601.aspx "MSBuild Inline Tasks")
* [C# Performance Benchmark Mistakes Part Two](https://tech.pro/tutorial/1295/c-performance-benchmark-mistakes-part-two "C# Performance Benchmark Mistakes Part Two by Epic Lippert")
