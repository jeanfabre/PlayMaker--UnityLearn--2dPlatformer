//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable
using System;

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Store all active GameObjects with a specific tag. Tags must be declared in the tag manager before using them")]
	public class ArrayListFindGameObjectsByTag : ArrayListActions
	{
	
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
	
		
		[Tooltip("the tag")]
		public FsmString tag;

		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			tag = null;
			
		}

		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				FindGOByTag();
			
			Finish();
		}

		
		public void FindGOByTag()
		{
			if (! isProxyValid()) 
				return;
			
			proxy.arrayList.Clear();
			
			GameObject[] list =  GameObject.FindGameObjectsWithTag (tag.Value);
			
			
			proxy.arrayList.InsertRange(0,list);
				
		
		}
	}
}