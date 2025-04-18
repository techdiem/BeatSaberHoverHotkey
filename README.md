# Beat Saber Hover Hotkey
Displays a hovering hotkey in-game that presses a keyboard button.
It uses similar code for the gui as my MicMuter mod, but I thought it wouldn't fit into this mod, as ist allows broader applications besides sound.

Compatible with **Beat Saber 1.40.3**

## Installation
* Required dependencies (can be installed using Mod Assistant)
    * BeatSaberMarkupLanguage v1.12.2+
    * BS_Utils v1.14.1+
* Mod installation
    * Download the latest DLL from the [Releases](https://github.com/techdiem/BeatSaberHoverHotkey/releases/latest) page and copy it into your Plugins folder.
    * You should see a HoverHotkeys entry in the Mod Settings menu when installed.

## Configuration

1) Launch Beat Saber and exit it again (to generate config)
2) Open <your beat saber dir>/UserData/HoverHotkey.json
3) Choose Button text
4) Configure keyboard key:
	- Pick keycode from this page: [Virtual Key Codes](https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes)
	- Take the characters after 0x and convert them to decimal: [online converter](https://www.binaryhexconverter.com/hex-to-decimal-converter)
	- Insert the result in the config as Hotkey
	- **Example:** F2 Key: 0x71 -> put 71 in converter -> 113 in config
5) Configure modifier key, available modifiers:
    - none (just press the configured key without any modifiers, default)
    - alt
    - ctrl
    - shift
6) Set the configured key in the software you want to control, for example as deafen hotkey in discord depending on your use case
7) Start Beat Saber and move the button in the Mod Settings ingame to your desired position

## Screenshot
![](button_screenshot.jpg)