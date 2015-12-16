// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// waiting for 1.8 to make it available using fsmEnum

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the navigation mode of a Selectable Ugui component.")]
	public class uGuiNavigationSetMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The navigation mode value")]
		public UnityEngine.UI.Navigation.Mode navigationMode;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		UnityEngine.UI.Selectable _selectable;
		UnityEngine.UI.Navigation _navigation;
		UnityEngine.UI.Navigation.Mode _originalValue;
		
		
		public override void Reset()
		{
			gameObject = null;
			navigationMode = UnityEngine.UI.Navigation.Mode.Automatic;

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
				_originalValue = _selectable.navigation.mode;
			}

			DoSetValue();

			Finish();
		}
		
		void DoSetValue()
		{
			if (_selectable!=null)
			{
				_navigation = _selectable.navigation;
				_navigation.mode = navigationMode;
				_selectable.navigation = _navigation;
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
				_navigation = _selectable.navigation;
				_navigation.mode = _originalValue;
				_selectable.navigation = _navigation;
			}
		}
		
		
	}
}