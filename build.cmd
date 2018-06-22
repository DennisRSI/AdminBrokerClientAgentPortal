for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /format:list') do set datetime=%%I
set datetime=%datetime:~0,8%-%datetime:~8,6%

del /q publish*.zip
rd /s /q ClientPortal\bin ClientPortal\obj Codes.Service\bin Codes.Service\obj

nuget restore ClientPortal\ClientPortal.sln
msbuild ClientPortal\ClientPortal.sln /m /t:Build /p:Configuration=Release /p:Platform="Any CPU" /p:DeployOnBuild=true /p:PublishProfile=FolderProfile

powershell Compress-Archive -Path ClientPortal\bin\Publish -Destination publish-%datetime%.zip