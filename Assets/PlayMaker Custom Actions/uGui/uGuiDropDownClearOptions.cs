// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

#if UNITY_5_3_OR_NEWER
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Clear options to the options of the Dropdown uGui Component")]
	public class uGuiDropDownClearOptions : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the DropDown ui component.")]
		public FsmOwnerDefault gameObject;


		UnityEngine.UI.Dropdown _dropDown;

		public override void Reset()
		{
			gameObject = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_dropDown = _go.GetComponent<UnityEngine.UI.Dropdown>();
			}


			if (_dropDown!=null)
			{
				_dropDown.ClearOptions ();
			}
			
			Finish();
			
		}

	}
}
#endif