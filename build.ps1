cd "$PSScriptRoot" 


remove-item .\build\ -Recurse -Force
mkdir .\build


copy-item .\fox\signalrsamples.exe .\build\
copy-item .\fox\signalrsamples.exe.config .\build\
copy-item .\fox\config.fpw .\build\
copy-item .\fox\*.dll .\build\

copy-item   .\fox\html\ .\build\ -Recurse 
remove-item .\build\html\node_modules -Recurse -Force

& ".\signtool.exe" sign /v /n "West Wind Technologies" /sm /s MY /tr "http://timestamp.digicert.com" /td SHA256 /fd SHA256 ".\build\signalrsamples.exe"

7z a -tzip -r ".\build\signalrsamples.zip" ".\build\*.*"
