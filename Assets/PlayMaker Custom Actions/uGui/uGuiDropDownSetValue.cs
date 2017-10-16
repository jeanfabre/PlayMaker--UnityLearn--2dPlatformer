// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

#if UNITY_5_3_OR_NEWER
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("set the value (zero based index) of the Dropdown uGui Component")]
	public class uGuiDropDownSetValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the DropDown ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The selected index of the dropdown (zero based index).")]
		public FsmInt value;


		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		UnityEngine.UI.Dropdown _dropDown;

		public override void Reset()
		{
			gameObject = null;
			value = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_dropDown = _go.GetComponent<UnityEngine.UI.Dropdown>();
			}


			SetValue ();

			if (!everyFrame)
			{
				Finish ();
			}
			
		}

		public override void OnUpdate()
		{
			SetValue ();
		}

		void SetValue()
		{
			if (_dropDown==null)
			{
				return;
			}

			if (_dropDown.value != value.Value)
			{
				_dropDown.value = value.Value;
			}
		}
	}
}
#endif