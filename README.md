# Augmemory  
__A AR memory game__

![scene](https://user-images.githubusercontent.com/28506721/59743024-5402b700-926f-11e9-833f-c6da639dd1e3.png)

Our application is a ready-to-play memory game for all ages but our main target audience is children between 6-12-year-olds. Like its physical counterpart, our memory game increases short term memory, enhances the visual distinction of objects, improves creativity and is a good brain exercise. We also offer the possibility to add text to the memory cards in order to make this game viable for educational purposes, e.g. learning a new language through connecting a word with an object/picture.  

# Build instructions
**Prerequisites:**    
Unity 2017.4.29f including Vuforia and Windows Universal Platform build support
Microsoft Visual Studio 2017+ with installed component for the development of Windows Universal Platform. Check that the Windows 10 SDK 10.0.17134 will get installed too.

To create a build for this project pull the github repository and open the including unity project with the unity version 2017.4.29f. Y. After unity has opened the project go under file > Build Settings and check if the universal windows platform is selected.

Make following Selection:

* Target Device: HoloLens
* Build Type: D3D
* SDK: 10.0.17134.0
* Visual Studio Version : Visual Studio 2017
* Unity C# Projects: enabled
  
Now build the project.

# End-User Guide
**Starting the game:**  
At the moment the game starts after you start the application.

**Playing the game:**  
In the middle of the screen you will see a field with grey blocks. You can interact with these blocks by hover over one with the white selection circle you see. If you are over one block make the click gesture in front of the hololens. Then you will see how the front of the block will switch to a model with a text. Now your goal is to click on a second block. If you get the same model. Both selected block will disappear. If their models are different the block will go into their initial state and are clickable again. The goal is to make that there is no block left.

**Restart the game:**  
Click the restart block on the left side of the playfield.

**When the game is finished**  
A menu will appear where the playfield was. There you can select between different options like start a new game or go back.

