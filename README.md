# Match-Game

A Matching mini game 

Basic Documentation : 

Scripts:

Core:

1. CardManager.cs - it is the most important script and it handles all the core logic of checking matched cards, randomising cards at start of every game, checking win, communicating with UI and audio, calculating scores, tracking attempts, Restart logic and saving highscore.

2. Card.cs - it has all core methods associated with cards like clicking on a card(Main Input), flipping a card, animation methods like scaling and shaking cards, with its own reset logic.

3. Timer.cs - it is a simple timer script that can be started and stopped, which tracks time taken to complete a game.

UI:

1. AnimateButton.cs - A simple script to scale up and down animation for ui buttons during mouse enter/exit.

2. GameOverUIHandler.cs - This script is responsible for showing, hiding and animating GameOver UI Panel after a game is complete, and it also has methods to set and update score, highscore, timeTaken, Attempts texts that will be displayed on the GameOver Panel, with a method to load mainmenu scene for its button.

3. ProgressUIHandler.cs - It has methods to set and update timeTaken, attempts texts that will be displayed on InGame Progress Panel.

4. MainMenuHandler.cs - It has methods to load Level 1 and quitting game from mainmenu.

AUDIO :

1. AudioObject.cs - it is the audioObject that will be instantiated when playing any sfx from an audioObjectPool, it has methods to playSFX, and playBGM.

2. AudioManager.cs - It is a singleton script, that manages all audio in the game across scenes, it has an ObjectPool consists of audioObjects that can be reused which can save performance, it also has methods to playSFX and playBGM.

3. AudioPlayer.cs - A simple script that contains data for audioclips that can be played through audiomanager, where you can have the flexibility to modify audio parameters for each audio, it also has playAudio method.

