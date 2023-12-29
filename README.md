# motion3fix
a small utility to fix model3.json (Live2D) files for web applications like SillyTavern

# What does it fix?
At the moment it fixes currupted motion3.json (aniomation) files of live2D models (like mutiple hands, eyes, brown or arms that are stuck behind the mainbody)

# What dies it not fix (for now)
If a model has multiple body parts from the get go (meaning, even outside of animations) this fix will not work. 
I try to figure out how to make a generaly fix for that but in the end this animations have to be fixed manually to get the best quality.

# How to use
For users it is recomented to use the executable provited.

Place the exe inside the folder of the model you want to fix and start it. (mode 1)
OR
Place the exe inside the root Folder of all Models you want do fix. (mode2)
Follow the instructions of the programm. (Yes, that is all)

# Known Issues
in Mode 2 (bulk) it is nessesary that ALL folders containing models with .moc3 files otherwise the application will detect this as a error and will shut down.
(This will be fixed in a further version)

