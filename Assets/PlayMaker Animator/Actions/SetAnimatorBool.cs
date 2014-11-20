// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Sets the value of a bool parameter")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1063")]
	public class SetAnimatorBool : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The animator parameter")]
		public FsmString parameter;
		
		[Tooltip("The Bool value to assign to the animator parameter")]
		public FsmBool Value;
		
		[Tooltip("Repeat every frame. Useful when value is changing over time.")]
		public bool everyFrame;
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		private int _paramID;
		
		public override void Reset()
		{
			gameObject = null;
			parameter = null;
			Value = null;
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
			
			// get hash from the param for efficiency:
			_paramID = Animator.StringToHash(parameter.Value);
			
			SetParameter();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
	
		public void OnAnimatorMoveEvent()
		{
			if (_animatorProxy!=null)
			{
				SetParameter();
			}
		}	
		
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				SetParameter();
			}
		}
		
		void SetParameter()
		{		
			if (_animator!=null)
			{
				_animator.SetBool(_paramID,Value.Value) ;
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