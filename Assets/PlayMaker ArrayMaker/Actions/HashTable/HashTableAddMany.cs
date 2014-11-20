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
	[Tooltip("Add key/value pairs to a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy).")]
	public class HashTableAddMany : HashTableActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[ActionSection("Data")]
		
		[CompoundArray("Count", "Key", "Value")]
	
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[Tooltip("The Key")]
		public FsmString[] keys;
		
		[RequiredField]
		[Tooltip("The value for that key")]
		public FsmVar[] variables;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when elements are added")]
		public FsmEvent successEvent;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when elements exists already")]
		public FsmEvent keyExistsAlreadyEvent;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			keys = null;
			variables = null;
			
			successEvent = null;
			keyExistsAlreadyEvent = null;
		}
		
		
		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
			{
				if (keyExistsAlreadyEvent != null)
				{
			
					foreach(FsmString _key in keys)
					{
						if (proxy.hashTable.ContainsKey(_key.Value))
						{
							Fsm.Event(keyExistsAlreadyEvent);
							Finish();
						}
					}
				}
				
				
				AddToHashTable();
				Fsm.Event(successEvent);
			}
			
			Finish();
		}
		
		
		public void AddToHashTable()
		{

			if (!isProxyValid()) 
				return;
			
			for(int i = 0;i<keys.Length;i++)
			{
				proxy.hashTable.Add(keys[i].Value,PlayMakerUtils.GetValueFromFsmVar(Fsm,variables[i]));
			}

		}
		
	}
}