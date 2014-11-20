// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Gets the current transition information on a specified layer. Only valid when during a transition.")]
	public class GetAnimatorCurrentTransitionInfo : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;
		
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The unique name of the Transition")]
		public FsmString name;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The unique name of the Transition")]
		public FsmInt nameHash;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The user-specidied name of the Transition")]
		public FsmInt userNameHash;

		[UIHint(UIHint.Variable)]
		[Tooltip("Normalized time of the Transition")]
		public FsmFloat normalizedTime;

		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			layerIndex = null;
			
			name = null;
			nameHash = null;
			userNameHash = null;
			normalizedTime = null;
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			// get the animator component
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go==null)
			{
				Finish();
				return;
			}
			
			_animator = go.GetComponent<Animator>();
			
			if (_animator==null)
			{
				Finish();
				return;
			}
			
			_animatorProxy = go.GetComponent<PlayMakerAnimatorMoveProxy>();
			if (_animatorProxy!=null)
			{		
				_animatorProxy.OnAnimatorMoveEvent += OnAnimatorMoveEvent;
			}
			
			GetTransitionInfo();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			if (_animatorProxy == null)
			{
				GetTransitionInfo();
			}
		}
		public void OnAnimatorMoveEvent()
		{
			if (_animatorProxy!=null)
			{
				GetTransitionInfo();
			}
		}
		
		void GetTransitionInfo()
		{		
			if (_animator!=null)
			{
				AnimatorTransitionInfo _info = _animator.GetAnimatorTransitionInfo(layerIndex.Value);

				if (!name.IsNone)
				{
					name.Value = _animator.GetLayerName(layerIndex.Value);	
				}

				nameHash.Value = _info.nameHash;
				userNameHash.Value = _info.userNameHash;
				normalizedTime.Value = _info.normalizedTime;
			}
		}
		
		public override void OnExit()
		{
			if (_animatorProxy!=null)
			{
				_animatorProxy.OnAnimatorMoveEvent -= OnAnimatorMoveEvent;
			}
		}	
	}
}