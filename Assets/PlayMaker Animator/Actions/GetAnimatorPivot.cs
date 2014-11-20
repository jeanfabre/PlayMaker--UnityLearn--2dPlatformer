// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Returns the pivot weight and/or position. The pivot is the most stable point between the avatar's left and right foot.\n For a weight value of 0, the left foot is the most stable point For a value of 1, the right foot is the most stable point")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1055")]
	public class GetAnimatorPivot : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The pivot is the most stable point between the avatar's left and right foot.\n For a value of 0, the left foot is the most stable point For a value of 1, the right foot is the most stable point")]
		public FsmFloat pivotWeight;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The pivot is the most stable point between the avatar's left and right foot.\n For a value of 0, the left foot is the most stable point For a value of 1, the right foot is the most stable point")]
		public FsmVector3 pivotPosition;

		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			pivotWeight = null;
			pivotPosition = null;
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
			
			
			DoCheckPivot();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public void OnAnimatorMoveEvent()
		{
			if (_animatorProxy!=null)
			{
				DoCheckPivot();
			}
		}	
		
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				DoCheckPivot();
			}
		}
		
	
		void DoCheckPivot()
		{		
			if (_animator==null)
			{
				return;
			}

			pivotWeight.Value = _animator.pivotWeight;
			pivotPosition.Value = _animator.pivotPosition;

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