# SFTG-ThrowCalc

<p align="center">
  <a href="https://forthebadge.com">
    <img src="https://forthebadge.com/images/badges/made-with-c-sharp.svg">
  </a>
</p>
<p align="center">
  <a href="https://github.com/Mn0ky/SFTG-ThrowCalc/releases/latest">
    <img src="https://img.shields.io/github/downloads/Mn0ky/SFTG-ThrowCalc/total?label=Github%20downloads&logo=github">
  </a>
  <a href="https://opensource.org/licenses/MIT">
    <img src="https://img.shields.io/badge/MIT-blue.svg">
  </a>
</p>

<p align="center">
  <a href="">
    <img src="https://user-images.githubusercontent.com/67206766/164878782-5c3bddef-0c00-4561-bb88-fd3a00e42b03.gif">
  </a>
</p>

## Description

This mod allows for rendering the path a weapon will take when thrown. It is unable to be used in public lobbies; **one must directly join another mod user**. However, 
it is able to be used along with other mods such as the [QOL Mod](https://github.com/Mn0ky/QOL-Mod). The use cases are not meant to be entirely practical, perhaps a certain edge-case/scenario on a
map, or, maybe, you'd simply like to visually see the limit a weapon can be thrown. It was initially created as a demonstration, and has now evolved into a 
mod everyone can freely play around with and or reference if ever needed.

The trajectory will begin being drawn as soon as the weapon is picked up. Once said weapon is thrown the trajectory will not be updated until a weapon is again picked up. 
Trajectories are drawn, by default, for all players within a lobby. Pressing the <kbd>P</kbd> key will disable rendering trajectories from other players and will let you see
only your own. This is changeable in the config at: ``BepInEx\config\BepInEx.cfg``.

## Installation

To install the mod, one can follow the [same instructions](https://github.com/Mn0ky/QOL-Mod/#installation) as found on the QOL-Mod page and apply them here. 
**In addition to putting the ``ThrowCalc.dll`` into the ``BepInEx\Plugins`` folder, you must [download](https://github.com/Mn0ky/SFTG-SimpleAntiCheat/releases/download/v1.0.0/SimpleAntiCheat.dll) and put the ``SimpleAntiCheat.dll`` into the folder as well, 
<ins>or the mod will not run</ins>**.

## Math Summary
To be written.
