// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value changed for a UGui Toggle component. Event bool data will feature the Toggle value")]
	public class uGuiToggleOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Toggle))]
		[Tooltip("The GameObject with the Toggle ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Send this event when value changed.")]
		public FsmEvent sendEvent;

		private UnityEngine.UI.Toggle _toggle;
		
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
				_toggle = go.GetComponent<UnityEngine.UI.Toggle>();
				if (_toggle!=null)
				{
					_toggle.onValueChanged.AddListener(DoOnValueChanged);
				}else{
					LogError("Missing UI.Toggle on "+go.name);
				}
			}else{
				LogError("Missing GameObject");
			}

		}

		public override void OnExit()
		{
			if (_toggle!=null)
			{
				_toggle.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}

		public void DoOnValueChanged(bool value)
		{
			Fsm.EventData.BoolData = value;
			Fsm.Event(sendEvent);
		}
	}
}