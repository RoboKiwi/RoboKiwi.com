# CodeQL

## Suppressions

Could implement suppression of CodeQL queries, by following the same options and strategies in .NET:

- Per-line suppression with inline comments
- Block suppression with inline comments
- Suppression using attributes on assembly, type, members
- Suppression and restore using directives
- EditorConfig

### Examples

```editorconfig
[*.{cs,vb}]
dotnet_diagnostic.<rule-ID>.severity = none
```

```csharp
#pragma warning disable CA2200 // Rethrow to preserve stack details
        throw e;
#pragma warning restore CA2200 /


[module: SuppressMessage("Design", "CA1055:AbstractTypesDoNotHavePublicConstructors", Scope="member", Target="MyTools.Type..ctor()")]

[assembly: SuppressMessage("Usage", "CA2200:Rethrow to preserve stack details", Justification = "Not production code.", Scope = "member", Target = "~M:MyApp.Program.IgnorableCharacters")]

[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2200:Rethrow to preserve stack details", Justification = "Not production code.")]
private static void IgnorableCharacters()
{
    try
    {
        ...
    }
    catch (Exception e)
    {
        throw e;
    }
}
```



- [Code Analysis Suppressions](https://github.com/dotnet/docs/blob/main/docs/fundamentals/code-analysis/suppress-warnings.md)
