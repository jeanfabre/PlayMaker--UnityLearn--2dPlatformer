// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the interactable flag of a Selectable Ugui component.")]
	public class uGuiGetIsInteractable : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Interactable value")]
		[UIHint(UIHint.Variable)]
		public FsmBool isInteractable;

		[Tooltip("Event sent if Component is Interactable")]
		public FsmEvent isInteractableEvent;

		[Tooltip("Event sent if Component is not Interactable")]
		public FsmEvent isNotInteractableEvent;

		UnityEngine.UI.Selectable _selectable;
		bool _originalState;
		
		
		public override void Reset()
		{
			gameObject = null;
			isInteractable = null;
			isInteractableEvent = null;
			isNotInteractableEvent = null;
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

			bool _flag  = _selectable.IsInteractable();
			isInteractable.Value = _flag;

			if (_flag)
			{
				Fsm.Event(isInteractableEvent);
			}else{
				Fsm.Event(isNotInteractableEvent);
			}

		}

		
	}
}