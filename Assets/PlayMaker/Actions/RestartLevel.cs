// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// micro script by Andrew Raphael Lukasik

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Restarts current level.")]
	public class RestartLevel : FsmStateAction
	{
	    public override void OnEnter()
	    {
		    Application.LoadLevel(Application.loadedLevelName);
	        Finish();
	    }
	}
}