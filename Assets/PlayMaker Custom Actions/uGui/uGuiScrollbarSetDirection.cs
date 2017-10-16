// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.UI;

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
		[ObjectType(typeof(Scrollbar.Direction))]
		public FsmEnum direction;

		[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		public FsmBool includeRectLayouts;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		Scrollbar _scrollbar;

		Scrollbar.Direction _originalValue;

		public override void Reset()
		{
			gameObject = null;
			direction = Scrollbar.Direction.LeftToRight;
			includeRectLayouts = new FsmBool(){UseVariable=true};
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_scrollbar = _go.GetComponent<Scrollbar>();
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
				if (includeRectLayouts.IsNone)
				{
					_scrollbar.direction = (Scrollbar.Direction)direction.Value;
			}else{
					_scrollbar.SetDirection((Scrollbar.Direction)direction.Value,includeRectLayouts.Value);
				}
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
				if (includeRectLayouts.IsNone)
				{
					_scrollbar.direction = _originalValue;
				}else{
					_scrollbar.SetDirection(_originalValue,includeRectLayouts.Value);
				}

			}
		}
	}
}