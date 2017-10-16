using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;

public class PlayMakerUGuiDropEventsProxy : MonoBehaviour, IDropHandler
{

	public bool debug = false;

	public PlayMakerEventTarget eventTarget = new PlayMakerEventTarget(true);
	
	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onDropEvent = new PlayMakerEvent("UGUI / ON DROP");
	
	public void OnDrop (PointerEventData data) {

		if (debug)
		{
			UnityEngine.Debug.Log("OnDrop "+data.pointerId+" on "+this.gameObject.name);
		}

		GetLastPointerDataInfo.lastPointeEventData = data;
		onDropEvent.SendEvent(null,eventTarget);
	}
}
