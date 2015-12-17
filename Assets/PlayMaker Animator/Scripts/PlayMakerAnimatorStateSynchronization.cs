// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*
 inspired by Paul Hayes asset: https://github.com/paulhayes/AnimatorStateMachineUtil

This script will monitor an animator Component and will switch the fsm target states to reflect the Animator current state

Fsm state name convention allows you to not mention the layer, so you can have two possibility in your fsm

"Base.xxx" or just "xxx". and the layer name will be injected under the hood based on the LayerIndex public variable of this script.
*/

using System;

using System.Collections;
using System.Collections.Generic;

using System.Reflection;

using UnityEngine;

using HutongGames.PlayMaker;


public class PlayMakerAnimatorStateSynchronization : MonoBehaviour
{
	public int LayerIndex = 0;
	
	public PlayMakerFSM Fsm;
	
	public bool EveryFrame = true;
	
	public bool debug = false;
	
	
	Animator animator;
	int lastState;
	int lastTransition;
	
	Dictionary<int,FsmState> fsmStateLUT;
	
	void Start()
	{
		animator = this.GetComponent<Animator>();
		
		if (Fsm!=null)
		{
			string layerName = animator.GetLayerName(LayerIndex);
			fsmStateLUT = new Dictionary<int, FsmState>();
			
			foreach (FsmState state in Fsm.Fsm.States)
			{
				string fsmStateName = state.Name;
				
				RegisterHash(state.Name,state);
				
				if (!fsmStateName.StartsWith(layerName+"."))
				{
					RegisterHash(layerName+"."+state.Name,state);
				}
			}
			
		}
	}
	
	void RegisterHash(string key,FsmState state)
	{
		int hash = Animator.StringToHash(key);
		fsmStateLUT.Add(hash,state);
		
		if ( debug) Debug.Log ("registered "+key+" ->"+hash );
	}
	
	void Update()
	{
		if (EveryFrame)
		{
			Synchronize();
		}
	}
	
	public void Synchronize()
	{
		if (animator==null || Fsm==null)
		{
			return;
		}
		
		bool hasSwitchedState = false;
		
		if ( animator.IsInTransition(LayerIndex))
		{
			AnimatorTransitionInfo _transitionInfo = animator.GetAnimatorTransitionInfo(LayerIndex);
			int _currentTransition = _transitionInfo.nameHash;
			int _currentTransitionUserName = animator.GetAnimatorTransitionInfo(LayerIndex).userNameHash;
			
			if (lastTransition != _currentTransition)
			{
				if (debug) Debug.Log("is in transition");
				
				// look for a username based transition
				if ( fsmStateLUT.ContainsKey(_currentTransitionUserName) )
				{ 
					FsmState _fsmState = fsmStateLUT[_currentTransitionUserName];
					if (Fsm.Fsm.ActiveState!=_fsmState)
					{
						SwitchState(Fsm.Fsm,_fsmState);
						// Only in 1.8
						//Fsm.Fsm.SwitchState(_fsmState);
						hasSwitchedState = true;
					}
				}
				
				// set state
				if (!hasSwitchedState && fsmStateLUT.ContainsKey(_currentTransition) )
				{
					FsmState _fsmState = fsmStateLUT[_currentTransition];
					if (Fsm.Fsm.ActiveState!=_fsmState)
					{
						SwitchState(Fsm.Fsm,_fsmState);
						// Only in 1.8
						//Fsm.Fsm.SwitchState(_fsmState);
						hasSwitchedState = true;
					}
				}
				
				
				if (!hasSwitchedState && debug) Debug.LogWarning("Fsm is missing animator transition name or username for hash:"+_currentTransition);
				
				// record change
				lastTransition = _currentTransition;
			}
			
		}
		
		// if we have not succeeded with any potential transitions, we look for states
		if (!hasSwitchedState)
		{
			#if UNITY_5
			int _currentState = animator.GetCurrentAnimatorStateInfo(LayerIndex).fullPathHash;
			#else
			int _currentState = animator.GetCurrentAnimatorStateInfo(LayerIndex).nameHash;
			#endif
			if (lastState != _currentState)
			{
				if (debug) Debug.Log("Net state switch");
				// set state
				if ( fsmStateLUT.ContainsKey(_currentState) )
				{
					FsmState _fsmState = fsmStateLUT[_currentState];
					if (Fsm.Fsm.ActiveState!=_fsmState)
					{
						SwitchState(Fsm.Fsm,_fsmState);
						// Only in 1.8
						//Fsm.Fsm.SwitchState(_fsmState);
					}
				}else{
					if (debug) Debug.LogWarning("Fsm is missing animator state hash:"+_currentState);
				}
				
				// record change
				lastState = _currentState;
			}
		}
		
	}
	
	void SwitchState(Fsm fsm, FsmState state)
	{
		MethodInfo switchState = fsm.GetType().GetMethod("SwitchState", BindingFlags.NonPublic | BindingFlags.Instance);
		if (switchState!=null)
		{
			switchState.Invoke(fsm , new object[] { state });
		}
	}
	
}




