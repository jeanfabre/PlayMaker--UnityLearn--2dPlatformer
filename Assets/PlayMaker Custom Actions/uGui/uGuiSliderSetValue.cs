// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the value of a UGui Slider component.")]
	public class uGuiSliderSetValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The value of the UGui slider component.")]
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
				_originalValue = _slider.value;
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
				_slider.value = value.Value;
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
				_slider.value = _originalValue;
			}
		}
	}
}