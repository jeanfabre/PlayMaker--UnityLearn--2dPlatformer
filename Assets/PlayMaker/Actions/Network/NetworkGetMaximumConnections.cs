// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

#if !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
	[Tooltip("Get the maximum amount of connections/players allowed.")]
	public class NetworkGetMaximumConnections : FsmStateAction
	{
		[Tooltip("Get the maximum amount of connections/players allowed.")]
		[UIHint(UIHint.Variable)]
		public FsmInt result;		

		public override void Reset()
		{
			result = null;
		}

		public override void OnEnter()
		{
			result.Value = Network.maxConnections;
			
			Finish();
		}

	}
}

#endif