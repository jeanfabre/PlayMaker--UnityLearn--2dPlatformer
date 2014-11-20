//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference.
// This action will find a PlayMakerArrayList by its reference.
// NOTE: This will find and reference the GameObject where the ArrayList component is attached. It is still necessary to keep track of the reference
// ArrayList actions requires both a gameObject AND a reference. 
// If only one arrayList is attached to a gameObject, and the reference name is left blank, referencing is not necessary.
// I really wonder why we can't maintain proper pointers to a particular component in Unity... this is odd.


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Finds an ArrayList by reference. Warning: this function can be very slow.")]
	public class FindArrayList : CollectionsActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component")]
		public FsmString ArrayListReference;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[Tooltip("Store the GameObject hosting the PlayMaker ArrayList Proxy component here")]
		public FsmGameObject store;
		
		public FsmEvent foundEvent;
		public FsmEvent notFoundEvent;

		public override void Reset()
		{
			ArrayListReference = "";
			store = null;
			foundEvent = null;
			notFoundEvent = null;
		}

		public override void OnEnter()
		{

			PlayMakerArrayListProxy[] proxies = UnityEngine.Object.FindObjectsOfType(typeof(PlayMakerArrayListProxy)) as PlayMakerArrayListProxy[];
	        foreach (PlayMakerArrayListProxy iProxy in proxies) {
	         	if (iProxy.referenceName == ArrayListReference.Value){
					store.Value = iProxy.gameObject;
					Fsm.Event(foundEvent);
					return;
				}
	        }
			
			store.Value = null;
			Fsm.Event(notFoundEvent);
			
			Finish();
		}

	}
}