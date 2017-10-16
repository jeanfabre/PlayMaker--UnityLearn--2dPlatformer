// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the isOn property of a UGui Toggle component.")]
	public class uGuiToggleSetIsOn : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Toggle))]
		[Tooltip("The GameObject with the Toggle UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Should the toggle be on?")]
		public FsmBool isOn;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.Toggle _toggle;

		bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			isOn = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_toggle = _go.GetComponent<UnityEngine.UI.Toggle>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _toggle.isOn;
			}

			DoSetValue();

			Finish();

		}

		void DoSetValue()
		{

			if (_toggle!=null)
			{
				_toggle.isOn = isOn.Value;
			}
		}

		public override void OnExit()
		{
			if (_toggle==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_toggle.isOn = _originalValue;
			}
		}
	}
}