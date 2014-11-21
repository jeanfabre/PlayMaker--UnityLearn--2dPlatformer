// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

#if !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
	[Tooltip("Get the current network time (seconds).")]
	public class NetworkGetTime : FsmStateAction
	{		
		[Tooltip("The network time.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat time;

		public override void Reset()
		{
			time = null;
		}

		public override void OnEnter()
		{
			// TODO: support double somehow because this can not work properly.
			time.Value = (float)Network.time;
				
			Finish();
		}

	}
}

#endif