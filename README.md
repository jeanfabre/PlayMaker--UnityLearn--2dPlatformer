UnityLearn--2dPlatformer--PlayMaker
===================================

This is a 100% PlayMaker port of Unity 2d Platformer learning project 

This project is a working in progress:

you need the followoing setup:

 Unity 4.6.4f1
 PlayMaker 1.8.0f31
 
 A Branch of this repository is available with PlayMaker Preview, which means you don't need to own PlayMaker, the preview version is a fully working PlayMaker system, but you can't edit anything.
 
 Improvments over the original version:
 
 -- Leak with Ennemies
 The original script when ennemies fall into the water has a memory leak: the enemy GameObject is not destroyed, leading a definite Memory Warning on Mobile devices and a crash.
 
 -- No Pooling system
 The original system creates and destroy rockets, ennemies and background props, while it runs fine on most modern platforms, it is known that creating and destroying GameObject has a performance impact. This PlayMaker version offers a simple custom Pooling solution (WIP)
 
 -- Bad Scene setup
  The original version is not very clean in the way the hierarchy is organized, typically, objects are being created without proper parenting resulting in a very clunky root with constant creation and deletion of object and so it's near impossibel to select a gameobject while the scene is running. The PlayMaker version parent all created object so that the initial hierarchy remains as is.
  
 -- Old UI
  This PlayMaker version uses the new Unity UI system, so this is an added value to this project as you can see how to integrate the new UI system in a proper project.
    -- Health Bar is following the Player, so this demonstrate important interaction between the 3d world and the UI Canvas.

TODOS:
-- The background parallax system needs porting to PlayMaker
-- Add Touch Support
-- Test on different Platform
-- Make a Networked version using Photon
