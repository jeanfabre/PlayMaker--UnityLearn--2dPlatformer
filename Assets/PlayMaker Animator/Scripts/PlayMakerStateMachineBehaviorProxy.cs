using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;

using HutongGames.PlayMaker.Ecosystem.Utils;

#if UNITY_5
public class PlayMakerStateMachineBehaviorProxy : StateMachineBehaviour {

	public bool debug;

	public PlayMakerEventTarget eventTarget;

	public PlayMakerEvent StateEnterEvent;

	public PlayMakerEvent StateExitEvent;

	//[Tooltip("Unfortunatly, there is no way yet to know the name of state. so you can manually set this here.")]
	public string eventData;

	string[] stateNames;
 	int[] stateHashes;


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		if (debug) Debug.Log(animator.gameObject.name+": Animator : OnStateEnter : data :"+eventData);

		if (eventTarget.eventTarget== ProxyEventTarget.Owner)
		{
			eventTarget.gameObject = animator.gameObject;
		}

		Fsm.EventData.StringData = eventData;

		StateEnterEvent.SendEvent(null,eventTarget);
		
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (debug) Debug.Log(animator.gameObject.name+": Animator : OnStateExit : data :"+eventData);

		if (eventTarget.eventTarget== ProxyEventTarget.Owner)
		{
			eventTarget.gameObject = animator.gameObject;
		}

		Fsm.EventData.StringData = eventData;

		StateExitEvent.SendEvent(null,eventTarget);
	}

	/*
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
	}
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
	}
	override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
	}
	*/

}
#endif
