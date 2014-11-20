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
	[Tooltip("Store a mesh vertex colors into an arrayList")]
	public class ArrayListGetVertexColors : ArrayListActions
	{
	
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		
		[ActionSection("Source")]
		
		[Tooltip("the GameObject to get the mesh from")]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmGameObject mesh;

		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			mesh = null;
			
		}

		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				getVertexColors();
			
			Finish();
		}

		
		public void getVertexColors()
		{
			if (! isProxyValid()) 
				return;
			
			proxy.arrayList.Clear();
			
			
			GameObject _go = mesh.Value;
			if (_go==null)
			{
				return;
			}
			
			
			MeshFilter _mesh = _go.GetComponent<MeshFilter>();
			if (_mesh ==null)
			{
				return;
			}
		
			
		 
			
			proxy.arrayList.InsertRange(0,_mesh.mesh.colors);
				
		
		}
	}
}