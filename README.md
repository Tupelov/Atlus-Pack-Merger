# Atlus Pack Merger
A program created by Tupelov for the purpose of merging atlus file types. Credit to TGE for making PackTools https://github.com/TGEnigma/AtlusFileSystemLibrary and DniweTamp for making the batch extract and batch pack batch scripts

If you need to unpack any mods just use the extract_tools folder. Place your file in unpacked then run batch extract with powershell

Install:
Download https://github.com/TGEnigma/AtlusFileSystemLibrary, and put Packtools.exe and the dll file inside of the packmerger directory>
(If you need to use extract_tools then you also need to put the exe and dll there)
 
Instructions:

1:Unpack a fresh copy of your folder and put it under the correct directory in original ie init_free.bin -> ORIGINAL/BIN/init_free
2: Unpack all the mods you want to merge and put them in the modded folder in the order you want to merger ie MODDED/BIN/init_free/modOne
3:The merger will merge from top down making the mods with names that start first being the ones that have priority over conflicting mods
4:Run Bin Merger.exe if the command window remians blank, Close the window and try again
5: Specify the version(v1 should work for p4g)
6:Check your ORIGINAL folder for the new packed folder ie ORIGINAL/BIN/init_free.bin
