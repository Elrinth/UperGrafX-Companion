# UperGrafX Companion

UperGrafX is a HDMI solution with cd image and .pce rom loading for the
Pc-Engine. You can find more information about the unit at the webpage here:
http://www.upergrafx.com/home_en (this page is dead as of 2023, however the howto page is still up here: https://howtouse.upergrafx.com/)

Functions / Features of UperGrafX Companion:

- Allows you to fix your library of .pce-files to work with the UperGrafX.
- Will notify you which of your .pce-files the UperGrafX won't work with (files larger than 512KiB won't work because of ram restrictions in the hardware design).
- Trim header of pce files to work with UperGrafX
- Convert region from J -> U or U -> J to work with your system.

- Allows you to convert .ccd-cdimages to .cdm-cdimages which work with
  UperGrafX.

- If you have daemon tools installed, this program can now convert .bin/.cue cd
  images to .cdm/.cue - This is a long progress and uses a modified version of
  the external program CD Manipulator. (https://github.com/Elrinth/CD-Manipulator)

- Can fire up / Focus "UperGrafx control panel" directly from the program.

- To read the actual finished .cdm images with the UperGrafX you need either a
  System Card 3 .pce file or an actual physical SystemCard 3 or Arcade Card
  inserted into your console.

- Obviously this tool won't fix 100% of the pce files! As some, for example
  public domain games, like the Zelda port won't work. I'm not sure why, but
  graphics would just appear corrupt for me. Maybe someone else has a clue? My
  guess is that this program removed 512 bytes of necessary data instead of
  header data :)

![Image of UperGrafX Companion](https://raw.githubusercontent.com/Elrinth/UperGrafX-Companion/main/screenshot_of_this_program_v1.png)
