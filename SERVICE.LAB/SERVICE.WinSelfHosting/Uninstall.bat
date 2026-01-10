@echo off

setlocal
set filepath=%~dp0

c:
cd\
cd Windows\Microsoft.NET\Framework\v4.0.30319
installutil -u "%filepath%WInBarcodeService.exe"
%windir%\system32\services.msc




