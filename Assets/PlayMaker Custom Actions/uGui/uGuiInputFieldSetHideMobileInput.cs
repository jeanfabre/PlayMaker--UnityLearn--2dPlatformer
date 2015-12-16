// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the hide mobile Input value of a UGui InputField component.")]
	public class uGuiInputFieldSetHideMobileInput : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The hide Mobile flag value of the UGui InputField component.")]
		public FsmBool hideMobileInput;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.InputField _inputField;

		bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			hideMobileInput = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _inputField.shouldHideMobileInput;
			}

			DoSetValue();

			Finish();

		}

		void DoSetValue()
		{
			if (_inputField!=null)
			{
				_inputField.shouldHideMobileInput = hideMobileInput.Value;
			}
		}

		public override void OnExit()
		{
			if (_inputField==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_inputField.shouldHideMobileInput = _originalValue;
			}
		}
	}
}