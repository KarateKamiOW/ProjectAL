# ProjectAL
Testing For Project AL
/*What this is:

Project Alien Life is an upcoming game made entirely by me.
Started 1/15/2021
The game is about living the life of an alien, as you probably guessed.
It's a small rogue-like that gives players the freedom of choice.
Spawn in as a random alien species equipped with their own special perk.
And survive the harsh, weird, and chaotic variables.
Targetting a late Summer release.

Music will be done by me.
As well as art
And the animations.
Art-> Pixel Art, done in Aseprite
The Code here, is everything so far done.
Code done in C# using Unity Game Engine

# 5/10
BACK ONCE AGAIN
-Been about a month since the last update but the work hasn't stopped!

-Although I've been a bit busy in preparing for my graduation, theres been a TON of changes since the last update.

-Improved game feel by adding screen shake in various other places such as shooting the gun.

-Added new shops that'll allow the players to buy items to boost their stats.

-Speaking of buying, there was no way the player could actually acquire money prior to a few weeks ago; that has changed.

-Player can now sell their unwanted items at the local 'Dump' which'll give money back to the player to spend!

-Guns can now be swapped. Players can hold up to 2 weapons at a time.

-Added explosion barrels. If the player(or an enemy) does enough damage to it, it'll explode!

-Random NPCs. Random NPCs now walk around and will say a random line when player interacts with them.

-This will be built upon more in the future to give players hints, tricks, and lore about the game!

-The Gachabog! Give your hard earned money to the Gachabog and she'll drop a random gun based on the selected tier.(3 tiers total)

-Planning on added dozens of guns, each with their own line of variants, amounting to (hopefully) well over 100 guns to play with.

-Enemy Spawner. A simple script that'll allow enemies to spawn randomly at a given location. Useful down the line.

-Added a squash and stretch effect to the player for improved Game feel.

-Skills! This one is much more recent(yesterday :P), but the basic framework for skills have been added! 

-Players will be able to equip up to 2 skills at a time,(and will even be able to upgrade them in the future).

-For now, the only available skill is called "Accelerate I" and will boost player speed for some seconds, before returning to normal.

-Within the week, I'll be looking to add much more interesting skills such as "Grenade Toss" and "Dash" (Placeholder names).

-Improved Dust particle effect.


June 1st marks the one year anniversary since I've began programming games. Although I don't have a game out yet, I've learned an absolute TON 
since I've began, and will be looking at finally revealing the game around that date.
Within the next few weeks I'll be mainly focused on.

-Improving Art some more.

-Adding Skills

-More enemy types

-More Weapons(The sprites for over 50 weapons are complete, and just need to be added now)

-More Shops and NPCs

-Quests

-Getting better at Github :)



//-------------------------------------------------------------------------------------------------------------------------------
# 4/13
I'm baaaaack. Ton of changes since the last posted update!
-Art has been improved across the board, with more improvements planned. By next month, the game will be ready to be revealed.
-The next steps will be looking towards its promotion and possibly open a kickstarter, which brings me to the next point.
-The next few months may be tricky. I need a steadier stream of income, and may need a job in the meantime. 
-This'll mean slower progress, however, worst case scenario, im still planning on releasing this game this year.
-I do not intend on spending years on this project.
-So with all this in mind, I'm also planning on not uploading future scripts or locking them for selective viewers. 
-The scripts that are uploaded will remain, this was initially just planned to show employers by semi-bi-weekly updates.




Back to the game updates!
-Muzzle flash has been added to the guns.

-Weapon Dialog! This is an interesting add on to the game but uhhhhh, periodically guns can talk to you. And you can even respond to them!

-SO you may be asking, why?... Because it's funny!

-But seriously, there's currently around 10 different personalities. Each personality will have dozens of different dialog.

-Depending on how you respond to your gun, their stats may increase slightly. Guns can roll with different personalities.

-So even if you play the game 3 times with the same type gun, you may roll a different personality with it, changing its stats uniquely.

Cool.
-Added bullet trails for better looking bullet styles.

-Finished tiles for different biomes.

-Added crosshairs for better visibility.

-Added Basic NPCs!

-These npcs simply walk around the world and say something if the player interacts with them.

-In the future, these npcs will be able to hand out repeatable quests.

-Additionally, added outlines to NPCs. It needs more tuning, but when the player stands near, or hovers their mouse over passive npcs,
they will have a green outline. In the future, enemies and other objects will have colored outlines as well.

-And other small changes! 



The next time I post here, the game will be verrryy close to reveal. 
Next on the agenda...

  -Damage text when hitting enemies
  -Improve enemy behavior
  -Add mods to guns
  -More Npcs
  -Quests
  -Intoduce the year system. Long story short, the game revolves around this system. All actions will add to a year counter (1/100). 
  When you reach 100/100, the year will end, increment, and start again at (1/100). You only have 10 years!

//---------------------------------------------------------------------------------------------------------------------
# 3/22

Been a little over a week but I've added some big additions to the game!

-Added a 'WhiteFlash' Material. Now when enemies are hit, they flash for a split second
This is for imporved game feel and better readability.

-Added portals. When the player enters the portal, they'll be transported to a new scene.
This is big for expanding the size of the game. I can now easily add additional maps to the game.

-Added a fully functional inventory system.

As well as items properly upadting when added to the inventory, players can also use consumables.
Consumables offer distinctive advantages (and disaadvantages) and incentivizes the player to collect.
These two additions are huge for expanding the game, and encompass a large part of the rest of the game.





//------------------------------------
3/11
- Fixed camera variables to better follow the player.
- Added 12 new items to the game. When used, they will boost stats. 'Use' function not added yet.
- Reworked enemy movement scripts. I had trouble with Raycasts, so I'm ditching that method for another.
- More will be added to the future. For now, enemies roam around and chase when you're within their 'Attack Range'
- Increased Bullet Size and added a slight glow; for better readability.





//------------------------------------
As of 3/9

2 new Npc's added.
6 Misc. items added. These are items that serve no direct purpose(such as recovering health or boosting stats), but may serve as crafting pieces later.
These misc. items drop randomly from monsters.
Additionaly, also added 'Skill Crystals'. These also drop randomly from monsters, and are needed to unlock skills in the future.
The 'Skill Unlock Vendors/NPCs" are associated with the two newly added NPCs.










//------------------------------------

As of 3/5 Six Alien species are available to the players. Perks are not in.

The scripts for dialog are completed. Using scriptable objects, I can easily create dialog entries for Npcs. Furthermore, I also added a response system for the player. With this, the player can choose between different dialog responses, resulting in different passive stat changes.

Player stats are randomly given on entry. Make do with what ya got.

Speed not yet affecting actual movement speed. I need to think of a decent modular formula.

Item scripts have been created as well as an inventory system. Items can be added to the player inventory easily, and it will show within the editor; however, I haven't drawn out the art for the inventory, so it does not show in game.

Enemy scripts have been created. I can now easily add enemies within the game; give it health, damage to the player, as well as items for it to drop. Items can be dropped with a percent chance based on the values I give in the editor. Nice.  

The distance that the enemy aggros the player, as well as the speed at which the enemy moves and chases the player, are both based on the values I give in the editor. 

Enemy attacks the player, buuut the movement scripts need more work, the enemy is chasing the player off of edges.

Npcs are in the game, however only about 8 are currently in.

Shop system script is added as well. As of now, it is not active on any Npc, however, I can easily create inventories for an Npc to "hold", and on command, allow the player to open and buy items within the inventory. They will be added to the player inventory, and your money counter will be reduced based on the value I give in the editor. Nice.



Next on the Agenda------
//Create a script that saves player data and allow player to transfer to different scenes. From here, I can create "scenario" systems that'll throw players into different scenes based on dialog choice.

//Add more enemies.(currently only 2)
//Add more Npcs.
//Tidy up shop system, allowing full player to npc control.
//Create s script that allows items to be "used", to give items different functions.
//More ART
//Add lighting system and sound.
















