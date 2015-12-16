// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// waiting for 1.8 to make it available using fsmEnum/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the direction of a UGui Slider component.")]
	public class uGuiSliderSetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The direction of the UGui slider component.")]
		public UnityEngine.UI.Slider.Direction direction;

		// not sure what it does
		//[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		//public FsmBool includeRectLayouts;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.Slider _slider;

		UnityEngine.UI.Slider.Direction _originalValue;

		public override void Reset()
		{
			gameObject = null;
			direction = UnityEngine.UI.Slider.Direction.LeftToRight;
			//includeRectLayouts = new FsmBool(){UseVariable=true};
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
				_originalValue = _slider.direction;
			}

			DoSetValue();
		}

		void DoSetValue()
		{

			if (_slider!=null)
			{
				//if (includeRectLayouts.IsNone)
				//{
					_slider.direction = direction;
			//	}else{
			//		_slider.SetDirection(direction,includeRectLayouts.Value);
			//	}
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
				_slider.direction = _originalValue;
			}
		}
	}
}