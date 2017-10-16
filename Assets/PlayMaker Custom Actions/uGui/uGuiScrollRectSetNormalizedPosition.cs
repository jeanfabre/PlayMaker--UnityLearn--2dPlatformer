// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("The scroll normalized position as a Vector2 between (0,0) and (1,1) with (0,0) being the lower left corner.")]
	public class uGuiScrollRectSetNormalizedPosition : FsmStateAction
	{
	
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.ScrollRect))]
		[Tooltip("The GameObject with the ScrollRect UGui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The position's value of the UGui Scrollbar component. Ranges from 0.0 to 1.0.")]
		public FsmVector2 normalizedPosition;

		[Tooltip("The horizontal position's value of the UGui ScrollRect component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f,1f)]
		public FsmFloat horizontalPosition;
	
		[Tooltip("The vertical position's value of the UGui ScrollRect component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f,1f)]
		public FsmFloat verticalPosition;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.ScrollRect _scrollRect;

		Vector2 _originalValue;

		public override void Reset()
		{
			gameObject = null;
			normalizedPosition = null;
			horizontalPosition = new FsmFloat(){UseVariable=true};
			verticalPosition = new FsmFloat(){UseVariable=true};
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_scrollRect = _go.GetComponent<UnityEngine.UI.ScrollRect>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _scrollRect.normalizedPosition;
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

			if (_scrollRect!=null)
			{

				Vector2 _temp = _scrollRect.normalizedPosition;

				if (!normalizedPosition.IsNone)
				{
					_temp = normalizedPosition.Value;
				}

				if (!horizontalPosition.IsNone)
				{
					_temp.x = horizontalPosition.Value;
				}

				if (!verticalPosition.IsNone)
				{
					_temp.y = verticalPosition.Value;
				}

				_scrollRect.normalizedPosition = _temp;
			}
		}

		public override void OnExit()
		{
			if (_scrollRect==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_scrollRect.normalizedPosition = _originalValue;
			}
		}
	}
}