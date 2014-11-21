// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the Right n characters from a String.")]
	public class GetStringRight : FsmStateAction
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
			DoGetStringRight();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetStringRight();
		}
		
		void DoGetStringRight()
		{
			if (stringVariable == null) return;
			if (storeResult == null) return;
			
			string text = stringVariable.Value;
			storeResult.Value = text.Substring(text.Length - charCount.Value, charCount.Value);
		}
		
	}
}