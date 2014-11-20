// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get a Random value from a Fsm array")]
	public class ArrayGetRandom : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[MatchFieldType("array")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the value in a variable.")]
		public FsmVar storeValue;

		public override void Reset()
		{
			array = null;
			everyFrame = false;
			storeValue =null;
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoGetRandomValue();
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoGetRandomValue();
			
		}
		
		private void DoGetRandomValue()
		{
			if (storeValue.IsNone)
			{
				return;
			}

			storeValue.SetValue(array.Get(Random.Range(0,array.Length)));
		
		}

		
	}
}

