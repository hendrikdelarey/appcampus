taskkill /F /IM AppCampus.Signboard.exe

ping 127.0.0.1 -n 3 > nul

start "AppCampus Software Update" %1

