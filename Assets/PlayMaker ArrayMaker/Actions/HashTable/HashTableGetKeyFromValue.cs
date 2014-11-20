//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Return the key for a value ofna PlayMaker hashtable Proxy component. It will return the first entry found.")]
	public class HashTableGetKeyFromValue : HashTableActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[ActionSection("Value")]
		[RequiredField]
		[Tooltip("The value to search")]
		public FsmVar theValue;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The key of that value")]
		public FsmString result;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when value is found")]
		public FsmEvent KeyFoundEvent;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when value is not found")]
		public FsmEvent KeyNotFoundEvent;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
		}

		
		public override void OnEnter()
		{
			if ( SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				SortHashTableByValues();
			
			Finish();
		}

		
		public void SortHashTableByValues()
		{
			if (! isProxyValid()) 
				return;
			
			var _val = PlayMakerUtils.GetValueFromFsmVar(this.Fsm,theValue);
			
			foreach (DictionaryEntry entry in proxy.hashTable)
		    {
		        if (entry.Value.Equals(_val))
		        {
		            result.Value = (string)entry.Key;
					Fsm.Event(KeyFoundEvent);

					return;
		        }
		    }
			
			Fsm.Event(KeyNotFoundEvent);		
		}
	}
}