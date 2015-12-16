// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// waiting for 1.8 to make it available using fsmEnum

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the transition type of a Selectable Ugui component.")]
	public class uGuiTransitionSetType : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The transition value")]
		public UnityEngine.UI.Selectable.Transition transition;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		UnityEngine.UI.Selectable _selectable;
		UnityEngine.UI.Selectable.Transition _originalTransition;
		
		
		public override void Reset()
		{
			gameObject = null;
			transition = UnityEngine.UI.Selectable.Transition.ColorTint;

			resetOnExit = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_selectable = _go.GetComponent<UnityEngine.UI.Selectable>();
			}

			if (_selectable!=null && resetOnExit.Value)
			{
				_originalTransition = _selectable.transition;
			}

			DoSetValue();

			Finish();
		}
		
		void DoSetValue()
		{
			if (_selectable!=null)
			{
				_selectable.transition = transition;
			}
		}
		
		public override void OnExit()
		{
			if (_selectable==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_selectable.transition = _originalTransition;
			}
		}
		
		
	}
}