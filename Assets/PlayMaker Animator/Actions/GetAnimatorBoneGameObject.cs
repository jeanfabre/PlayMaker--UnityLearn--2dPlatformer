// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Gets the GameObject mapped to this human bone id")]
	public class GetAnimatorBoneGameObject : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The bone reference")]
		public HumanBodyBones bone;

		[Tooltip("The bone reference as a string, case insensitive")]
		public FsmString boneAsString;

		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bone's GameObject")]
		public FsmGameObject boneGameObject;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			bone = HumanBodyBones.Hips;
			boneAsString = new FsmString(){UseVariable=false};
			boneGameObject = null;
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

			GetBoneTransform();

			Finish();

		}

		void GetBoneTransform()
		{
			HumanBodyBones _boneid =  boneAsString.IsNone?bone:(HumanBodyBones)Enum.Parse(typeof(HumanBodyBones),boneAsString.Value,true);
			boneGameObject.Value = _animator.GetBoneTransform(_boneid).gameObject;
		}
		

	}
}