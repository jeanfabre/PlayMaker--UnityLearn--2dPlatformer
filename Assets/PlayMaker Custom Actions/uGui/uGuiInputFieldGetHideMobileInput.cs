// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the hide Mobile Input value of a UGui InputField component.")]
	public class uGuiInputFieldGetHideMobileInput : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The hide Mobile flag value of the UGui InputField component.")]
		public FsmBool hideMobileInput;

		[Tooltip("Event sent if hide mobile input property is true")]
		public FsmEvent mobileInputHiddenEvent;

		[Tooltip("Event sent if hide mobile input property is false")]
		public FsmEvent mobileInputShownEvent;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			hideMobileInput = null;
			mobileInputHiddenEvent = null;
			mobileInputShownEvent = null;
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
				hideMobileInput.Value = _inputField.shouldHideMobileInput;

				if (_inputField.shouldHideMobileInput)
				{
					Fsm.Event(mobileInputHiddenEvent);
				}else{
					Fsm.Event(mobileInputShownEvent);
				}
			}
		}
		
	}
}