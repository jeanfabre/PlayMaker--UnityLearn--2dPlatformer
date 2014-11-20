// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Sets a trigger parameter to active or inactive. Triggers are parameters that act mostly like booleans, but get resets to inactive when they are used in a transition.")]
	//[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1063")]
	public class SetAnimatorTrigger : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The trigger name")]
		public FsmString trigger;
		
		private Animator _animator;
		private int _paramID;
		
		public override void Reset()
		{
			gameObject = null;
			trigger = null;

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

			SetTrigger();

			Finish();

		}
		

		void SetTrigger()
		{		
			if (_animator!=null)
			{
				_animator.SetTrigger(trigger.Value);
			}
		}

	}
}