// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the navigation mode of a Selectable Ugui component.")]
	public class uGuiNavigationGetMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The navigation mode value")]
		public FsmString navigationMode;

		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent automaticEvent;

		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent horizontalEvent;

		[Tooltip("Event sent if transition is SpriteSwap")]
		public FsmEvent verticalEvent;

		[Tooltip("Event sent if transition is Animation")]
		public FsmEvent explicitEvent;

		[Tooltip("Event sent if transition is none")]
		public FsmEvent noNavigationEvent;

		UnityEngine.UI.Selectable _selectable;
		UnityEngine.UI.Selectable.Transition _originalTransition;
		
		
		public override void Reset()
		{
			gameObject = null;

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

			navigationMode.Value = _selectable.navigation.mode.ToString();

			if (_selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.None)
			{
				Fsm.Event(noNavigationEvent);
			}else if (_selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Automatic)
			{
				Fsm.Event(automaticEvent);
			}else if (_selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Vertical)
			{
				Fsm.Event(verticalEvent);
			}else if (_selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Horizontal)
			{
				Fsm.Event(horizontalEvent);
			}else if (_selectable.navigation.mode == UnityEngine.UI.Navigation.Mode.Explicit)
			{
				Fsm.Event(explicitEvent);
			}

		}
		

	}
}