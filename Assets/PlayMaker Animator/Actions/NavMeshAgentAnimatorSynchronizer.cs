// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Synchronize a NavMesh Agent velocity and rotation with the animator process.")]
	//[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1068")]
	public class NavMeshAgentAnimatorSynchronizer : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[CheckForComponent(typeof(Animator))]
		[CheckForComponent(typeof(PlayMakerAnimatorMoveProxy))]
		[Tooltip("The Agent target. An Animator component and a PlayMakerAnimatorMoveProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		
		private PlayMakerAnimatorMoveProxy _animatorProxy;
		private Animator _animator;
		private NavMeshAgent _agent;
		
		private Transform _trans;
		
		public override void Reset()
		{
			gameObject = null;
		}
		
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			
			// get the animator component
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go==null)
			{
				Finish();
				return;
			}
			_agent = go.GetComponent<NavMeshAgent>();
			
			
			_animator = go.GetComponent<Animator>();
			
			if (_animator==null)
			{
				Finish();
				return;
			}
			
			_trans = go.transform;
			
			_animatorProxy = go.GetComponent<PlayMakerAnimatorMoveProxy>();
			if (_animatorProxy!=null)
			{
				_animatorProxy.OnAnimatorMoveEvent += OnAnimatorMoveEvent;
			}
	
		}
	
		public void OnAnimatorMoveEvent()
		{
			_agent.velocity = _animator.deltaPosition / Time.deltaTime;
			_trans.rotation = _animator.rootRotation;
		}	
		
		public override void OnExit()
		{
			if (_animatorProxy!=null)
			{
				_animatorProxy.OnAnimatorMoveEvent -= OnAnimatorMoveEvent;
			}
		}
		
	}
}