// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("If true, additionnal layers affects the mass center")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1070")]
	public class SetAnimatorLayersAffectMassCenter: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("If true, additionnal layers affects the mass center")]
		public FsmBool affectMassCenter;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			affectMassCenter= null;
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
			
			SetAffectMassCenter();
			
			Finish();
			
		}
	
		void SetAffectMassCenter()
		{		
			if (_animator==null)
			{
				return;
			}
			
			_animator.layersAffectMassCenter = affectMassCenter.Value;
			
		}
		
	}
}