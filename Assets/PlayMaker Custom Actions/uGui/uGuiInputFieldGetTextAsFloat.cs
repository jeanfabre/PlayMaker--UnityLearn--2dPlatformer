// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the text value as a float of a UGui InputField component.")]
	public class uGuiInputFieldGetTextAsFloat : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value as a float of the UGui InputField component.")]
		public FsmFloat value;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if text resolves to a float")]
		public FsmBool isFloat;

		[Tooltip("true if text resolves to a float")]
		public FsmEvent isFloatEvent;

		[Tooltip("Event sent if text does not resolves to a float")]
		public FsmEvent isNotFloatEvent;

		public bool everyFrame;
		
		private UnityEngine.UI.InputField _inputField;

		float _value = 0f;
		bool _success;

		public override void Reset()
		{
			value = null;
			isFloat = null;
			isFloatEvent = null;
			isNotFloatEvent = null;
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
				_success = float.TryParse(_inputField.text, out _value);
				value.Value = _value;
				isFloat.Value = _success;
				Fsm.Event(_success ? isFloatEvent : isNotFloatEvent);
			}
		}
		
	}
}