// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Force all canvases to update their content.\n" +
	         "Code that relies on up-to-date layout or content can call this method to ensure it before executing code that relies on it.")]
	public class uGuiCanvasForceUpdateCanvases: FsmStateAction
	{

		public override void OnEnter()
		{
			Canvas.ForceUpdateCanvases();

			Finish();
		}
	}
}