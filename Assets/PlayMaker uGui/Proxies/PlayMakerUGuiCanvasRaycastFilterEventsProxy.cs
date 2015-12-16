using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class PlayMakerUGuiCanvasRaycastFilterEventsProxy : MonoBehaviour, ICanvasRaycastFilter
{

	public bool RayCastingEnabled = true;

	#region ICanvasRaycastFilter implementation
	public bool IsRaycastLocationValid (Vector2 sp, Camera eventCamera)
	{
		return RayCastingEnabled;
	}
	#endregion


	
}
