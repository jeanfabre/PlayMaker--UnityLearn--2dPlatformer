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
	[Tooltip("Add an key/value pair to a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy).")]
	public class HashTableAdd : HashTableActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[ActionSection("Data")]
		
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[Tooltip("The Key value for that hash set")]
		public FsmString key;
		
		[RequiredField]
		[Tooltip("The variable to add.")]
		public FsmVar variable;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when element is added")]
		public FsmEvent successEvent;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when element exists already")]
		public FsmEvent keyExistsAlreadyEvent;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			key = null;
			variable = null;
			
			successEvent = null;
			keyExistsAlreadyEvent = null;
		}
		
		
		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
			{
				if ( proxy.hashTable.ContainsKey(key.Value) )
				{
					Fsm.Event(keyExistsAlreadyEvent);
				}else{
					AddToHashTable();
					Fsm.Event(successEvent);
				}
				
			}
			Finish();
		}
		
		
		public void AddToHashTable()
		{

			if (!isProxyValid()) 
				return;
			
			proxy.hashTable.Add(key.Value,PlayMakerUtils.GetValueFromFsmVar(Fsm,variable));
			
		}
		
	}
}