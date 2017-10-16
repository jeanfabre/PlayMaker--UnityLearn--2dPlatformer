// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 
/*
EcoMetaStart
{
	"script dependancies":[
		"Assets/PlayMaker Custom Actions/uGui/EventSystem/GetLastPointerDataInfo.cs"
	]
}
EcoMetaEnd
*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sends event when OnSubmit is called on the GameObject." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class uGuiOnSubmitEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnSubmitEvent is called")]
		public FsmEvent onSubmitEvent;


		GameObject _go;
		EventTrigger _trigger;
		EventTrigger.Entry entry;

		public override void Reset()
		{
			gameObject = null;
			onSubmitEvent = null;
		}
		
		public override void OnEnter()
		{
			_go =  Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go==null)
			{
				return;
			}

			_trigger = _go.GetComponent<EventTrigger>();

			if (_trigger == null)
			{
				_trigger = _go.AddComponent<EventTrigger>();
			}
				
			if (entry == null)
			{
				entry = new EventTrigger.Entry ();
			}

			entry.eventID = EventTriggerType.Submit;
			entry.callback.AddListener((data) => { OnSubmitDelegate((PointerEventData)data); });

			_trigger.triggers.Add(entry);
		}

		public override void OnExit()
		{
			entry.callback.RemoveAllListeners ();
			_trigger.triggers.Remove (entry);
		}


		void OnSubmitDelegate( PointerEventData data)
		{
			GetLastPointerDataInfo.lastPointeEventData = data;
			Fsm.Event(onSubmitEvent);
		}
	}
}

#endif