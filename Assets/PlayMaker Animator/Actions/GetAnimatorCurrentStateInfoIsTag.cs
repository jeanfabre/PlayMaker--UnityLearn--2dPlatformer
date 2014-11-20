// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Does tag match the tag of the active state in the statemachine")]
	public class GetAnimatorCurrentStateInfoIsTag : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;
		
		[Tooltip("The tag to check the layer against.")]
		public FsmString tag;
		
		public bool everyFrame;
		
		[ActionSection("Results")]
		
		public FsmBool tagMatch;
		
		public FsmEvent tagMatchEvent;
		public FsmEvent tagDoNotMatchEvent;
		
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			layerIndex = null;
			
			tag = null;
			
			tagMatch = null;
			tagMatchEvent = null;
			tagDoNotMatchEvent = null;
			
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
			
			IsTag();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			IsTag();
		}
		
		void IsTag()
		{		
			if (_animator!=null)
			{
				AnimatorStateInfo _info = _animator.GetCurrentAnimatorStateInfo(layerIndex.Value);
				
				if (_info.IsTag(tag.Value))
				{
					tagMatch.Value = true;
					Fsm.Event(tagMatchEvent);
				}else{
					tagMatch.Value = false;
					Fsm.Event(tagDoNotMatchEvent);
				}
			}
		}
	}
}