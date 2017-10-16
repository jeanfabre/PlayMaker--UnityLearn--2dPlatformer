// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

#if UNITY_5_3_OR_NEWER
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("get the value (zero based index) sprite and text  the Dropdown uGui Component")]
	public class uGuiDropDownGetSelectedData : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the DropDown ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The selected index of the dropdown (zero based index).")]
		[UIHint(UIHint.Variable)]
		public FsmInt value;

		[Tooltip("The selected text.")]
		[UIHint(UIHint.Variable)]
		public FsmString text;

		[ObjectType(typeof(Sprite))]
		[UIHint(UIHint.Variable)]
		public FsmObject image;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		UnityEngine.UI.Dropdown _dropDown;
		List<UnityEngine.UI.Dropdown.OptionData> _options;

		public override void Reset()
		{
			gameObject = null;
			value = null;
			text = null;
			image = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_dropDown = _go.GetComponent<UnityEngine.UI.Dropdown>();
			}


			GetValue ();

			if (!everyFrame)
			{
				Finish ();
			}
			
		}

		public override void OnUpdate()
		{
			GetValue ();
		}

		void GetValue()
		{
			if (_dropDown==null)
			{
				return;
			}

			if (!value.IsNone)
			{
				value.Value = _dropDown.value;
			}

			if (!text.IsNone )
			{
				text.Value = _dropDown.options [_dropDown.value].text;
			}

			if (!image.IsNone )
			{
				image.Value = _dropDown.options [_dropDown.value].image;
			}
		}
	}
}
#endif