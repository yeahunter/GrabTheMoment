@echo off

if exist "C:\Program Files\Git\bin\git.exe" (
    set GIT="C:\Program Files\Git\bin\git.exe"
) else (
    if exist "C:\Program Files (x86)\Git\bin\git.exe" (
        set GIT="C:\Program Files (x86)\Git\bin\git.exe"
    ) else (
        if exist "git" (
            set GIT="git"
        )
    )
)

if defined GIT (
    %GIT% describe --long --dirty --always
) else (
    echo -
)
