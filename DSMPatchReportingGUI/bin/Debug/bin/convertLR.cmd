echo off
set arg1=%1
powershell.exe -File bin\convertLR.ps1 -filePath %arg1%