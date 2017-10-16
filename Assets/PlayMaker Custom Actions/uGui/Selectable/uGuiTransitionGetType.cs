// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the transition type of a Selectable Ugui component.")]
	public class uGuiTransitionGetType : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The transition value")]
		public FsmString transition;

		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent colorTintEvent;

		[Tooltip("Event sent if transition is SpriteSwap")]
		public FsmEvent spriteSwapEvent;

		[Tooltip("Event sent if transition is Animation")]
		public FsmEvent animationEvent;

		[Tooltip("Event sent if transition is none")]
		public FsmEvent noTransitionEvent;

		UnityEngine.UI.Selectable _selectable;
		UnityEngine.UI.Selectable.Transition _originalTransition;
		
		
		public override void Reset()
		{
			gameObject = null;
			transition = null;

			colorTintEvent = null;
			spriteSwapEvent = null;
			animationEvent = null;
			noTransitionEvent = null;

		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_selectable = _go.GetComponent<UnityEngine.UI.Selectable>();
			}
			
			DoGetValue();

			Finish();
		}
		
		void DoGetValue()
		{
			if (_selectable==null)
			{
				return;
			}

			transition.Value = _selectable.transition.ToString();

			if (_selectable.transition == UnityEngine.UI.Selectable.Transition.None)
			{
				Fsm.Event(noTransitionEvent);
			}else if (_selectable.transition == UnityEngine.UI.Selectable.Transition.ColorTint)
			{
				Fsm.Event(colorTintEvent);
			}else if (_selectable.transition == UnityEngine.UI.Selectable.Transition.SpriteSwap)
			{
				Fsm.Event(spriteSwapEvent);
			}else if (_selectable.transition == UnityEngine.UI.Selectable.Transition.Animation)
			{
				Fsm.Event(animationEvent);
			}

		}
		

	}
}