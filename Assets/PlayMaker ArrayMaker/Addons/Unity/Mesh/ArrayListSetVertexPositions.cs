//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

// credits: action created by LampRabbit: http://hutonggames.com/playmakerforum/index.php?topic=3982.msg18550#msg18550

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Set mesh vertex positions based on vector3 found in an arrayList")]
	public class ArrayListSetVertexPositions : ArrayListActions
	{
	
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		
		[ActionSection("Target")]
		
		[Tooltip("The GameObject to set the mesh vertex positions to")]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmGameObject mesh;

		public bool everyFrame;
		
		
		private Mesh _mesh;
		private Vector3[] _vertices;
		
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
				SetVertexPositions();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			SetVertexPositions();
		}
		
		public void SetVertexPositions()
		{
			if (! isProxyValid()) 
				return;
			
			_vertices = new Vector3[proxy.arrayList.Count];
			
			int i= 0;
			foreach(Vector3 _pos in proxy.arrayList)
			{
				_vertices[i] = _pos;
				i++;
			}
			
			_mesh.vertices = _vertices;
				
		
		}
		
	}
}