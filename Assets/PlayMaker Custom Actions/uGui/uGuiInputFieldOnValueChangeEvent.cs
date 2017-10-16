// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value change for a UGui InputField component. Event string data will feature the text value")]
	public class uGuiInputFieldOnValueChangeEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Send this event when value changed.")]
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
					#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
					_inputField.onValueChange.AddListener(DoOnValueChange);
					#else
					_inputField.onValueChanged.AddListener(DoOnValueChange);
					#endif
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
				#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
					_inputField.onValueChange.RemoveListener(DoOnValueChange);
				#else
					_inputField.onValueChanged.RemoveListener(DoOnValueChange);
				#endif
			}
		}

		public void DoOnValueChange(string value)
		{
			Fsm.EventData.StringData = value;
			Fsm.Event(sendEvent);
		}
	}
}