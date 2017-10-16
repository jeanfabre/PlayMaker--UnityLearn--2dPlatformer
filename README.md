# UnityLearn 2dPlatformer PlayMaker


This is a 100% PlayMaker port of [Unity](https://unity3d.com) [2d Platformer learning project](https://www.assetstore.unity3d.com/en/#!/content/11228) 


## Description

you need the following setup:

 - [Unity](https://unity3d.com) Compatible from 5.3.8 to 2017.2
 - [PlayMaker](https://www.assetstore.unity3d.com/en/#!/content/368) 1.8.5

### Download
 
 you can either clone this repository, or download the [Full Package directly](https://github.com/jeanfabre/PlayMaker--UnityLearn--2dPlatformer/blob/master/Packages/PlayMaker2dPlatformer.unitypackage).
 
Install tip: Import PlayMaker First then import the [package](https://github.com/jeanfabre/PlayMaker--UnityLearn--2dPlatformer/blob/master/Packages/PlayMaker2dPlatformer.unitypackage) it will prevent errors while PlayMaker is imported.  
 
### Improvements over the original version:
 
 - Leak with Enemies  
 The original script when enemies fall into the water has a memory leak: the enemy GameObject is not destroyed, leading a definite Memory Warning on Mobile devices and a crash.
 
 - No Pooling system  
 The original system creates and destroy rockets, enemies and background props, while it runs fine on most modern platforms, it is known that creating and destroying GameObject has a performance impact. This PlayMaker version offers a simple custom Pooling solution (WIP)
 
 - Bad Scene setup  
  The original version is not very clean in the way the hierarchy is organised, typically, objects are being created without proper parenting resulting in a very clunky root with constant creation and deletion of object and so it's near impossible to select a GameObject while the scene is running. The PlayMaker version parent all created object so that the initial hierarchy remains as is.
  
 - Old UI  
  This PlayMaker version uses the new Unity UI system, so this is an added value to this project as you can see how to integrate the new UI system in a proper project. Health Bar is following the Player, so this demonstrate important interaction between the 3d world and the UI Canvas.

## BenchMark
This port is published on many platforms and playable online. It serves as a comparison between a 100% PlayMaker solution and 100% scripted solution. You will find below benchmarks for the WebGL, web Player (deprecated), the Mac Application and the IOS apps build targets.

Test on Computers are done on an mid-2009 (!!!) mac book pro, so a very average configuration, far from game Spec standards...

### 100% PlayMaker WebGL
Test where done using Firefox, which gaves better result than with Chrome.

You can play this version [here](http://fabrejean.net/projects/PlayMaker/Platformer2D_PlayMaker/index.html)

- Average FPS: 56
- Memory Total: 5.95MB  
- Memory allocation: 4.01MB
- Build Size: 9.1MB

### 100% Scripted WebGL
You can play this version [here](http://fabrejean.net/projects/Unity/Platformer2D_Source/index.html)

- Average FPS: 58
- Memory Total: 4.95MB  
- Memory allocation: 3.24MB
- Build Size: 8MB


### 100% PlayMaker WebPlayer

You can play this version [here](http://htmlpreview.github.io/?https://github.com/jeanfabre/PlayMaker--UnityLearn--2dPlatformer/blob/master/Builds/PlayMakerVersion/PlayMakerVersion.html)

#### Stats:

- Average FPS: 52 -> 55
- Memory Total: 74MB  
- Memory allocation: 43MB
- Build Size: 4.67MB


### 100% Scripted WebPlayer

You can play this version [here](http://htmlpreview.github.io/?https://github.com/jeanfabre/PlayMaker--UnityLearn--2dPlatformer/blob/master/Builds/OriginalVersion/OriginalVersion.html)

#### Stats:

- Average FPS: 53 -> 58 
- Memory Total: 75MB  
- Memory allocation: 44MB
- Build Size: 5.00MB

### 100% PlayMaker Mac Application

#### Stats:

- Average FPS: 59
- Memory Total: 46MB  
- Memory allocation: 17MB
- Build Size: 58.5MB

### 100% Scripted Mac Application

#### Stats:

- Average FPS: 59
- Memory Total: 46MB  
- Memory allocation: 18MB
- Build Size: 60.2MB

### 100% PlayMaker IOS ( IOS 8.1 on iPhone 5S)
It's not really playable because of the Inputs and layout, but I made the test just to get some stats from Xcode

#### Stats:

- Average FPS: 29
- Memory Total: 10MB  
- Memory allocation: 9MB
- CPU usage: 21%-75%
- Memory : 79.6MB

### 100% Scripted IOS ( IOS 8.1 on iPhone 5S)
It's not really playable because of the Inputs and layout, but I made the test just to get some stats from Xcode

#### Stats:

- Average FPS: 29
- Memory Total: 10MB  
- Memory allocation: 10MB
- CPU usage: 12%-74%
- Memory : 61.9MB

**Notes:**

Overall, this is very interesting to see that the pure PlayMaker solution doesn't expose any issues or noticeable downside on playability and performances, a 1 or 2 frame difference for the FPS isn't really an issue given that this will vary very much based on the client computer performances. If you have other stats numbers, please share so we can get a sense of the variation across different computers. Noticeable, Memory allocation from Xcode debugging shows 10MB more for PlayMaker. It should be expected that more memory is allocated when using big frameWork like PlayMaker.

#### TODOS:
- The background parallax system needs porting to PlayMaker. 
- Add Touch Support. 
- Make a Networked version using Photon. 
