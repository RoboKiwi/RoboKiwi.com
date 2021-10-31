---
title: "Code Coverage"
description: "Configuring Code Coverage"
toc: true
weight: 10
menu:
    wiki:
        identifier: "code-coverage"
        weight: 40
---

## Collect default code coverage

You collect code coverage by passing `--collect "Code coverage"` to your `dotnet test` command line, e.g.:

```
dotnet test .\MyProject.UnitTests\MyProject.UnitTests.csproj --collect "Code coverage"
```

This generates a `.coverage` file, which is a proprietary binary format that's only viewable in Visual Studio Enterprise.

## Problems when collecting code coverage

* Coverage being collected for third party libraries you don't control
* Coverage being collected for unit tests themselves
* Potential issues with coverage then being collected for dynamically generated fakes, such as those by Moq
* Coverage being collected for generated code or internals
* Hard to diagnose what is being collected (or not collected) if you don't have Visual Studio Enterprise

## Configuring Code Coverage

You can configure Code Coverage by using a `.runsettings` file.

This is an XML configuration file, and there is no longer built-in support for editing this in Visual Studio so you must edit it by hand.

# Recommendations

* Add a `.runsettings` file for code coverage
* Make sure you set `mergeDefaults` to `false` for the `ModulePaths`, and then explicitly add the patterns for matching your included and excluded files.
* The included pattern doesn't seem to be reliable, so you will likely need to add a lot of exclusions for libraries such as Moq, and to exclude your Unit Test and Integration Test projects.
* The module paths may be normalized to lower case, so the regex should be lower case too.
* Add exclusions to the `Sources` for boilerplate or high-level files that don't need coverage e.g. `Startup.cs`, `Global.asax.cs`
* Use ReportGenerator to generate viewable output that can also be integrated easily into your CI/CD pipeline, so everyone can get visibility on your code coverage
* Add various unit testing framework attributes to the ignore lists

# .runsettings Boilerplate

```xml
<?xml version="1.0" encoding="UTF-8"?>
<RunSettings>
	<RunConfiguration>
		<ResultsDirectory>.\TestResults</ResultsDirectory>
		<EnvironmentVariables>
			<!-- List of environment variables we want to set-->
			<!-- <DOTNET_ROOT>C:\ProgramFiles\dotnet</DOTNET_ROOT> -->
		</EnvironmentVariables>
	</RunConfiguration>
	<DataCollectionRunSettings>
		<DataCollectors>
			<DataCollector friendlyName="Code Coverage" uri="datacollector://Microsoft/CodeCoverage/2.0">
				<Configuration>
					<CodeCoverage>
						<ModulePaths mergeDefaults="false">
              				<Include>
								<ModulePath>.*MyProject.*\.dll$</ModulePath>
								<ModulePath>.*\.exe$</ModulePath>
							</Include>
							<Exclude>
								<ModulePath>.*TestAdapter.*</ModulePath>
								<ModulePath>.*NUnit.*</ModulePath>
								<ModulePath>.*\\moq\.dll$</ModulePath>
								<ModulePath>.*\\fluentvalidation\.dll$</ModulePath>
								<ModulePath>.*\\MyCompany\.MyProject\.unittests\.dll</ModulePath>
								<ModulePath>.*(unit|integration)tests\.dll</ModulePath>
								<ModulePath>.*CodeCoverage.exe$</ModulePath>
								<ModulePath>.*\Coverlet.Collector.dll$</ModulePath>
								<ModulePath>.*\Coverlet.Core.dll$</ModulePath>
							</Exclude>
						</ModulePaths>
            <Sources>
              <Exclude>
                <Source>.*\\Program.cs$</Source>
                <Source>.*\\Startup.cs$</Source>
                <Source>.*\\Global.asax.cs$</Source>
              </Exclude>
            </Sources>
            <UseVerifiableInstrumentation>True</UseVerifiableInstrumentation>
			<AllowLowIntegrityProcesses>True</AllowLowIntegrityProcesses>
			<CollectFromChildProcesses>True</CollectFromChildProcesses>
			<CollectAspDotNet>false</CollectAspDotNet>
          </CodeCoverage>
				</Configuration>
			</DataCollector>
			<!-- Configuration for blame data collector -->
			<DataCollector friendlyName="blame" enabled="True">
			</DataCollector>
		</DataCollectors>
	</DataCollectionRunSettings>
	<LoggerRunSettings>
		<Loggers>
			<Logger friendlyName="console" enabled="True">
				<Configuration>
					<Verbosity>quiet</Verbosity>
				</Configuration>
			</Logger>
			<Logger friendlyName="trx" enabled="True"/>
		</Loggers>
	</LoggerRunSettings>
</RunSettings>
```

# References

* [Use code coverage to determine how much code is being tested @ docs.microsoft.com](https://docs.microsoft.com/en-us/visualstudio/test/using-code-coverage-to-determine-how-much-code-is-being-tested)
* [VSTest Platform @ GitHub](https://github.com/microsoft/vstest/)
* [dotnet test @ docs.microsoft.com](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test)
* https://github.com/Microsoft/vstest-docs/blob/master/docs/RunSettingsArguments.md
* https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/GlobalTool.md
* https://docs.microsoft.com/en-us/azure/devops/pipelines/test/review-code-coverage-results?view=azure-devops
* https://github.com/danielpalme/ReportGenerator/wiki/Integration
* https://docs.microsoft.com/en-us/visualstudio/test/vstest-console-options
* Default configuration @ https://github.com/microsoft/vstest/blob/master/test/Microsoft.TestPlatform.Utilities.UnitTests/DefaultCodeCoverageConfig.xml
