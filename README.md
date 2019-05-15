# SameBrightnessSharp

Syncs your brightness settings between different battery states (i.e. battery and charging brightness)

Comes with a service version (SameBrightnessService.exe) and regular application version (SameBrightnessSharp.exe)

# Installation

I prefer to use the service as it's easier to have manual control over. However, use the application version if you don't have admin privileges.

## Application

This file has no interface (you have to use task manager to manually close the process) and can be run without installation but I suggest that you place it in your startup directory.

1. Find your startup folder. (Easiest way is to open windows explorer, click on the top bar and type in ```shell:startup```)
2. Place ```SameBrightnessSharp.exe``` into the folder.
3. Start the program manually or restart your computer.

## Service version (requires admin to install)

1. Open command prompt as admin
2. Navigate to the folder with the application with ```cd <directory path>```
3. Install the service with ```SameBrightnessService.exe install```
4. The service should automatically configure and start itself

# Usage (SameBrightnessService CLI)
	SameBrightnessService.exe command

- ```install```
	- Installs the service, automatically configures and starts itself

- ```uninstall```
	- Uninstalls the service automatically. (You have to close the process manually if uninstallation doesn't stop it)
