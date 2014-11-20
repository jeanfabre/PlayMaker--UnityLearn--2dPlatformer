// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Returns true if automatic matching is active. Can also send events")]
	public class GetAnimatorIsMatchingTarget: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Repeat every frame. Warning: do not use the events in this action if you set everyFrame to true")]
		public bool everyFrame;
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("True if automatic matching is active")]
		public FsmBool isMatchingActive;
		
		[Tooltip("Event send if automatic matching is active")]
		public FsmEvent matchingActivatedEvent;
		
		[Tooltip("Event send if automatic matching is not active")]
		public FsmEvent matchingDeactivedEvent;
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			isMatchingActive = null;
			matchingActivatedEvent = null;
			matchingDeactivedEvent = null;
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
			
			
			DoCheckIsMatchingActive();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				DoCheckIsMatchingActive();
			}
		}
		
		public void OnAnimatorMoveEvent()
		{
			DoCheckIsMatchingActive();
		}
		
		void DoCheckIsMatchingActive()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bool _isMatchingActive = _animator.isMatchingTarget;
			isMatchingActive.Value = _isMatchingActive;
			
			if (_isMatchingActive)
			{
				Fsm.Event(matchingActivatedEvent);
			}else{
				Fsm.Event(matchingDeactivedEvent);
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