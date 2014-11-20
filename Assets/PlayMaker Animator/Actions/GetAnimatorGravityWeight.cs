// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Returns The current gravity weight based on current animations that are played")]
	public class GetAnimatorGravityWeight: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The current gravity weight based on current animations that are played")]
		public FsmFloat gravityWeight;
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			gravityWeight= null;
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
			
			
			DoGetGravityWeight();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
	
		public void OnAnimatorMoveEvent()
		{
			if (_animatorProxy!=null)
			{
				DoGetGravityWeight();
			}
		}	
		
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				DoGetGravityWeight();
			}
		}
	
		void DoGetGravityWeight()
		{		
			if (_animator==null)
			{
				return;
			}
			
			gravityWeight.Value = _animator.gravityWeight;
			
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