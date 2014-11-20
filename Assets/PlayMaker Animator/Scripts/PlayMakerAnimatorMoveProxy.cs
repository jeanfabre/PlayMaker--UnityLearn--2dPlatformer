using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayMakerFSM))]
public class PlayMakerAnimatorMoveProxy : MonoBehaviour 
{

	public delegate void EventHandler();

	public event EventHandler OnAnimatorMoveEvent; // it was before event Action, but that generates now a CS0066 error. Very odd. it seems that it's because it doesn't have a return type, OnAnimatorIKEvent.cs is ok.

	void OnAnimatorMove()
	{
		if( OnAnimatorMoveEvent != null )
		{
			OnAnimatorMoveEvent();
		}
	}

}
