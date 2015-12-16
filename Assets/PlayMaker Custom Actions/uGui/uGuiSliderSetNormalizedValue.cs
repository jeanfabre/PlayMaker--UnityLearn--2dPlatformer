// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the normalized value ( between 0 and 1) of a UGui Slider component.")]
	public class uGuiSliderSetNormalizedValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[HasFloatSlider(0f,1f)]
		[Tooltip("The normalized value ( between 0 and 1) of the UGui slider component.")]
		public FsmFloat value;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Slider _slider;

		float _originalValue;

		public override void Reset()
		{
			gameObject = null;
			value = null;
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
				_originalValue = _slider.normalizedValue;
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
				_slider.normalizedValue = value.Value;
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
				_slider.normalizedValue = _originalValue;
			}
		}
	}
}