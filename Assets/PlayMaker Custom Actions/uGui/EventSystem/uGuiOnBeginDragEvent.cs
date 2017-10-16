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
	[Tooltip("Sends event when GameObject is called before a drag is started." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class uGuiOnBeginDragEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnBeginDrag is called")]
		public FsmEvent onBeginDragEvent;


		GameObject _go;
		EventTrigger _trigger;
		EventTrigger.Entry entry;

		public override void Reset()
		{
			gameObject = null;
			onBeginDragEvent = null;
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

			entry.eventID = EventTriggerType.BeginDrag;
			entry.callback.AddListener((data) => { OnBeginDragDelegate((PointerEventData)data); });

			_trigger.triggers.Add(entry);
		}

		public override void OnExit()
		{
			entry.callback.RemoveAllListeners ();
			_trigger.triggers.Remove (entry);
		}


		void OnBeginDragDelegate( PointerEventData data)
		{
			GetLastPointerDataInfo.lastPointeEventData = data;
			Fsm.Event(onBeginDragEvent);
		}
	}
}

#endif