// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__ __BETA__ ---*/

using UnityEngine;
namespace HutongGames.PlayMaker.Actions
	{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get the number of items in an array")]
	public class ArrayCount : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;
		
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[Tooltip("Store the number of items in a variable.")]
		public FsmInt count;
		
		public override void Reset()
		{
			array = null;
			everyFrame = false;
			count = null;
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoGetCount();
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoGetCount();
			
		}
		
		private void DoGetCount()
		{
			count.Value = array.Length;
		}
	}
}
