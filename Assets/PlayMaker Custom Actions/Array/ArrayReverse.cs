// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__ __BETA__ ---*/

using UnityEngine;

using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Reverse values from an arrays.")]
	public class ArrayReverse : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to sort.")]
		public FsmArray array;
		
		
		public override void Reset()
		{
			array = null;
		}
		
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			
			List<object> _list = new List<object>(array.Values);
			
			_list.Reverse();
			
			array.Values = _list.ToArray();
			
			Finish();
			
		}
		
		
	}
}