# LibraryCleaner
Configurable Library folder cleaner for Unity

**RECOMMENDED INSTALLATION**
Add via the Unity Package Manager
"Add package from git URL..."
https://github.com/ryanslikesocool/Easings.git
Add

**Not-so Recommended Installation**
Get the latest release
Open with the desired Unity project
Import into the Plugins folder

The editor must be restarted after installing for the Library folder to be cleaned.

## Usage
Select `Tools/ifelse/LibraryCleaner Manager` from the menu bar to open the manager window.
Pick the settings scriptable object labeled "CleanerSettings".  There should only be one in a project.
Set the desired settings.  The Library folder will be cleaned accordingly when the editor is quit.

- Clean Condition
  - Always: The Library folder will be cleaned every time the editor quits.
  - Threshold: The Library folder will be cleaned only when it takes up the defined amount of space.
    - Clean Threshold: Set a float amount and storage unit.

**The following options apply to cleaning only.  Nothing will be cleaned if the above conditions are not met**
- Delete Everything: All of the Library folder's contents will be deleted.  The editor will take a while to load next time the project is opened.
- Delete Package Cache: The package cache will be deleted.  You will need an internet connection the next time you open the project to redownload packages.  This may take a while.
- Delete Script Assemblies: Script assemblies will be deleted.  The editor may take a while to load next time.
- Delete Shader Cache: The shader cache will be deleted.  This should not affect project load time much, but shaders will likely be magenta or cyan for a few frames before compiling the next time the project is loaded.
- Delete Asset Database: Assets will need to be reimported the next time the project loads.  None of your import settings will be lost.  This may take a while.
- Delete Additional Folders: Delete the contents of any folders in the list.  Add paths relative to the Library folder.  Useful for IL2CPP build caches.
- Delete Additional Files: Delete files in the list.  Add paths relative to the Library folder.