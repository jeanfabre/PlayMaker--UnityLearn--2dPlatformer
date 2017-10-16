// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the wholeNumbers property of a UGui Slider component. If true, the slider is constrained to integer values")]
	public class uGuiSliderGetWholeNumbers : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Is the slider constrained to integer values?")]
		public FsmBool wholeNumbers;

		[Tooltip("Event sent if slider is showing integers")]
		public FsmEvent isShowingWholeNumbersEvent;
		
		[Tooltip("Event sent if slider is showing floats")]
		public FsmEvent isNotShowingWholeNumbersEvent;

		private UnityEngine.UI.Slider _slider;

		public override void Reset()
		{
			gameObject = null;
			isShowingWholeNumbersEvent = null;
			isNotShowingWholeNumbersEvent = null;
			wholeNumbers = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_slider = _go.GetComponent<UnityEngine.UI.Slider>();
			}

			DoGetValue();

			Finish();
		}

		void DoGetValue()
		{
			bool _val = false;

			if (_slider!=null)
			{
				_val = _slider.wholeNumbers;
			}
		
			wholeNumbers.Value =_val;

			if(_val)
			{
				Fsm.Event(isShowingWholeNumbersEvent);
			}else{
				Fsm.Event(isNotShowingWholeNumbersEvent);
			}

		}
	}
}