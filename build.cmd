@ECHO OFF

cd %~dp0

del "*.nupkg"

rem nuget pack -Build -Symbols -Properties Configuration=Release Source\LandauMedia.Telemetry\LandauMedia.Telemetry.csproj
echo %~dp0
dotnet pack --include-symbols -o ..\..\ LandauMedia.Telemetry.sln
rem dotnet pack --include-symbols -o "%~dp0" LandauMedia.Telemetry.sln