// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Sets the position, rotation and weights of an IK goal. A GameObject can be set to control the position and rotation, or it can be manually expressed.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1067")]
	public class SetAnimatorIKGoal: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[CheckForComponent(typeof(PlayMakerAnimatorIKProxy))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorIKProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The IK goal")]
		public AvatarIKGoal iKGoal;
		
		[Tooltip("The gameObject target of the ik goal")]
		public FsmGameObject goal;
		
		[Tooltip("The position of the ik goal. If Goal GameObject set, position is used as an offset from Goal")]
		public FsmVector3 position;
		
		[Tooltip("The rotation of the ik goal.If Goal GameObject set, rotation is used as an offset from Goal")]
		public FsmQuaternion rotation;
		
		[HasFloatSlider(0f,1f)]
		[Tooltip("The translative weight of an IK goal (0 = at the original animation before IK, 1 = at the goal)")]
		public FsmFloat positionWeight;
		
		[HasFloatSlider(0f,1f)]
		[Tooltip("Sets the rotational weight of an IK goal (0 = rotation before IK, 1 = rotation at the IK goal)")]
		public FsmFloat rotationWeight;
		
		[Tooltip("Repeat every frame. Useful when changing over time.")]
		public bool everyFrame;
		
		private PlayMakerAnimatorIKProxy _animatorProxy;
		
		private Animator _animator;
		
		private Transform _transform;
		
		public override void Reset()
		{
			gameObject = null;
			goal = null;
			position = new FsmVector3() {UseVariable=true};
			rotation = new FsmQuaternion() {UseVariable=true};
			positionWeight = 1f;
			rotationWeight = 1f;
			
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
			
			_animatorProxy = go.GetComponent<PlayMakerAnimatorIKProxy>();
			if (_animatorProxy!=null)
			{
				_animatorProxy.OnAnimatorIKEvent += OnAnimatorIKEvent;
			}else{
				Debug.LogWarning("This action requires a PlayMakerAnimatorIKProxy. It may not perform properly if not present.");
			}
			
			
			
			GameObject _goal = goal.Value;
			if (_goal!=null)
			{
				_transform = _goal.transform;
			}
			
			
			DoSetIKGoal();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
	
		public void OnAnimatorIKEvent(int layer)
		{
			if (_animatorProxy!=null)
			{
				DoSetIKGoal();
			}
		}	
		
		/* not working
		public override void OnUpdate() 
		{
			if (_animatorProxy==null)
			{
				DoSetIKGoal();
			}
		}
		 */
		void DoSetIKGoal()
		{		
			if (_animator==null)
			{
				return;
			}
			
			if (_transform!=null)
			{
				if (position.IsNone)
				{
					_animator.SetIKPosition(iKGoal,_transform.position);
				}else{
					_animator.SetIKPosition(iKGoal,_transform.position+position.Value);
				}
				
				if (rotation.IsNone)
				{
					_animator.SetIKRotation(iKGoal,_transform.rotation);
				}else{
					_animator.SetIKRotation(iKGoal,_transform.rotation*rotation.Value);
				}
			}else{
				
				if (!position.IsNone)
				{
					_animator.SetIKPosition(iKGoal,position.Value);
				}
				
				if (!rotation.IsNone)
				{
					_animator.SetIKRotation(iKGoal,rotation.Value);
				}
			}
			
			if (!positionWeight.IsNone)
			{
				_animator.SetIKPositionWeight(iKGoal,positionWeight.Value);
			}
			if (!rotationWeight.IsNone)
			{
				_animator.SetIKRotationWeight(iKGoal,rotationWeight.Value);
			}
		}
		
		public override void OnExit()
		{
			if (_animatorProxy!=null)
			{
				_animatorProxy.OnAnimatorIKEvent -= OnAnimatorIKEvent;
			}
		}
		
	}
}