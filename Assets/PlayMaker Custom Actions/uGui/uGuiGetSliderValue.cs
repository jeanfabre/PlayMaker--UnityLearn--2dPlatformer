// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Get the value of a UGui Slider component.")]
	public class uGuiGetSliderValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Slider))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The value of the UGui slider component.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat value;
		
		public bool everyFrame;
		
		private uUI.Slider _slider;
		
		public override void Reset()
		{
			value = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_slider = _go.GetComponent<uUI.Slider>();
			}
			
			DoGetValue();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetValue();
		}
		
		void DoGetValue()
		{
			
			if (_slider!=null)
			{
					value.Value = _slider.value ;
			}
		}

		
	}
}