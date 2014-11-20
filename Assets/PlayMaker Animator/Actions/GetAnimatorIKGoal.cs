// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Gets the position, rotation and weights of an IK goal. A GameObject can be set to use for the position and rotation")]
	public class GetAnimatorIKGoal: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The IK goal")]
		public AvatarIKGoal iKGoal;
				
		[Tooltip("Repeat every frame. Useful for changing over time.")]
		public bool everyFrame;
		
		[ActionSection("Results")]
		
		[Tooltip("The gameObject to apply ik goal position and rotation to")]
		public FsmGameObject goal;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The position of the ik goal. If Goal GameObject define, position is used as an offset from Goal")]
		public FsmVector3 position;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The rotation of the ik goal.If Goal GameObject define, rotation is used as an offset from Goal")]
		public FsmQuaternion rotation;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The translative weight of an IK goal (0 = at the original animation before IK, 1 = at the goal)")]
		public FsmFloat positionWeight;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets the rotational weight of an IK goal (0 = rotation before IK, 1 = rotation at the IK goal)")]
		public FsmFloat rotationWeight;

		private Animator _animator;
		
		private Transform _transform;
		
		public override void Reset()
		{
			gameObject = null;
			
			goal = null;
			position = null;
			rotation = null;
			positionWeight = null;
			rotationWeight = null;
			
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
			
			GameObject _goal = goal.Value;
			if (_goal!=null)
			{
				_transform = _goal.transform;
			}
			
			DoGetIKGoal();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoGetIKGoal();
		}
		
	
		void DoGetIKGoal()
		{		
			if (_animator==null)
			{
				return;
			}
			
			if (_transform!=null)
			{
				_transform.position = _animator.GetIKPosition(iKGoal);
				_transform.rotation = _animator.GetIKRotation(iKGoal);
			}
			
			if (!position.IsNone)
			{
				position.Value = _animator.GetIKPosition(iKGoal);
			}
			
			if (!rotation.IsNone)
			{
				rotation.Value =_animator.GetIKRotation(iKGoal);
			}
			
			if (!positionWeight.IsNone)
			{
				positionWeight.Value = _animator.GetIKPositionWeight(iKGoal);
			}
			if (!rotationWeight.IsNone)
			{
				rotationWeight.Value = _animator.GetIKRotationWeight(iKGoal);
			}
		}
		
	}
}