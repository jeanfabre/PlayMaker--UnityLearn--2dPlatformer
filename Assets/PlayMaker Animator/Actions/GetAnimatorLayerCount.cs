// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Returns the Animator controller layer count")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1051")]
	public class GetAnimatorLayerCount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Animator controller layer count")]
		public FsmInt layerCount;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			layerCount = null;
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
			
			DoGetLayerCount();
			
			Finish();
			
		}
	
		void DoGetLayerCount()
		{		
			if (_animator==null)
			{
				return;
			}

			layerCount.Value = _animator.layerCount;

		}
		
	}
}