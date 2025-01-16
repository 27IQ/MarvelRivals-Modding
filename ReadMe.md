# MarvelRivalsPakManager

### Building:
build with dotnet8.0

    dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

### First steps:

    .\MarvelRivalsPakManager.exe generate "yourGameDir"

e.g. "C:\Program Files (x86)\Steam\steamapps\common\MarvelRivals"


### install.bat: 
installs the current mod config (the mods in the generated mod folder) into the game

### remove.bat: 
resets the game into an unmodded state