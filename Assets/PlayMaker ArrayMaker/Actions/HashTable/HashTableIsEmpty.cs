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
	[Tooltip("Check if an HashTable Proxy component is empty.")]
	public class HashTableIsEmpty : ArrayListActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

		[ActionSection("Result")]
		
		[Tooltip("Store in a bool wether it is empty or not")]
		[UIHint(UIHint.Variable)]
		public FsmBool isEmpty;	
		
		[Tooltip("Event sent if this HashTable is empty ")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isEmptyEvent;	

		[Tooltip("Event sent if this HashTable is not empty")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isNotEmptyEvent;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;

			isEmpty = null;
			isEmptyEvent = null;
			isNotEmptyEvent = null;
		}
		
		public override void OnEnter()
		{
			PlayMakerHashTableProxy _proxy = GetHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value,true);
			
			bool _isEmpty = _proxy.hashTable.Count==0;
			
			isEmpty.Value = _isEmpty;
			if(_isEmpty){
				Fsm.Event(isEmptyEvent);
			}else{
				Fsm.Event(isNotEmptyEvent);
			}
			
			Finish();
		}
		
	}
}