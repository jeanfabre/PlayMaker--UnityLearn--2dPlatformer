// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets The scrollrect horizontal flag")]
	public class uGuiScrollRectSetHorizontal : FsmStateAction
	{
	
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.ScrollRect))]
		[Tooltip("The GameObject with the ScrollRect UGui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The horizontal flag")]
		public FsmBool horizontal;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.ScrollRect _scrollRect;

		bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			horizontal = null;
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
				_originalValue = _scrollRect.vertical;
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
				_scrollRect.horizontal = horizontal.Value;
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
				_scrollRect.horizontal = _originalValue;
			}
		}
	}
}