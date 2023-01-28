---
author: David
categories:
- .net
- How To
- Software Development
date: 2009-06-03T10:50:15Z
excerpt: |2

  <![CDATA[]]>
guid: "9bf9ad10-9e9d-4e9f-acdf-9edcf0a7716d"
id: 1461
tags:
- .net
- asynchronous
- csharp
- events
- mstest
- nunit
- threading
- unit test
title: Unit testing multi-threaded, asynchronous code and/or events
url: /blog/2009/06/03/unit-testing-multi-threaded-asynchronous-code-andor-events/
aliases: /2009/06/03/unit-testing-multi-threaded-asynchronous-code-andor-events/
---

I've been writing some unit tests recently that test some multi-threaded functionality.

Typically this involves hooking up some event handlers then waiting for some asynchronous code to fire the event before proceeding with the unit test and assertions.

The [ManualResetEvent](https://docs.microsoft.com/dotnet/api/system.threading.manualresetevent) class seems a good choice for this, and [this post](https://jopinblog.wordpress.com/2007/07/10/unit-testing-multi-threaded-asynchronous-events/ "Unit Testing Multi-Threaded Asynchronous Events") has a small example of using it in a unit test:

```csharp
[Test()]
public void AfterRunAsync()
{
    ManualResetEvent manualEvent = new ManualResetEvent(false);

    TestTestCase tc = new TestTestCase(1, "", 0, 0);
    bool eventFired = false;
    tc.RunCompleted +=
        delegate(object sender, AsyncCompletedEventArgs e) {
            Assert.IsInstanceOfType(typeof (TestTestCase), sender, "sender is TestCase");
            bool passed = tc.Passed;
            string output = tc.Output;
            eventFired = true;
            manualEvent.Set();
        };
    tc.RunAsync();
    manualEvent.WaitOne(500, false);
    Assert.IsTrue(eventFired, "RunCompleted fired");
}
```
