//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Get the current resolution")]
	public class GetCurrentResolution : FsmStateAction
	{
		[Tooltip("The current resolution width")]
		[UIHint(UIHint.Variable)]
		public FsmFloat width;
		
		[Tooltip("The current resolution height")]
		[UIHint(UIHint.Variable)]
		public FsmFloat height;
		
		[Tooltip("The current resolution refrehs rate")]
		[UIHint(UIHint.Variable)]
		public FsmFloat refreshRate;
		
		[Tooltip("The current resolution ( width, height, refreshRate )")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 currentResolution;
		
		public override void Reset()
		{
			width = null;
			height = null;
			refreshRate = null;
			currentResolution = null;
		}

		public override void OnEnter()
		{
		
			width.Value = Screen.currentResolution.width;
			height.Value = Screen.currentResolution.height;
			refreshRate.Value = Screen.currentResolution.refreshRate;
			
			currentResolution.Value = new Vector3(Screen.currentResolution.width,Screen.currentResolution.height,Screen.currentResolution.refreshRate);
			
			Finish();
		}
		

	}
}