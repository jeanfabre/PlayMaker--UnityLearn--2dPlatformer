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
	[Tooltip("Set a mesh vertex colors based on colors found in an arrayList")]
	public class ArrayListSetVertexColors : ArrayListActions
	{
	
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		
		[ActionSection("Target")]
		
		[Tooltip("The GameObject to set the mesh colors to")]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmGameObject mesh;

		public bool everyFrame;
		
		
		private Mesh _mesh;
		private Color[] _colors;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			mesh = null;
			everyFrame = false;
		}

		
		public override void OnEnter()
		{
			GameObject _go = mesh.Value;
			if (_go==null)
			{
				Finish();
				return;
			}
			
			
			MeshFilter _meshFilter = _go.GetComponent<MeshFilter>();
			if (_meshFilter ==null)
			{
				Finish();
				return;
			}
			
			_mesh = _meshFilter.mesh;
			
			
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				SetVertexColors();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			SetVertexColors();
		}
		
		public void SetVertexColors()
		{
			if (! isProxyValid()) 
				return;
			
			_colors = new Color[proxy.arrayList.Count];
			int i= 0;
			foreach(Color _col in proxy.arrayList)
			{
				_colors[i] = _col;
				i++;
			}
			_mesh.colors = _colors;
				
		
		}
	}
}