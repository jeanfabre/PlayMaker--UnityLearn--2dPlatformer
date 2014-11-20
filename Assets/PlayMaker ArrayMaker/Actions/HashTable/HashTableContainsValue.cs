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
	[Tooltip("Check if a value exists in a PlayMaker HashTable Proxy component (PlayMakerHashTablePRoxy)")]
	public class HashTableContainsValue : HashTableActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component (necessary if several component coexists on the same GameObject)")]
		public FsmString reference;
		
		[Tooltip("The variable to check for.")]
		public FsmVar variable;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the test")]
		public FsmBool containsValue;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when value is found")]
		public FsmEvent valueFoundEvent;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger when value is not found")]
		public FsmEvent valueNotFoundEvent;
		
		
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			containsValue = null;
			valueFoundEvent = null;
			valueNotFoundEvent= null;
			variable = null;
		}
		
		
		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
				doContainsValue();
			
			Finish();
		}
		
		
		public void doContainsValue()
		{

			if (!isProxyValid()) 
				return;
			
			containsValue.Value = proxy.hashTable.ContainsValue(PlayMakerUtils.GetValueFromFsmVar(Fsm,variable));
			
			if (containsValue.Value){
				Fsm.Event(valueFoundEvent);
			}else{
				Fsm.Event(valueNotFoundEvent);
			}
			
			
		}// doContainsValue	
	}
}