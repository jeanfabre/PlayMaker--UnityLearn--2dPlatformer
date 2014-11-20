//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerHashTableProxy script onto a GameObject, and define a unique name for reference if several PlayMakerHashTableProxy coexists on that GameObject.
// In this Action interface, link that GameObject in "hashTableObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Edit a key from a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy)")]
	public class HashTableEditKey : HashTableActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[Tooltip("The Key value to edit")]
		public FsmString key;
	
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[Tooltip("The Key value to edit")]
		public FsmString newKey;
		
		[ActionSection("Result")]
		
		[Tooltip("Event sent if this HashTable key does not exists")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent keyNotFoundEvent;
		
		[Tooltip("Event sent if this HashTable already contains the new key")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent newKeyExistsAlreadyEvent;	

		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			key = null;
			newKey = null;
			
			keyNotFoundEvent = null;
			newKeyExistsAlreadyEvent = null;
		}
		
		
		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
			{
				EditHashTableKey();
			}
			
			Finish();
		}

		public void EditHashTableKey()
		{

			if (!isProxyValid()) 
				return;
			
			if (!proxy.hashTable.ContainsKey(key.Value))
			{
				Fsm.Event(keyNotFoundEvent);
				return;
			}
			
			if (proxy.hashTable.ContainsKey(newKey.Value))
			{
				Fsm.Event(newKeyExistsAlreadyEvent);
				return;
			}
			
			object _val = proxy.hashTable[key.Value];
			proxy.hashTable[newKey.Value] = _val;
			proxy.hashTable.Remove(key.Value);
			
		}
		
		
	}
}