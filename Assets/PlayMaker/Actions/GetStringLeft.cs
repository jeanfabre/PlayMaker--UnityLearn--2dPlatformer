// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the Left n characters from a String Variable.")]
	public class GetStringLeft : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;
		public FsmInt charCount;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;
		public bool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			charCount = 0;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetStringLeft();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetStringLeft();
		}
		
		void DoGetStringLeft()
		{
			if (stringVariable == null) return;
			if (storeResult == null) return;
			
			storeResult.Value = stringVariable.Value.Substring(0, charCount.Value);
		}
		
	}
}