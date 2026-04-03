@echo off
setlocal

set NAME=ª§’’πÒ≤¢––≤‚ ‘Ω≈±æ
set OUTPUT_DIR=%1
set VERSION=%2

set ZIP_NAME=%NAME% v%VERSION%.zip
set ZIP_PATH=%OUTPUT_DIR%\%ZIP_NAME%

echo Packaging %ZIP_NAME%

REM …æ≥˝æ… zip
for %%f in ("%OUTPUT_DIR%\%NAME%*.zip") do (
    if /I not "%%~nxf"=="%ZIP_NAME%" (
        del "%%f"
    )
)

powershell -Command ^
"Compress-Archive -Path '%OUTPUT_DIR%\*' -DestinationPath '%ZIP_PATH%' -Force"

endlocal