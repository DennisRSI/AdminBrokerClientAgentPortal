for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /format:list') do set datetime=%%I
set build1=1
set build2=%datetime:~2,2%
set build3=%datetime:~4,4%
set build4=%datetime:~8,4%

set buildnum=%build1%.%build2%.%build3%.%build4%

del /q ClientPortal*.zip
rd /s /q ClientPortal\bin ClientPortal\obj Codes.Service\bin Codes.Service\obj

:: To use msbuild directly:
:: nuget restore ClientPortal\ClientPortal.sln
:: msbuild ClientPortal\ClientPortal.sln /m /t:Build /p:Configuration=Release /p:Platform="Any CPU" /p:DeployOnBuild=true /p:PublishProfile=FolderProfile /p:Version=%buildnum%

dotnet publish ClientPortal\ClientPortal.sln -c Release -o bin\Publish -r win-x64 /p:Platform="Any CPU" /p:PublishProfile=FolderProfile /p:Version=%buildnum%

powershell Compress-Archive -Path ClientPortal\bin\Publish -Destination ClientPortal-%buildnum%.zip