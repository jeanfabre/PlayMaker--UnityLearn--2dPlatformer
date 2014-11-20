// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Returns the name of a layer from its index")]
	//[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1051")]
	public class GetAnimatorLayerName : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The layer index")]
		public FsmInt layerIndex;

		[ActionSection("Results")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer name")]
		public FsmString layerName;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			layerIndex = null;
			layerName = null;
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
			
			DoGetLayerName();
			
			Finish();
			
		}
		
		void DoGetLayerName()
		{		
			if (_animator==null)
			{
				return;
			}

			layerName.Value = _animator.GetLayerName(layerIndex.Value);
			
		}
		
	}
}