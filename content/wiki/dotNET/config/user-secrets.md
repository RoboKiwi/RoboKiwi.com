---
title: User Secrets
---

## Overview

User Secrets can store particular app settings for a project against the local developer's user profile, so that it doesn't need to be checked into source control.

This is typically sensitive settings like connection strings and API keys, but is also a very effective workflow for a developer when wanting to change any application settings specific to what they're working on, such as changing logging levels.

{{<warning>}}### User Secrets security

User Secrets aren't inherently secure; the motivation behind User Secrets is to **prevent sensitive configuration from being stored in appsettings.json and checked into source control**. While these sensitive settings are now not stored in source control, they are still stored on the developer's machine and can be compromised, in the same manner as alternatives like environment variables.
{{</warning>}}

## Getting Started

By default, User Secrets are only loaded into the configuration when ASP.NET detects you're running the `Development` environment:

```csharp {hl_lines=[1]}
if (env.IsDevelopment())
{
    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
    if (appAssembly != null)
    {
        config.AddUserSecrets(appAssembly, optional: true);
    }
}
```

## How User Secrets work

Under the covers, user secrets isn't even a full-fledged configuration provider; it is only interested in finding a UserSecretsId, using that to locate the `secrets.json` file if it exists, and simply passing that through to the actual `JsonConfigurationProvider` to load in the settings from that file (the same one that deals with `appSettings.json` et al).

1. Find the UserSecretsId.
1. Does `%APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json` (Windows) or `~/.microsoft/usersecrets/<UserSecretsId>/secrets.json` (Mac/Linux) exist?
1. Pass `secrets.json` through to the `JsonConfigurationProvider` to parse and add to the configuration pipeline.

> There is a special `DOTNET_USER_SECRETS_FALLBACK_DIR` environment variable you can set as a final fallback, which may serve as an undocumented way to share secrets by setting it to a common location.

### Finding the UserSecretsId

If User Secrets were initialized in the default way, then a UserSecretsId guid was randomly generated and added to the project file. The compiler finds this value and generates a `UserSecretsIdAttribute` on the assembly containing this value.

When the default User Secrets provider executes, it is passed the executing assembly, tries to find the attribute on this assembly and then uses that to locate the `secrets.json`.

If the User Secrets provider is being added manually, there are overrides that allow you to manually pass through the assembly you want the secrets loaded for (provided that assembly has a `UserSecretsIdAttribute`).

You can also simply pass through the `UserSecretsId` string you want the configuration framework to use for locating the secrets.

## Finding the User Secrets directory

.NET tries to *resolve* your User Secrets directory in this order. Note that it won't check the paths; it will simply try to resolve the first environment variable it can.

| Windows | Mac / Linux |
|----------|-------------|
| `%APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json` |  |
| `%HOME%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json` | `~/.microsoft/usersecrets/<UserSecretsId>/secrets.json` |
| `%DOTNET_USER_SECRETS_FALLBACK_DIR%\Microsoft\UserSecrets\` | `$DOTNET_USER_SECRETS_FALLBACK_DIR/.microsoft/usersecrets/<UserSecretsId>/secrets.json` |

{{<warning>}}
There is the undocumented potential for you to set `$APPDATA` on Mac or Linux to specify your own base path for the user secrets, as it gets resolved first. I wouldn't recommend
this as it could cause huge potential problems for other parts of the framework that look for the `APPDATA` environment variable and expect it to be Windows-based.
{{</warning>}}

## How the UserSecretsIdAttribute is applied to your assembly

When you initialize the User Secrets, you will get the property added to your project:

```xml
<UserSecretsId>guid</UserSecretsId>
```

The compiler then trims this value and dynamically applies the attribute to your assembly at compilation time:

```xml
<ItemGroup Condition=" '$(UserSecretsId)' != '' AND '$(GenerateUserSecretsAttribute)' != 'false' ">
<AssemblyAttribute Include="Microsoft.Extensions.Configuration.UserSecrets.UserSecretsIdAttribute">
    <_Parameter1>$(UserSecretsId.Trim())</_Parameter1>
</AssemblyAttribute>
</ItemGroup>
```

## Troubleshooting

### .NET isn't loading my User Secrets

If you're not manually configuring configuration providers yourself, the default builders for .NET and ASP.NET will only load User Secrets if you're running from the Development environment.

If you'd like User Secrets to be loaded in other environments, you can configure the configuration providers yourself when configuring your application builder in `Program.cs`.

## FAQ

### Why User Secrets over Environment Variables?

User Secrets are per-user, per-project. You can switch freely between many different projects that all have a different User Secret value for `ConnectionStrings:ConnectionString`. However, if you configure a `ConnectionStrings__ConnectionString` environment variable, that is a single value in your User or System environment, and you will have to change the value whenever you switch between projects.

### Can I choose my own UserSecretsId?

Yes. The id can be any string value, and doesn't need to be a GUID or something random. You can add or change the UserSecretsId property in your project file yourself, or specify it to the `dotnet user-secrets` CLI.

### Can I specify to the framework exactly which secrets.json file to load?

You can only pass the UserSecretsId to the framework (either as a string parameter, or through an assembly that's been annotated with the `UserSecretsIdAttribute` either manually or through the project system).

The secrets filename is also hardcoded as a `const` of `secrets.json`.

However, the User Secrets provider isn't doing anything special; if you wish, you could replicate it locating and loading in json files from your own predetermined paths.

### I want to generate and specify the UserSecretsIdAttribute myself

In your project file, you can opt out of the compiler generating the attribute for you:

```xml
<PropertyGroup>
    <GenerateUserSecretsAttribute>false</GenerateUserSecretsAttribute>
</PropertyGroup>
```

Then, you'll need to apply the attribute yourself:

```csharp
assembly: [Microsoft.Extensions.Configuration.UserSecrets.UserSecretsIdAttribute("Your user secrets id")]
```
