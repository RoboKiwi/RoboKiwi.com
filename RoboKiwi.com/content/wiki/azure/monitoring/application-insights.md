---
title: Application Insights
---

## Configuration

You can configure Application Insights through the configuration pipeline (environment variables, appsettings.json etc).

The section name is `ApplicationInsights`, and will be bound to the `ApplicationInsightsServiceOptions` object.

```json
{
    "ApplicationInsights": {
        "EnableDependencyTrackingTelemetryModule": false,
    }
}
```
