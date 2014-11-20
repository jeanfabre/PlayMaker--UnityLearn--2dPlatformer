// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the value of a UGui Slider component.")]
	public class uGuiSetSliderValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Slider))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The value of the UGui slider component.")]
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
			
			DoSetValue();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetValue();
		}
		
		void DoSetValue()
		{

			if (_slider!=null)
			{
					_slider.value = value.Value;
			}
		}
		
	}
}