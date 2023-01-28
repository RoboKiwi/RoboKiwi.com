:: UnrealProjectMenuRegistration.cmd
::
:: This batch file will try to find where the Unreal Engine is installed, then update your registry
:: to register the Unreal Project File type (and its right click menu for things such as generating
:: Visual Studio project files, and launching)
::
:: Run as an Administrator, as it's required for writing to HKLM.
::
:: Author: David Moore <david@sadrobot.co.nz>

@ECHO OFF

echo.
echo Locating where Unreal Engine is installed by checking the Windows registry...

FOR /F "tokens=2*" %%A IN ('reg query "HKLM\SOFTWARE\EpicGames\Unreal Engine" /v "INSTALLDIR"') DO (
	SET "UnrealEngineDir=%%B"
)
IF "%UnrealEngineDir%"=="" GOTO CannotFindUnrealEngine

echo.
echo Using Unreal Engine directory: %UnrealEngineDir%

set LauncherPath=%UnrealEngineDir%Launcher\Engine\Binaries\Win64\UnrealVersionSelector.exe

echo.
echo Adding registry keys for the Unreal Project right click menu...

:: HKLM\SOFTWARE\Classes\.uproject
reg ADD "HKLM\Software\Classes\.uproject" /ve /d Unreal.ProjectFile /f
IF ERRORLEVEL 1 (
    echo Couldn't write to registry. Did you forget to run this batch file as an administrator?
    goto TheEnd
)

:: HKLM\SOFTWARE\Classes\Unreal.ProjectFile
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile" /ve /d "Unreal Engine Project File" /f

:: HKLM\SOFTWARE\Classes\Unreal.ProjectFile\DefaultIcon
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\DefaultIcon" /ve /d "\"%LauncherPath%\"" /f

:: HKLM\SOFTWARE\Classes\Unreal.ProjectFile\shell\open
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\open" /ve /d "Open" /f
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\open\command" /ve /d "\"%LauncherPath%\" /editor \"%%1\"" /f

:: HKLM\SOFTWARE\Classes\Unreal.ProjectFile\shell\run
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\run" /ve /d "Launch game" /f
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\run" /v Icon /t REG_SZ /d "\"%LauncherPath%\"" /f
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\run\command" /ve /d "\"%LauncherPath%\" /game \"%%1\"" /f

:: HKLM\SOFTWARE\Classes\Unreal.ProjectFile\shell\rungenproj
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\rungenproj" /ve /d "Generate Visual Studio project files" /f
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\rungenproj" /v Icon /t REG_SZ /d "\"%LauncherPath%\"" /f
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\rungenproj\command" /ve /d "\"%LauncherPath%\" /projectfiles \"%%1\"" /f

:: HKLM\SOFTWARE\Classes\Unreal.ProjectFile\shell\switchversion
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\switchversion" /ve /d "Switch Unreal Engine version..." /f
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\switchversion" /v Icon /t REG_SZ /d "\"%LauncherPath%\"" /f
reg ADD "HKLM\Software\Classes\Unreal.ProjectFile\shell\switchversion\command" /ve /d "\"%LauncherPath%\" /switchversion \"%%1\"" /f

:Done
echo.
echo Done.
echo.
goto TheEnd

:CannotFindUnrealEngine
echo.
echo DOH! Can't find the Unreal Engine.
echo.
goto TheEnd

:TheEnd
pause