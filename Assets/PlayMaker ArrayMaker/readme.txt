(c) Jean Fabre, 2011-2014 All rights reserved.
This package is released under LGPL license: http://opensource.org/licenses/LGPL-3.0.
This content is released under the (http://opensource.org/licenses/MIT) MIT License.
http://www.fabrejean.net
contact: http://www.fabrejean.net/contact.htm

ArrayMaker Addon for PlayMaker. Version Alpha 0.99

changes from 0.98
-- MIT License Addition
-- Windows Phone compatibility work in progress, with extensions for ArrayList LastIndexOf and manual snapshots for lists copies and removal of commented lines giving a hard time to the compiler
-- fixed easySave addon HashTableEasyLoad action
-- fixed texture usage in EasySave sample to use it's own texture set to read write properly.

changes from 0.97
-- removed undo when adding proxies to selection, just not finding the right way to do a consistent behavior... and it created obsolete warnings on 4.x
-- added reset to iterative actions to make sure we have a way to force iterating back from the the first item if we exited the loop early.
-- added currentIndex in iterative actions to know where we are, not just to get the value itself.
-- added ArrayListIsEmpty and HashTableIsEmpty action
-- added ArrayListGetFarthestGameObjectInSight
-- added HashtableEditKey action
-- remove obsolete warnings on U4
-- added arrayList min max and average values custom actions to start some statistic actions around content.

changes from 0.96
-- added snapshots: prefilled data is stored and use RevertToSnapshot to get back to it, and TakeSnapShot to record the current state of things.

changes from 0.95
-- fixed prefill setup when downsizing the number of prefilled items.

changes from 0.94
-- added ArrayListSwapIndex

changes from 0.93
-- added ArrayListGetGameObjectMaxFsmFloatIndex, thanks for FlyingRobot: http://hutonggames.com/playmakerforum/index.php?topic=5116
-- added ArrayListGetClosestGameObjectInSight, thanks to FlyingRobot: http://hutonggames.com/playmakerforum/index.php?topic=5056
-- added ArrayListInsert, thanks to FlyingRobot: http://hutonggames.com/playmakerforum/index.php?topic=5146.0
-- added backEvent properties actions.

changes from 0.92

-- fixed vector2 support ( display of vector2 data in inspector)
-- added AudioClip support
-- added HashTableAddMany, HashTableSetMany, HashTableGetMany

changes from 0.91
-- Using now utomate to automate packaging
-- Packages now separated, just the framework, with samples and addons
-- Added addons for EasySave serialization
-- Added proper support for Vector2 in arrayList and HashTable inspector
-- fixed null values display in inspector for arrayList nad HashTable.

changes from 0.9
 WARNING: NOT COMPATIBLE WITH PREVIOUS VERSIONS: many actions public interface changed, so you will need to re-assign setters and getters on all of them.
 This version is a huge improvment in terms of ease of use, with now much more comprehensive and logical actions interfaces, no more lenghtly actions for nothing, 
 the latest playmaker version features a new FsmVar class that let the user select first the variable type, and then select it or feed some values, 
 I don't have to expose ALL possibilities anymore which was confusing, prone to error and frustrations and totally messy.
 
changes from 0.7
 WARNING: previous version had a different file organisation, so now it sits in its own folder and not within "PlayMaker" which was a mistake, 
 I assumed actions would only be detected if within the Actions folder...
-- goes to 0.9 because I can :) well I had a 0.8 version sort of, but so much went that I moved to 0.9
-- new actions
-- incorporate fixes for Texture and proceduralMaterial assignment
-- fixed prefab instance editing not serializing properly
-- fixed ArrayListCopyTo action

changes from 0.6
-- now using gamObject for reference instead of the component itself via fsmObject, since this doesn't bring any advantages ( as we still need a string reference...)


DESCRIPTION:
ArrayMaker Addon provide the ability for PlayMaker to work with ArrayLists and HashTables. 
ArrayLists and HashTables are defined as proxy components on GameObjects: They can be referenced within Fsm using a gameObject referemce, a string reference and the related custom actions.
A full set of Custom Actions is available to work with those ArrayLists and HashTables proxies. 
ArrayLists and HashTables proxies can be created at runtime within Fsm for convenience ( a set of Actions is available for this).

Features of ArrayLists and HashTables proxies: 
	* You can fill ArrayLists and Hashtables with content during authoring.
	* You can live preview and edit content of ArrayLists and Hashtables.
	* hashtable inspector check and visually warn for key duplicates.
	* during playback, you can narrow the preview given a start index and a row count, so if you have thousands of entrys, it is still manageable in the inspector.

NOTE: ArrayLists and HashTables proxy components can be used and accessed by normal scripts as well. 

More info on the PlayMaker forum dedicated section ( not yet available )


INSTALLATION:

***************** WARNING: PLAYMAKER NEEDS TO BE INSTALL. *******************
 You must own PlayMaker to use all the customs actions created
*****************************************************************************

 
To install ArrayMaker Addon for PlayMaker unpack the unitypackage.
To see sample scenes, import the included ArrayMakerSamples.unitypackage.	


KNOWN ISSUES:
	
		
TOASK ALEX:
	* Is it possible to gain access even via raw commandline to the help generator to document each actions on the wiki ( pretty please :) )
	* I used the same font for the "arrayMaker" logo. Is that acceptable? I am ok to change it of course, I just messed with it and had this idea since "a" "y" and "r was available in your logo :)
	
TOASK :
	* when getting an item from list, should it convert automatically and if possible to the type set in the get value?
		ie: arrayList[2] contains a int. If the arrayListGet is set to retrieve a string, should I convert? maybe have an explicit flag to allow for this?
	* Actions are implemented in a strict manner, that is HashTableAdd will not "set" instead if the key already exists. 
	* Some actions have events such as Success, Found, NotFound. I need a clear and consistent behavior here, but unsettled about this. for example: if I expose a NoFound event ( exmaple ArraListRemove), should I expose a Found event as well or simply rely on the built in Finish event? I tend to think the more the better ( FOUND & NOTFOUND) but actions starts to be crowded with optionnal stuff, this could be misleading for beginners.
	* should I use DEFINE in my proxies so that if the user doesn't have playmaker he can still benefit from my proxies ( cause they offers features not available with conventionnal arrays).
	
TOFIX:
	* broadcast events to fsm from wrappers. Is it possible without a Fsm reference? it should I think. -> will implement with the new api when available
	* how can I improve the hashTableActions and ArrayTableActions to avoid duplicate code just because the type is different, 
	the routine is identical to get and check for a component on a gameObject.
	* How PlayMaker LogError is working? I can't get anything, I have to use debug.LogError.
	
ROADMAP:
	* more collections type if required
	* more tools to build arrays during authoring and playback : add, insert, remove, remove at, and change type for each entry.
	* possible specialties for GUI listing and stuff ( like drop down menu)

Similar Addons down the line:
	* XML Parser -> REST?
	* json Parser ?
	* CSV Parser ( definitly, with headers etc etc)
	* rss parser?
	* database bridge?