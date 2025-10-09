# Getting Started

```powershell
dotnet tool install --global dotnet-ef
```

```powershell
dotnet tool update --global dotnet-ef
```

dotnet tool update dotnet-ef

dotnet add package Microsoft.EntityFrameworkCore.Design


dotnet ef database drop
dotnet ef database update
dotnet ef dbcontext info


Compile models:

dotnet ef dbcontext optimize

dotnet ef dbcontext scaffold



dotnet ef migrations add Initial -o Migrations -n Data.Migrations

--namespace | -n Namespace
--output | -o Output directory / file

dotnet ef migrations bundle

dotnet ef migrations has-pending-model-changes
dotnet ef migrations list
dotnet ef migrations remove

dotnet ef dbcontext script -o output.sql

## Options

Option 	Short 	Description
--json 		Show JSON output.
--context <DBCONTEXT> 	-c 	The DbContext class to use. Class name only or fully qualified with namespaces. If this option is omitted, EF Core will find the context class. If there are multiple context classes, this option is required.
--project <PROJECT> 	-p 	Relative path to the project folder of the target project. Default value is the current folder.
--startup-project <PROJECT> 	-s 	Relative path to the project folder of the startup project. Default value is the current folder.
--framework <FRAMEWORK> 		The Target Framework Moniker for the target framework. Use when the project file specifies multiple target frameworks, and you want to select one of them.
--configuration <CONFIGURATION> 		The build configuration, for example: Debug or Release.
--runtime <IDENTIFIER> 		The identifier of the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog.
--no-build 		Don't build the project. Intended to be used when the build is up-to-date.
--help 	-h 	Show help information.
--verbose 	-v 	Show verbose output.
--no-color 		Don't colorize output.
--prefix-output 		Prefix output with level.
