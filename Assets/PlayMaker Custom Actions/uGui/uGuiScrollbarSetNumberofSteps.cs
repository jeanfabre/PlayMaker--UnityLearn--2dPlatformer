// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the number of distinct scroll positions allowed of a uGui Scrollbar component.")]
	public class uGuiScrollbarSetNumberOfSteps : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The number of distinct scroll positions allowed of the uGui Scrollbar component.")]
		public FsmInt value;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Scrollbar _scrollbar;

		int _originalValue;

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
				_scrollbar = _go.GetComponent<UnityEngine.UI.Scrollbar>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _scrollbar.numberOfSteps;
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

			if (_scrollbar!=null)
			{
				_scrollbar.numberOfSteps = value.Value;
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
				_scrollbar.numberOfSteps = _originalValue;
			}
		}
	}
}