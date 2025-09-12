# Match-Game

LEVEL DESIGN INSTRUCTIONS :

1. This game contains one level for now, but each time we play the game, the cards are randomised, keeping the level fresh
2. Right now it has 16 cards, and maximum of 2 cards can be matched, but designers can expand this by minor tweaking without touching any code.

STEPS TO EXPAND LEVEL OR CREATE A NEW ONE :

1. Arrange all the slots(transforms) appropriately in scene, right now its 16 slots, but it can be expanded to your requirements.
2. Arrange all cards , by using the card prefab, and set its match id and match sprite same for all the same cards that can be matched, it can be a pair, or more than 2.
3. Make sure total number of cards is equal to total number of slots.
4. Now assign all the slots and all the cards in Slots and allCards field in cardManager.cs.
5. Set matchCount value in cardManager.cs based on your matching preference, right now its 2, so only 2 cards can be matched. But it can be set to 3 or 4,etc based on the level requirements and it will work fine as long as you arranged slots and cards properly.
6. (Optional) If u feel score values are bit higher or lower, you can adjust the scoreMultiplier value in cardManager.cs to get the desired result.

With these above steps you can easily create a whole new level or expand any level.
