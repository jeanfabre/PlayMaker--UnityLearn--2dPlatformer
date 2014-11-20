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
	[Tooltip("Remove an item by key ( key/value pairs) in a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy).")]
	public class HashTableRemove : HashTableActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[RequiredField]
		[Tooltip("The item key in that hashTable")]
		public FsmString key;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			key = null;

		}
		
		
		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
				doHashTableRemove();

			Finish();
		}
		
		
		public void doHashTableRemove()
		{
			if (!isProxyValid()) 
				return;
			
			proxy.hashTable.Remove(key.Value);
		}
		
	}
}