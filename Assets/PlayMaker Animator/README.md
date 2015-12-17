(c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
This package is released under [LGPL 3.0 license](http://opensource.org/licenses/LGPL-3.0).

##PlayMaker Animator

This is the PlayMaker bridge for working with [Unity Animator](http://docs.unity3d.com/Manual/Animator.html) (also known as Mecanim).

###Documentation

Here is the link to the related [PlayMaker wiki page][1] 

###Support

**Forum**
Please use the [PlayMaker Forum](http://hutonggames.com/playmakerforum/index.php?board=1.0) to ask questions

###Installation
You have 3 options to install PlayMaker Animator in your Unity Project:  

- Wiki  
- Ecosystem  
- Github Submodule  

###Install from the Wiki
- Get on the [wiki page][1], and download the package suitable for your Unity Version 
- Import that package inside your Unity project 


###Install from the Ecosystem
The best/recommanded way to install PlayMaker Animator support in your Unity Project is to use the Ecosystem.

- Install the Ecosystem in your Unity Project
- In the Ecosystem Browser, search for "Mecanim"
- "get" the desired version of Mecanim: The browser will download and launch the package installer automatically.

###Install from the Github Submodule
The following install is for adventures developers, willing to integrate Animator support in their project via Github directly.

This folder is hosted on github as is, and acts as a [SubModule](http://git-scm.com/docs/git-submodule) to be used on other Git repositories Unity Projects.

To Include this submodule in your github project, do the following (using [SourceTree](https://www.sourcetreeapp.com/))


- Create/select a Unity Project that is using Github
- Add a new github "SubModule"
  - Point to the PlayMaker Animator submodule repository: https://jeanfabre@github.com/jeanfabre/PlayMaker--Utils.git
  - The local repository path must be **"Assets/PlayMaker Animator/"** (at the very least inside the Assets/ folder) 

Now, everytime you work on your project, you can pull these three submodules, and you'll get the latest.

[1]: https://hutonggames.fogbugz.com/default.asp?W1031