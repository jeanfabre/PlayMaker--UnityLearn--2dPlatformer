using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;

public class PlayMakerUGuiDragEventsProxy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public bool debug = false;

	public PlayMakerEventTarget eventTarget = new PlayMakerEventTarget(true);

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onBeginDragEvent= new PlayMakerEvent("UGUI / ON BEGIN DRAG");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onDragEvent = new PlayMakerEvent("UGUI / ON DRAG");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onEndDragEvent= new PlayMakerEvent("UGUI / ON END DRAG");
	

	public void OnBeginDrag (PointerEventData data) {

		if (debug)
		{
			UnityEngine.Debug.Log("OnBeginDrag "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onBeginDragEvent.SendEvent(null,eventTarget);
	}

	public void OnDrag (PointerEventData data) {

		if (debug)
		{
			UnityEngine.Debug.Log("OnDrag "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onDragEvent.SendEvent(null,eventTarget);
	}
	
	public void OnEndDrag (PointerEventData data) {

		if (debug)
		{
			UnityEngine.Debug.Log("OnEndDrag "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onEndDragEvent.SendEvent(null,eventTarget);
	}

}
