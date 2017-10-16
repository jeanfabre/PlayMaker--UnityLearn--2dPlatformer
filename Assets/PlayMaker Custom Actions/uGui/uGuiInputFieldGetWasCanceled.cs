// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the cancel state of a UGui InputField component. This relates to the last onEndEdit Event")]
	public class uGuiInputFieldGetWasCanceled : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The was canceled flag value of the UGui InputField component.")]
		public FsmBool wasCanceled;

		[Tooltip("Event sent if inputField was canceled")]
		public FsmEvent wasCanceledEvent;

		[Tooltip("Event sent if inputField was not canceled")]
		public FsmEvent wasNotCanceledEvent;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			wasCanceled = null;
			wasCanceledEvent = null;
			wasNotCanceledEvent = null;
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
				wasCanceled.Value = _inputField.wasCanceled;

				if (_inputField.wasCanceled)
				{
					Fsm.Event(wasCanceledEvent);
				}else{
					Fsm.Event(wasNotCanceledEvent);
				}
			}
		}
		
	}
}