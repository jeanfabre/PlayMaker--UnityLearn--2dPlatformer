// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Animator")]
	[Tooltip("Follow a target")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1033")]
	public class AnimatorFollow : FsmStateAction
	{
		[RequiredField]
		//[CheckForComponent(typeof(PlayMakerAnimatorProxy))]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The GameObject. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The Game Object to target.")]
		public FsmGameObject target;
		
		[Tooltip("The minimum distance to follow.")]
		public FsmFloat minimumDistance;
		
		[Tooltip("The damping for following up.")]
		public FsmFloat speedDampTime;
		
		[Tooltip("The damping for turning.")]
		public FsmFloat directionDampTime;	
		
		GameObject _go;
		PlayMakerAnimatorMoveProxy _animatorProxy;
		Animator avatar;
		CharacterController controller;
		
		public override void Reset()
		{
			gameObject = null;
			target = null;
			speedDampTime = 0.25f;
			directionDampTime = 0.25f;
			minimumDistance = 1f;
		}
		
		
		public override void OnEnter()
		{
		 	_go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (_go==null)
			{
				Finish();
				return;
			}
			
			_animatorProxy = _go.GetComponent<PlayMakerAnimatorMoveProxy>();
			if (_animatorProxy!=null)
			{
				_animatorProxy.OnAnimatorMoveEvent += OnAnimatorMoveEvent;
			}
			
			avatar = _go.GetComponent<Animator>();
			controller = _go.GetComponent<CharacterController>();

			avatar.speed = 1 + UnityEngine.Random.Range(-0.4f, 0.4f);	
		}
		
		public override void OnUpdate()
		{
			GameObject _target = target.Value;
			
			float _speedDampTime = speedDampTime.Value;
			float _directionDampTime = directionDampTime.Value;
			float _minimumDistance = minimumDistance.Value;
			
			if (avatar && _target)
			{			
				if(Vector3.Distance(_target.transform.position,avatar.rootPosition) > _minimumDistance)
				{
					avatar.SetFloat("Speed",1,_speedDampTime, Time.deltaTime);
					
					Vector3 currentDir = avatar.rootRotation * Vector3.forward;
					Vector3 wantedDir = (_target.transform.position - avatar.rootPosition).normalized;
		
					if(Vector3.Dot(currentDir,wantedDir) > 0)
					{
						avatar.SetFloat("Direction",Vector3.Cross(currentDir,wantedDir).y,_directionDampTime, Time.deltaTime);
					}
					else
					{
	            		avatar.SetFloat("Direction", Vector3.Cross(currentDir,wantedDir).y > 0 ? 1 : -1, _directionDampTime, Time.deltaTime);
					}
				}
				else
				{
	            	avatar.SetFloat("Speed",0,_speedDampTime, Time.deltaTime);
				}
				
				if (_animatorProxy==null)
				{
					OnAnimatorMoveEvent();
				}
			}
		}
		
		public override void OnExit()
		{
			if (_animatorProxy!=null)
			{
				_animatorProxy.OnAnimatorMoveEvent -= OnAnimatorMoveEvent;
			}
		}
		
		public void OnAnimatorMoveEvent()
		{
			controller.Move(avatar.deltaPosition);
			_go.transform.rotation = avatar.rootRotation;
		}
		


	}
}