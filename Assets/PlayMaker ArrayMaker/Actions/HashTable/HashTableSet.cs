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
	[Tooltip("Set an key/value pair to a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy)")]
	public class HashTableSet : HashTableActions
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
		[Tooltip("The Key value for that hash set")]
		public FsmString key;
	
		/*
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set Value if key exists already")]
		public FsmBool setValueIfKeyExists;	
		*/
		
		
		[ActionSection("Result")]
		
		[Tooltip("The variable to set.")]
		public FsmVar variable;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			key = null;
			//setValueIfKeyExists = null;
			variable = null;
		}
		
		
		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
			{
				SetHashTable();
			}
			
			Finish();
		}

		public void SetHashTable()
		{

			if (!isProxyValid()) 
				return;
			
			proxy.hashTable[key.Value] = PlayMakerUtils.GetValueFromFsmVar(Fsm,variable);
			
		}
		
		
	}
}