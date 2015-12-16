// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the wholeNumbers property of a UGui Slider component. This defines if the slider will be constrained to integer values ")]
	public class uGuiSliderSetWholeNumbers : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Should the slider be constrained to integer values?")]
		public FsmBool wholeNumbers;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.Slider _slider;

		bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			wholeNumbers = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_slider = _go.GetComponent<UnityEngine.UI.Slider>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _slider.wholeNumbers;
			}

			DoSetValue();

			Finish();

		}

		void DoSetValue()
		{

			if (_slider!=null)
			{
				_slider.wholeNumbers = wholeNumbers.Value;
			}
		}

		public override void OnExit()
		{
			if (_slider==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_slider.wholeNumbers = _originalValue;
			}
		}
	}
}