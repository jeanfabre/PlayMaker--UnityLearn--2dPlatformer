using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayMakerUGuiDragEventsExecutionProxy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	public bool AutoDetect;
	public Component[] _beginDragTargets,_dragTargets,_endDragTargets;

	void Start() {
		if (AutoDetect)
		{
			_beginDragTargets = this.transform.parent.GetComponentsInParent(typeof(IBeginDragHandler));
			_dragTargets = this.transform.parent.GetComponentsInParent(typeof(IDragHandler));
			_endDragTargets = this.transform.parent.GetComponentsInParent(typeof(IEndDragHandler));
		}
	}

	
	public void OnBeginDrag (PointerEventData data) {

		foreach(IBeginDragHandler _go in _beginDragTargets)
		{
			ExecuteEvents.beginDragHandler(_go, data);
		}
	}
	
	public void OnDrag (PointerEventData data) {

		foreach(IDragHandler _go in _dragTargets)
		{
			ExecuteEvents.dragHandler(_go, data);
		}
	}
	
	public void OnEndDrag (PointerEventData data) {

		foreach(IEndDragHandler _go in _endDragTargets)
		{
			ExecuteEvents.endDragHandler(_go, data);
		}

	}
}

