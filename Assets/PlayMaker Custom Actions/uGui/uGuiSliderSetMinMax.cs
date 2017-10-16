// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the minimum and maximum limits for the value of a UGui Slider component. Optionally resets on exit")]
	public class uGuiSliderSetMinMax : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;


		[Tooltip("The minimum value of the UGui slider component. Leave to none for no effect")]
		public FsmFloat minValue;

		[Tooltip("The maximum value of the UGui slider component. Leave to none for no effect")]
		public FsmFloat maxValue;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Slider _slider;

		float _originalMinValue;
		float _originalMaxValue;

		public override void Reset()
		{
			gameObject = null;
			minValue = new FsmFloat(){UseVariable=true};
			maxValue = new FsmFloat(){UseVariable=true};
			resetOnExit = null;
			everyFrame = false;
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
				_originalMinValue = _slider.minValue;
				_originalMaxValue = _slider.maxValue;
			}

			DoSetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValue();
		}
		
		void DoSetValue()
		{

			if (_slider!=null)
			{
				if (!minValue.IsNone)
				{
					_slider.minValue = minValue.Value;
				}
				if (!maxValue.IsNone)
				{
					_slider.maxValue = maxValue.Value;
				}
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
				_slider.minValue = _originalMinValue;
				_slider.maxValue = _originalMaxValue;
			}
		}
	}
}