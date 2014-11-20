// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Gets the avatar body mass center position and rotation.Optionally accept a GameObject to get the body transform. \nThe position and rotation are local to the gameobject")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1036")]
	public class GetAnimatorBody: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Repeat every frame. Useful when changing over time.")]
		public bool everyFrame;
		
		[ActionSection("Results")]
			
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmVector3 bodyPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmQuaternion bodyRotation;
		
		[Tooltip("If set, apply the body mass center position and rotation to this gameObject")]
		public FsmGameObject bodyGameObject;
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		private Transform _transform;
		
		public override void Reset()
		{
			gameObject = null;
			bodyPosition= null;
			bodyRotation = null;
			bodyGameObject = null;
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
			
			
			GameObject _body = bodyGameObject.Value;
			if (_body!=null)
			{
				_transform = _body.transform;
			}
			
			DoGetBodyPosition();
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
	
		public void OnAnimatorMoveEvent()
		{
			if (_animatorProxy!=null)
			{
				DoGetBodyPosition();
			}
		}	
		
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				DoGetBodyPosition();
			}
		}
		
		void DoGetBodyPosition()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bodyPosition.Value = _animator.bodyPosition;
			bodyRotation.Value = _animator.bodyRotation;
			
			if (_transform!=null)
			{
				_transform.position = _animator.bodyPosition;
				_transform.rotation = _animator.bodyRotation;
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