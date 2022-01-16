# SpinSearch

[![Build](https://github.com/SRXDModdingGroup/SpinSearch/actions/workflows/automated-build.yaml/badge.svg)](https://github.com/SRXDModdingGroup/SpinSearch/actions/workflows/automated-build.yaml)

Do you have an unhealthy number of charts, and you don't want to scroll through them all to get to the one you want to play?  Or do you only want to play charts from a certain artist, or charter?

Introducing the solution you've been waiting for: :sparkles:_**SpinSearch**_:sparkles:!

(_its a search bar_)

## Requirements

This mod requires BepInEx Mono to function.

The _recommended_ method of installation is detailed in the [SRXDBepInExInstaller](https://github.com/SRXDModdingGroup/SRXDBepInExInstaller) repository.

Alternatively you can install it manually:
- Download and install the latest release of [BepInEx](https://github.com/BepInEx/BepInEx/releases/latest).
- Rename `Spin Rhythm/UnityPlayer.dll` to `Spin Rhythm/UnityPlayer.dll.bak`.
- Copy `Spin Rhythm/UnityPlayer_Mono.dll` to `Spin Rhythm/UnityPlayer.dll`.

## Installation

1. Download _both_ `SRXD.SpinSearch.Plugin.dll` & `SRXD.SpinSearch.Patcher.dll` from the [latest release](https://github.com/SRXDModdingGroup/SRXDBepInExInstaller/releases/latest)
2. Place the `SRXD.SpinSearch.Plugin.dll` file in the `Spin Rhythm/BepInEx/plugins` directory
3. Place the `SRXD.SpinSearch.Patcher.dll` file in the `Spin Rhythm/BepInEx/patcher` directory

Please report any issues or bugs on the [issues page](https://github.com/SRXDModdingGroup/SpinSearch/issues).

## Known Bugs

- Sometimes after clicking into the textbox moving the mouse will also scroll through tracks

## TODOs

- [ ] In Normal mode if nothing matches display a placeholder
- [ ] Make filtering stay at the same position
- [ ] Prefer first found track over `Random` track
- [ ] Allow for more advanced queries
- [ ] Customization of parameters like bar position through config

## Building

Clone the repo and put the referenced dlls (`Assembly-CSharp.dll` and `SDD.Game.dll`) from the game directory into the `lib/` folder. Then run `dotnet build -c Release -o bin` to create a directory `bin/` containing the two output dlls.
