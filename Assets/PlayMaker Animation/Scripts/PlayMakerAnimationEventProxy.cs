using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;

public class PlayMakerAnimationEventProxy : MonoBehaviour {


	public void SendPlayMakerEvent()
	{
		PlayMakerUtils.SendEventToGameObject(this.GetComponent<PlayMakerFSM>(),this.gameObject,"ANIMATION EVENT");
	}
}
