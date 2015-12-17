// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Sets an AvatarTarget and a targetNormalizedTime for the current state")]
	//[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1066")]
	public class SetAnimatorTarget : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The avatar target")]
		public AvatarTarget avatarTarget;
		
		[Tooltip("The current state Time that is queried")]
		public FsmFloat targetNormalizedTime;

		[Tooltip("Repeat every frame. Useful when changing over time.")]
		public bool everyFrame;
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			avatarTarget = AvatarTarget.Body;
			targetNormalizedTime = null;
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


			SetTarget();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
		
		public void OnAnimatorMoveEvent()
		{
			if (_animatorProxy!=null)
			{
				SetTarget();
			}
		}	
		
		
		
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				SetTarget();
			}
		}
		
		void SetTarget()
		{		
			if (_animator!=null)
			{
					_animator.SetTarget(avatarTarget,targetNormalizedTime.Value) ;
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