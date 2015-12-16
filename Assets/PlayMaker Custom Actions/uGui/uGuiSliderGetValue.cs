// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the value of a UGui Slider component.")]
	public class uGuiSliderGetValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The value of the UGui slider component.")]
		public FsmFloat value;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Slider _slider;

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
				_slider = _go.GetComponent<UnityEngine.UI.Slider>();
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

			if (_slider!=null)
			{
				value.Value = _slider.value ;
			}
		}
	}
}