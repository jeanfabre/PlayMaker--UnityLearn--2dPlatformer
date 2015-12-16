// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Fires an event when editing ended for a UGui InputField component. Event string data will feature the text value")]
	public class uGuiInputFieldOnEndEditEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Send this event when editing ended.")]
		public FsmEvent sendEvent;

		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			gameObject = null;
			sendEvent = null;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go!=null)
			{
				_inputField = go.GetComponent<UnityEngine.UI.InputField>();
				if (_inputField!=null)
				{
					_inputField.onEndEdit.AddListener(DoOnEndEdit);
				}else{
					LogError("Missing UI.InputField on "+go.name);
				}
			}else{
				LogError("Missing GameObject");
			}

		}

		public override void OnExit()
		{
			if (_inputField!=null)
			{
				_inputField.onEndEdit.RemoveListener(DoOnEndEdit);
			}
		}

		public void DoOnEndEdit(string value)
		{
			Fsm.EventData.StringData = value;
			Fsm.Event(sendEvent);
		}
	}
}