// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Controls culling of this Animator component.\n" +
		"If true, set to 'AlwaysAnimate': always animate the entire character. Object is animated even when offscreen.\n" +
		 "If False, set to 'BasedOnRenderes' animation is disabled when renderers are not visible.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1064")]
	public class SetAnimatorCullingMode: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("If true, always animate the entire character, else animation is disabled when renderers are not visible")]
		public FsmBool alwaysAnimate;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			alwaysAnimate= null;
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
			
			SetCullingMode();
			
			Finish();
			
		}
	
		void SetCullingMode()
		{		
			if (_animator==null)
			{
				return;
			}
			
			_animator.cullingMode = alwaysAnimate.Value?AnimatorCullingMode.AlwaysAnimate:AnimatorCullingMode.BasedOnRenderers;
			
		}
		
	}
}