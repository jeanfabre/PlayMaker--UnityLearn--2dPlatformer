// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Gets the playback speed of the Animator. 1 is normal playback speed")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1056")]
	public class GetAnimatorSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The playBack speed of the animator. 1 is normal playback speed")]
		public FsmFloat speed;
		
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			speed = null;
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

			GetPlaybackSpeed();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			GetPlaybackSpeed();
		}
		
		void GetPlaybackSpeed()
		{		
			if (_animator!=null)
			{
				speed.Value = _animator.speed;
			}
		}
	}
}