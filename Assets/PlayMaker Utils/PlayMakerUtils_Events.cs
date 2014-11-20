//	(c) Jean Fabre, 2013 All rights reserved.
//	http://www.fabrejean.net
//  contact: http://www.fabrejean.net/contact.htm
//
// Version Alpha 0.1

// INSTRUCTIONS
// This set of utils is here to help custom action development, and scripts in general that wants to connect and work with PlayMaker API.


using UnityEngine;

using HutongGames.PlayMaker;

public partial class PlayMakerUtils {
	

	public static void SendEventToGameObject(PlayMakerFSM fromFsm,GameObject target,string fsmEvent)
	{
		SendEventToGameObject(fromFsm,target,fsmEvent,null);
	}

	public static void SendEventToGameObject(PlayMakerFSM fromFsm,GameObject target,string fsmEvent,FsmEventData eventData)
	{
		if (eventData!=null)
		{
			HutongGames.PlayMaker.Fsm.EventData = eventData;
		}
		
		FsmEventTarget _eventTarget = new FsmEventTarget();
		_eventTarget.excludeSelf = false;
		FsmOwnerDefault owner = new FsmOwnerDefault();
		owner.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
		owner.GameObject = new FsmGameObject();
		owner.GameObject.Value = target;
		_eventTarget.gameObject = owner;
		_eventTarget.target = FsmEventTarget.EventTarget.GameObject;	
			
		_eventTarget.sendToChildren = false;
		
		fromFsm.Fsm.Event(_eventTarget,fsmEvent);


	}


	public static bool DoesTargetImplementsEvent(FsmEventTarget target,string eventName)
	{

		if (target.target == FsmEventTarget.EventTarget.BroadcastAll)
		{
			return FsmEvent.IsEventGlobal(eventName);
		}

		if (target.target == FsmEventTarget.EventTarget.FSMComponent)
		{
			return DoesFsmImplementsEvent(target.fsmComponent,eventName);
		}

		if (target.target == FsmEventTarget.EventTarget.GameObject)
		{
			return DoesGameObjectImplementsEvent(target.gameObject.GameObject.Value,eventName);
		}

		if (target.target == FsmEventTarget.EventTarget.GameObjectFSM)
		{
			return DoesGameObjectImplementsEvent(target.gameObject.GameObject.Value,target.fsmName.Value, eventName);
		}

		if (target.target == FsmEventTarget.EventTarget.Self)
		{
			Debug.LogError("Self target not supported yet");
		}

		if (target.target == FsmEventTarget.EventTarget.SubFSMs)
		{
			Debug.LogError("subFsms target not supported yet");
		}

		if (target.target == FsmEventTarget.EventTarget.HostFSM)
		{
			Debug.LogError("HostFSM target not supported yet");
		}

		return false;
	}

	public static bool DoesGameObjectImplementsEvent(GameObject go, string fsmEvent)
	{
		if (go==null || string.IsNullOrEmpty(fsmEvent))
		{
			return false;
		}
		
		foreach(PlayMakerFSM _fsm in go.GetComponents<PlayMakerFSM>())
		{
			if (DoesFsmImplementsEvent(_fsm,fsmEvent))
			{
				return true;
			}
		}
		return false;
	}

	public static bool DoesGameObjectImplementsEvent(GameObject go,string fsmName, string fsmEvent)
	{
		if (go==null || string.IsNullOrEmpty(fsmEvent))
		{
			return false;
		}

		bool checkFsmName = !string.IsNullOrEmpty(fsmName);

		foreach(PlayMakerFSM _fsm in go.GetComponents<PlayMakerFSM>())
		{
			if ( checkFsmName &&  string.Equals(_fsm,fsmName) )
			{
				if (DoesFsmImplementsEvent(_fsm,fsmEvent))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool DoesFsmImplementsEvent(PlayMakerFSM fsm, string fsmEvent)
	{

		if (fsm==null || string.IsNullOrEmpty(fsmEvent))
		{
			return false;
		}

		foreach(FsmTransition _transition in fsm.FsmGlobalTransitions)
		{
			if (_transition.EventName.Equals(fsmEvent))
			{
				return true;
			}
		}
		
		foreach(FsmState _state in fsm.FsmStates)
		{
			
			foreach(FsmTransition _transition in _state.Transitions)
			{
				
				if (_transition.EventName.Equals(fsmEvent))
				{
					return true;
				}
			}
		}
		
		return false;
	}

	/*
	public bool DoesTargetMissEventImplementation(PlayMakerFSM fsm, string fsmEvent)
	{
		if (DoesTargetImplementsEvent(fsm,fsmEvent))
		{
			return false;
		}
		
		foreach(FsmEvent _event in fsm.FsmEvents)
		{
			if (_event.Name.Equals(fsmEvent))
			{
				return true;
			}
		}
		
		return false;
	}
*/
}
