@echo off

Echo Enabling Process Performance Counters
Reg add HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PerfProc\Performance /v "Disable Performance Counters" /t REG_DWORD /d 0 /f
if ERRORLEVEL 1 goto Failed

Echo Disabling UAC remote logon token filtering
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\system" /v LocalAccountTokenFilterPolicy /t REG_DWORD /d 1 /f
if ERRORLEVEL 1 goto Failed

Echo Starting, and Enabling RemoteRegistry Service For Auto-Start, with failure recovery options

net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
net stop RemoteRegistry /Y
@echo Exited with %ErrorLevel%
if ERRORLEVEL 1 (
	if NOT %ErrorLevel% == 2 goto Failed
)

sc config RemoteRegistry start= auto
if ERRORLEVEL 1 goto Failed

sc failure RemoteRegistry reset= 3600 actions= restart/10000/restart/30000/restart/60000
if ERRORLEVEL 1 goto Failed

net start RemoteRegistry
net start RemoteRegistry
net start RemoteRegistry
net start RemoteRegistry
net start RemoteRegistry
net start RemoteRegistry
@echo Exited with %ErrorLevel%
if ERRORLEVEL 1 (
	if NOT %ErrorLevel% == 2 goto Failed
)


goto Success

:Failed 

echo ------ FALED! Try running again -------
goto End

:Success

echo.
echo.
echo ------ Success! ------
echo.

:End

exit 0