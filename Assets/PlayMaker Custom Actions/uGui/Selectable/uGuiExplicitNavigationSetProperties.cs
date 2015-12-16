// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the explicit navigation properties of a Selectable Ugui component. Note that it will have no effect until Navigation mode is set to 'Explicit'.")]
	public class uGuiNavigationExplicitSetProperties : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The down Selectable. Leave to none for no effect")]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		public FsmGameObject selectOnDown;

		[Tooltip("The up Selectable.  Leave to none for no effect")]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		public FsmGameObject selectOnUp;

		[Tooltip("The left Selectable.  Leave to none for no effect")]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		public FsmGameObject selectOnLeft;

		[Tooltip("The right Selectable.  Leave to none for no effect")]
		[CheckForComponent(typeof(UnityEngine.UI.Selectable))]
		public FsmGameObject selectOnRight;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		UnityEngine.UI.Selectable _selectable;
		UnityEngine.UI.Navigation _navigation;
		UnityEngine.UI.Navigation _originalState;
		
		
		public override void Reset()
		{
			gameObject = null;
			selectOnDown = new FsmGameObject(){UseVariable=true};
			selectOnUp = new FsmGameObject(){UseVariable=true};
			selectOnLeft = new FsmGameObject(){UseVariable=true};
			selectOnRight = new FsmGameObject(){UseVariable=true};

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
				_originalState = _selectable.navigation;
			}
			
			DoSetValue();
			

			Finish();
		}
		
		void DoSetValue()
		{
			if (_selectable!=null)
			{
				_navigation = _selectable.navigation;
				
				if (!selectOnDown.IsNone )
				{
					_navigation.selectOnDown = GetComponentFromFsmGameObject<UnityEngine.UI.Selectable>(selectOnDown);
				}
				if (!selectOnUp.IsNone )
				{
					_navigation.selectOnUp = GetComponentFromFsmGameObject<UnityEngine.UI.Selectable>(selectOnUp);
				}
				if (!selectOnLeft.IsNone )
				{
					_navigation.selectOnLeft = GetComponentFromFsmGameObject<UnityEngine.UI.Selectable>(selectOnLeft);
				}
				if (!selectOnRight.IsNone )
				{
					_navigation.selectOnRight = GetComponentFromFsmGameObject<UnityEngine.UI.Selectable>(selectOnRight);
				}

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
				_navigation =  _selectable.navigation;
				_navigation.selectOnDown = _originalState.selectOnDown;
				_navigation.selectOnLeft = _originalState.selectOnLeft;
				_navigation.selectOnRight = _originalState.selectOnRight;
				_navigation.selectOnUp = _originalState.selectOnUp;

				_selectable.navigation = _navigation;
			}
		}
		
		T GetComponentFromFsmGameObject<T>(FsmGameObject variable) where T : Component
		{
			if (variable.Value!=null)
			{
				T _sel = variable.Value.GetComponent<T>();
				
				return _sel;
			}
			
			return null;
		}		
	}
}