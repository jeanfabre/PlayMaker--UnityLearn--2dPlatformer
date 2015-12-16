// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the caret's blink rate of a UGui InputField component.")]
	public class uGuiInputfieldSetCaretBlinkRate : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The caret's blink rate for the UGui InputField component.")]
		public FsmInt caretBlinkRate;

		[Tooltip("Deactivate when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.InputField _inputField;

		float _originalValue;

		public override void Reset()
		{
			gameObject = null;
			caretBlinkRate = null;
			resetOnExit = null;
			everyFrame = false;
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
				_originalValue = _inputField.caretBlinkRate;
			}

			DoSetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValue();
		}
		
		void DoSetValue()
		{

			if (_inputField!=null)
			{
				_inputField.caretBlinkRate = caretBlinkRate.Value;
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
				_inputField.caretBlinkRate = _originalValue;
			}
		}
	}
}