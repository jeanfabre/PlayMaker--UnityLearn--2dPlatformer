//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Use this action to destroy a PlayMakerHashTableProxy component.
// Note: you need to reference an FsmObject of type PlayMakerHashTableProxy and a FsmString representing the reference name for a given PlayMakerHashTableProxy
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Destroys a PlayMakerHashTableProxy Component of a Game Object.")]
	public class DestroyHashTable : HashTableActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker HashTable proxy component ( necessary if several component coexists on the same GameObject")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the HashTable proxy component is destroyed")]
		public FsmEvent successEvent;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the HashTable proxy component was not found")]
		public FsmEvent notFoundEvent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			successEvent = null;
			notFoundEvent = null;
	
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (SetUpHashTableProxyPointer(go,reference.Value))
			{
				DoDestroyHashTable(go);
			}else{
				Fsm.Event(notFoundEvent);
			}
			
			Finish();
		}

		void DoDestroyHashTable(GameObject go)
		{
			PlayMakerHashTableProxy[] proxies = proxy.GetComponents<PlayMakerHashTableProxy>();
			foreach (PlayMakerHashTableProxy iProxy in proxies) {
        		if (iProxy.referenceName == reference.Value){
					Object.Destroy(iProxy);
					Fsm.Event(successEvent);
					return;
				}
		 	}
			
			Fsm.Event(notFoundEvent);
		}
	}
}