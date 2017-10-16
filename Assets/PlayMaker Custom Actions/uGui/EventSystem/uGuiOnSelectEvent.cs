// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

#if UNITY_5_6_OR_NEWER   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sends event when Called by the EventSystem when a Select event occurs." +
		"\n Use GetLastPointerDataInfo action to get info from the event")]
	public class uGuiOnSelectEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnSelect is called")]
		public FsmEvent onSelectEvent;


		GameObject _go;
		EventTrigger _trigger;
		EventTrigger.Entry entry;

		public override void Reset()
		{
			gameObject = null;
			onSelectEvent = null;
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

			entry.eventID = EventTriggerType.Select;
			entry.callback.AddListener((data) => { OnSelectDelegate((PointerEventData)data); });

			_trigger.triggers.Add(entry);
		}

		public override void OnExit()
		{
			entry.callback.RemoveAllListeners ();
			_trigger.triggers.Remove (entry);
		}


		void OnSelectDelegate( PointerEventData data)
		{
			GetLastPointerDataInfo.lastPointeEventData = data;
			Fsm.Event(onSelectEvent);
		}
	}
}

#endif