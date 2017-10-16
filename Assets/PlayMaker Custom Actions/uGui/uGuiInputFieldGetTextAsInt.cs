// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the text value as an int of a UGui InputField component.")]
	public class uGuiInputFieldGetTextAsInt : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value as an int of the UGui InputField component.")]
		public FsmInt value;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if text resolves to an int")]
		public FsmBool isInt;

		[Tooltip("true if text resolves to an int")]
		public FsmEvent isIntEvent;

		[Tooltip("Event sent if text does not resolves to an int")]
		public FsmEvent isNotIntEvent;

		public bool everyFrame;
		
		private UnityEngine.UI.InputField _inputField;

		int _value = 0;
		bool _success;

		public override void Reset()
		{
			value = null;
			isInt = null;
			isIntEvent = null;
			isNotIntEvent = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}
			
			DoGetTextValue();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetTextValue();
		}
		
		void DoGetTextValue()
		{
			if (_inputField!=null)
			{
				_success = int.TryParse(_inputField.text, out _value);
				value.Value = _value;
				isInt.Value = _success;
				Fsm.Event(_success ? isIntEvent : isNotIntEvent);
			}
		}
		
	}
}