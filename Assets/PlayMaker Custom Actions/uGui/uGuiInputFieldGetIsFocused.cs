// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the focused state of a UGui InputField component.")]
	public class uGuiInputFieldGetIsFocused : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The is focused flag value of the UGui InputField component.")]
		public FsmBool isFocused;

		[Tooltip("Event sent if inputField is focused")]
		public FsmEvent isfocusedEvent;

		[Tooltip("Event sent if nputField is not focused")]
		public FsmEvent isNotFocusedEvent;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			isFocused = null;
			isfocusedEvent = null;
			isNotFocusedEvent = null;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}
			
			DoGetValue();
			
			Finish();
		}

		void DoGetValue()
		{
			if (_inputField!=null)
			{
				isFocused.Value = _inputField.isFocused;

				if (_inputField.isFocused)
				{
					Fsm.Event(isfocusedEvent);
				}else{
					Fsm.Event(isNotFocusedEvent);
				}
			}
		}
		
	}
}