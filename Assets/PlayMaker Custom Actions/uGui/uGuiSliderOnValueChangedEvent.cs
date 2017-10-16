// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value changed for a UGui Slider component. Event float data will feature the slider value")]
	public class uGuiSliderOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the Slider ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;

		private UnityEngine.UI.Slider _slider;
		
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
				_slider = go.GetComponent<UnityEngine.UI.Slider>();
				if (_slider!=null)
				{
					_slider.onValueChanged.AddListener(DoOnValueChanged);
				}else{
					LogError("Missing UI.Slider on "+go.name);
				}
			}else{
				LogError("Missing GameObject");
			}

		}

		public override void OnExit()
		{
			if (_slider!=null)
			{
				_slider.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}

		public void DoOnValueChanged(float value)
		{
			Fsm.EventData.FloatData = value;
			Fsm.Event(sendEvent);
		}
	}
}