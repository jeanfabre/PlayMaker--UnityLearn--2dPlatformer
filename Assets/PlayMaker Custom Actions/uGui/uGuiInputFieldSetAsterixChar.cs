// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the Asterix Character of a UGui InputField component.")]
	public class uGuiInputFieldSetAsterixChar : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The asterix Character used for password field type of the UGui InputField component. Only the first character will be used, the rest of the string will be ignored")]
		public FsmString asterixChar;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;


		private UnityEngine.UI.InputField _inputField;

		char _originalValue;
		static char __char__ = ' ';

		public override void Reset()
		{
			gameObject = null;
			asterixChar = "*";
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
				_originalValue = _inputField.asteriskChar;
			}

			DoSetValue();
			
			Finish();
		}

		void DoSetValue()
		{
			char _char = __char__;

			if (asterixChar.Value.Length>0)
			{
				_char = asterixChar.Value[0];
			}

			if (_inputField!=null)
			{
				_inputField.asteriskChar = _char;
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
				_inputField.asteriskChar = _originalValue;
			}
		}
	}
}