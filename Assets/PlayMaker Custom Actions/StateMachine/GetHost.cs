// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the fsm name and or component that hosts this FSM when ran as a sub Fsm")]
	public class GetHost : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString hostName;

		public override void Reset()
		{
			hostName = null;
		}
		
		public override void OnEnter()
		{
			hostName.Value = Fsm.Host.Name;
			
			Finish();
		}
	}
}