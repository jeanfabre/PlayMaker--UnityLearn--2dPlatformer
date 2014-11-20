using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;

public class PlayMakerUGuiSceneProxy : MonoBehaviour {


	public static PlayMakerFSM fsm;

	// Use this for initialization
	void Start () {
		PlayMakerUGuiSceneProxy.fsm = GetComponent<PlayMakerFSM>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
