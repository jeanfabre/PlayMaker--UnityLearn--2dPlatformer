//	(c) Jean Fabre, 2013 All rights reserved.
//	http://www.fabrejean.net
//  contact: http://www.fabrejean.net/contact.htm
//
// Version Alpha 0.1


using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

using HutongGames.PlayMaker;

public partial class PlayMakerUtils {

	public static PlayMakerFSM FindFsmOnGameObject(GameObject go,string fsmName)
	{
		if (go==null || string.IsNullOrEmpty(fsmName))
		{
			return null;
		}

		foreach(PlayMakerFSM _fsm in go.GetComponents<PlayMakerFSM>())
		{
			if (string.Equals(_fsm.FsmName,fsmName))
			{
				return _fsm;
			}
		}

		return null;
	}
}
