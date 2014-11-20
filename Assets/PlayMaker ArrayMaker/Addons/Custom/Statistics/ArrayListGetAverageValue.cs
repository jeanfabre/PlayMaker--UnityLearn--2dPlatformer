//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Return the average value within an arrayList. It can use float, int, vector2 and vector3 ( uses magnitude), rect ( uses surface), gameobject ( using bounding box volume), and string ( use lenght)")]
	public class ArrayListGetAverageValue : ArrayListActions
	{

		
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("Performs every frame. WARNING, it could be affecting performances.")]
		public bool everyframe;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The average Value")]
		public FsmFloat averageValue;
		
		List<float> _floats;
		
		public override void Reset()
		{
		
			gameObject = null;
			reference = null;
			
			averageValue = null;
			
			everyframe = true;
		}
		
		
		public override void OnEnter()
		{

			if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				Finish();
			}
			
			DoGetAverageValue();
			
			if (!everyframe)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoGetAverageValue();
		}
		
		void DoGetAverageValue()
		{
			
			if (! isProxyValid())
			{
				return;
			}
			
			_floats = new List<float>();

			foreach(object _obj in proxy.arrayList)
			{
				try{
					_floats.Add( System.Convert.ToSingle(_obj) );
			  	}finally
				{
					
				}
				
			}
			if (_floats.Count>0)
			{
				averageValue.Value = _floats.Aggregate((acc, cur) => acc + cur) / _floats.Count;
			}else{
				averageValue.Value = 0f;
			}
		}
		
	}
}