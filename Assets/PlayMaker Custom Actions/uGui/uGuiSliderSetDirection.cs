// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the direction of a UGui Slider component.")]
	public class uGuiSliderSetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The direction of the UGui slider component.")]
		[ObjectType(typeof(Slider.Direction))]
		public FsmEnum  direction;

		[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		public FsmBool includeRectLayouts;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		Slider _slider;

		Slider.Direction _originalValue;

		public override void Reset()
		{
			gameObject = null;
			direction = Slider.Direction.LeftToRight;
			includeRectLayouts = new FsmBool(){UseVariable=true};
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_slider = _go.GetComponent<Slider>();
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
				if (includeRectLayouts.IsNone)
				{
					_slider.direction = (Slider.Direction)direction.Value;
				}else{
					_slider.SetDirection((Slider.Direction)direction.Value,includeRectLayouts.Value);
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
				if (includeRectLayouts.IsNone)
				{
					_slider.direction = _originalValue;
				}else{
					_slider.SetDirection(_originalValue,includeRectLayouts.Value);
				}
			}
		}
	}
}