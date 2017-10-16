// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the position's value of a UGui Scrollbar component. Ranges from 0.0 to 1.0.")]
	public class uGuiScrollbarSetValue : FsmStateAction
	{
	
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The position's value of the UGui Scrollbar component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f,1f)]
		public FsmFloat value;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Scrollbar _scrollbar;

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
				_scrollbar = _go.GetComponent<UnityEngine.UI.Scrollbar>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _scrollbar.value;
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
				_scrollbar.value = value.Value;
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
				_scrollbar.value = _originalValue;
			}
		}
	}
}