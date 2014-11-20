//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerHashTable coexists on that GameObject.
// In this Action interface, link that GameObject in "gameObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Concat joins two or more hashTable proxy components. if a target is specified, the method use the target store the concatenation, else the ")]
	public class HashTableConcat : HashTableActions
	{
		[ActionSection("Storage")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component to store the concatenation ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[ActionSection("HashTables to concatenate")]
		
		[CompoundArray("HashTables", "HashTable GameObject", "Reference")]
		
		[RequiredField]
		[Tooltip("The GameObject with the PlayMaker HashTable Proxy component to copy to")]
		[ObjectType(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault[] hashTableGameObjectTargets;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component to copy to ( necessary if several component coexists on the same GameObject")]
		public FsmString[] referenceTargets;		
		
		[Tooltip("Overwrite existing key with new values")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool overwriteExistingKey;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			hashTableGameObjectTargets = null;
			referenceTargets = null;
			overwriteExistingKey = null;

		}

		
		public override void OnEnter()
		{

			if ( SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
					DoHashTableConcat(proxy.hashTable);
			
			Finish();
		}

		
		public void DoHashTableConcat(Hashtable source)
		{
			if (! isProxyValid()) 
				return;
			
			
		
			for(int i=0;i<hashTableGameObjectTargets.Length;i++){
				if ( SetUpHashTableProxyPointer( Fsm.GetOwnerDefaultTarget(hashTableGameObjectTargets[i]),referenceTargets[i].Value) ){
						if (isProxyValid()){
						
						
						
						foreach(object key in proxy.hashTable.Keys){
							
							if (source.ContainsKey(key) ) {
								if (overwriteExistingKey.Value == true){
									source[key] = proxy.hashTable[key];
								}
							}else{
								source[key] = proxy.hashTable[key];
							}
							
						}
							
					}
				}
			}
				
		}
	}
}