// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Gets the avatar delta position and rotation for the last evaluated frame.")]
	public class GetAnimatorDelta: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar delta position for the last evaluated frame")]
		public FsmVector3 deltaPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar delta position for the last evaluated frame")]
		public FsmQuaternion deltaRotation;
		
		[Tooltip("Repeat every frame. Useful when changing over time.")]
		public bool everyFrame;
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Transform _transform;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			deltaPosition= null;
			deltaRotation = null;
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
			
			
			DoGetDeltaPosition();
			
			Finish();
			
		}
	
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				DoGetDeltaPosition();
			}
		}
		
		public void OnAnimatorMoveEvent()
		{
			DoGetDeltaPosition();
		}
		
		void DoGetDeltaPosition()
		{		
			if (_animator==null)
			{
				return;
			}
			
			deltaPosition.Value = _animator.deltaPosition;
			deltaRotation.Value = _animator.deltaRotation;
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