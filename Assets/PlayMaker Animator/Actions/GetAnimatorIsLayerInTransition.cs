// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Returns true if the specified layer is in a transition. Can also send events")]
	public class GetAnimatorIsLayerInTransition: FsmStateAction
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
		[Tooltip("True if automatic matching is active")]
		public FsmBool isInTransition;
		
		[Tooltip("Event send if automatic matching is active")]
		public FsmEvent isInTransitionEvent;
		
		[Tooltip("Event send if automatic matching is not active")]
		public FsmEvent isNotInTransitionEvent;
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			isInTransition = null;
			isInTransitionEvent = null;
			isNotInTransitionEvent = null;
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
			
			
			DoCheckIsInTransition();
			
			if(!everyFrame)
			{
				Finish();
			}
		}
			
		public void OnAnimatorMoveEvent()
		{
			if (_animatorProxy!=null)
			{
				DoCheckIsInTransition();
			}
		}	
		
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				DoCheckIsInTransition();
			}
		}
		
		
		void DoCheckIsInTransition()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bool _isInTransition = _animator.IsInTransition(layerIndex.Value);
			isInTransition.Value = _isInTransition;
			
			if (_isInTransition)
			{
				Fsm.Event(isInTransitionEvent);
			}else{
				Fsm.Event(isNotInTransitionEvent);
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