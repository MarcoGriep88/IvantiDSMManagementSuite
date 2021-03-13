echo off
set arg1=%1
powershell.exe -File bin\convertVR.ps1 -filePath %arg1%