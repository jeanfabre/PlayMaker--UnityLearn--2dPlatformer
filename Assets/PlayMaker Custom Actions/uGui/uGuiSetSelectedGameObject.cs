// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// http://hutonggames.com/playmakerforum/index.php?topic=8452
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the EventSystem's currently select GameObject.")]
	public class uGuiSetSelectedGameObject : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject to select.")]
		public FsmGameObject gameObject;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
		    DoSetSelectedGameObject();
			
			Finish();	
		}
		
		void DoSetSelectedGameObject()
		{
			EventSystem.current.SetSelectedGameObject (gameObject.Value);
		}

	}
}
