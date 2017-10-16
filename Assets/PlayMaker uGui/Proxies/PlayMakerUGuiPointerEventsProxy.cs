using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

using HutongGames.PlayMaker.Ecosystem.Utils;

public class PlayMakerUGuiPointerEventsProxy : MonoBehaviour, 
				IPointerClickHandler, 
				IPointerDownHandler, 
				IPointerEnterHandler,
				IPointerExitHandler,
				IPointerUpHandler
{

	public bool debug = false;

	public PlayMakerEventTarget eventTarget;

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onClickEvent = new PlayMakerEvent("UGUI / ON POINTER CLICK");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onDownEvent = new PlayMakerEvent("UGUI / ON POINTER DOWN");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onEnterEvent = new PlayMakerEvent("UGUI / ON POINTER ENTER");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onExitEvent = new PlayMakerEvent("UGUI / ON POINTER EXIT");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onUpEvent = new PlayMakerEvent("UGUI / ON POINTER UP");
	
	public void OnPointerClick (PointerEventData data) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnPointerClick "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onClickEvent.SendEvent(null,eventTarget);
	}

	public void OnPointerDown (PointerEventData data) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnPointerDown "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onDownEvent.SendEvent(null,eventTarget);
	}

	public void OnPointerEnter (PointerEventData data) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnPointerEnter "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onEnterEvent.SendEvent(null,eventTarget);
	}

	public void OnPointerExit (PointerEventData data) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnPointerExit "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onExitEvent.SendEvent(null,eventTarget);
	}

	public void OnPointerUp (PointerEventData data) {
		if (debug)
		{
			UnityEngine.Debug.Log("OnPointerUp "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onUpEvent.SendEvent(null,eventTarget);
	}

}
