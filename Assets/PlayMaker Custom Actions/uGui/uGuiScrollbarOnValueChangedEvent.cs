// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value changed for a UGui Scrollbar component. Event float data will feature the slider value")]
	public class uGuiScrollbarOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;

		private UnityEngine.UI.Scrollbar _scrollbar;
		
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
				_scrollbar = go.GetComponent<UnityEngine.UI.Scrollbar>();
				if (_scrollbar!=null)
				{
					_scrollbar.onValueChanged.AddListener(DoOnValueChanged);
				}else{
					LogError("Missing UI.Scrollbar on "+go.name);
				}
			}else{
				LogError("Missing GameObject");
			}

		}

		public override void OnExit()
		{
			if (_scrollbar!=null)
			{
				_scrollbar.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}

		public void DoOnValueChanged(float value)
		{
			Fsm.EventData.FloatData = value;
			Fsm.Event(sendEvent);
		}
	}
}