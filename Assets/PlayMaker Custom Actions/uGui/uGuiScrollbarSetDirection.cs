// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// waiting for 1.8 to make it available using fsmEnum

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the direction of a UGui Scrollbar component.")]
	public class uGuiScrollbarSetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The direction of the UGui Scrollbar component.")]
		public UnityEngine.UI.Scrollbar.Direction direction;

		// not sure what it does
		//[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		//public FsmBool includeRectLayouts;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.Scrollbar _scrollbar;

		UnityEngine.UI.Scrollbar.Direction _originalValue;

		public override void Reset()
		{
			gameObject = null;
			direction = UnityEngine.UI.Scrollbar.Direction.LeftToRight;
			//includeRectLayouts = new FsmBool(){UseVariable=true};
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_scrollbar = _go.GetComponent<UnityEngine.UI.Scrollbar>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _scrollbar.direction;
			}

			DoSetValue();

			Finish();
		}

		void DoSetValue()
		{

			if (_scrollbar!=null)
			{
				//if (includeRectLayouts.IsNone)
				//{
					_scrollbar.direction = direction;
			//	}else{
			//		_slider.SetDirection(direction,includeRectLayouts.Value);
			//	}
			}
		}

		public override void OnExit()
		{
			if (_scrollbar==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_scrollbar.direction = _originalValue;
			}
		}
	}
}