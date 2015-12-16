// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the isOn value of a UGui Toggle component. Optionally send events")]
	public class uGuiToggleGetIsOn : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Toggle))]
		[Tooltip("The GameObject with the Toggle UGui component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The isOn Value of the UGui slider component.")]
		public FsmBool value;

		[Tooltip("Event sent when isOn Value is true.")]
		public FsmEvent isOnEvent;

		[Tooltip("Event sent when isOn Value is false.")]
		public FsmEvent isOffEvent;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Toggle _toggle;

		public override void Reset()
		{
			gameObject = null;
			value = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_toggle = _go.GetComponent<UnityEngine.UI.Toggle>();
			}

			DoGetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetValue();
		}
		
		void DoGetValue()
		{

			if (_toggle!=null)
			{
				value.Value = _toggle.isOn;

				if (_toggle.isOn)
				{
					if (isOnEvent!=null)
					{
						Fsm.Event(isOnEvent);
					}
				}else{
					if (isOnEvent!=null)
					{
						Fsm.Event(isOffEvent);
					}
				}

			}


		}
	}
}