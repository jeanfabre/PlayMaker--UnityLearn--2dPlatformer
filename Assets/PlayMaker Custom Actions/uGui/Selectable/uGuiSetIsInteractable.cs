// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the interactable flag of a Selectable Ugui component.")]
	public class uGuiSetIsInteractable : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Interactable value")]
		public FsmBool isInteractable;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		UnityEngine.UI.Selectable _selectable;
		bool _originalState;
		
		
		public override void Reset()
		{
			gameObject = null;
			isInteractable = null;
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
				_originalState = _selectable.IsInteractable();
			}

			DoSetValue();



			Finish();
		}
		
		void DoSetValue()
		{
			if (_selectable!=null)
			{
				_selectable.interactable = isInteractable.Value;
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
				_selectable.interactable = _originalState;
			}
		}
		
		
	}
}