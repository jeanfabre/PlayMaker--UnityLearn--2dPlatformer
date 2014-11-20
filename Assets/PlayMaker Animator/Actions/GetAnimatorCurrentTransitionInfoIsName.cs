// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Check the active Transition name on a specified layer. Format is 'CURRENT_STATE -> NEXT_STATE'.")]
	public class GetAnimatorCurrentTransitionInfoIsName : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;
		
		[Tooltip("The name to check the transition against.")]
		public FsmString name;
		
		public bool everyFrame;
		
		[ActionSection("Results")]
		
		public FsmBool nameMatch;
		
		public FsmEvent nameMatchEvent;
		public FsmEvent nameDoNotMatchEvent;
		
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			layerIndex = null;
			
			name = null;
			
			nameMatch = null;
			nameMatchEvent = null;
			nameDoNotMatchEvent = null;
			
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
			
			IsName();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			IsName();
		}
		
		void IsName()
		{		
			if (_animator!=null)
			{
				AnimatorTransitionInfo _info = _animator.GetAnimatorTransitionInfo(layerIndex.Value);
				
				if (_info.IsName(name.Value))
				{
					nameMatch.Value = true;
					Fsm.Event(nameMatchEvent);
				}else{
					nameMatch.Value = false;
					Fsm.Event(nameDoNotMatchEvent);
				}
			}
		}
	}
}