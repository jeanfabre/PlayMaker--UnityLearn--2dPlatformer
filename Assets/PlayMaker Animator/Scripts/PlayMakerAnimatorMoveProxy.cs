// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayMakerFSM))]
public class PlayMakerAnimatorMoveProxy : MonoBehaviour {
	
	public bool applyRootMotion;
	
	public event Action OnAnimatorMoveEvent;

//	private Animator _animator;
	
//	private Vector3 gravityVelocity;
	
	void Start()
	{
	//	_animator = gameObject.GetComponent<Animator>();
	}
	
	void Update() {

    	//gravityVelocity += Physics.gravity * Time.deltaTime;

	}

	void OnAnimatorMove()
	{
		if( OnAnimatorMoveEvent != null )
		{
			OnAnimatorMoveEvent();
		}
	}

}
