// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayMakerFSM))]
public class PlayMakerAnimatorIKProxy : MonoBehaviour 
{
	
	public event Action<int> OnAnimatorIKEvent;
	
	private Animator _animator;

	void OnAnimatorIK(int layerIndex)
	{
		if( OnAnimatorIKEvent != null )
		{
			OnAnimatorIKEvent(layerIndex);
		}
	}
}
