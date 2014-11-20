//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Each time this action is called it gets the next item from a PlayMaker HashTable Proxy component. \n" +
		"This lets you quickly loop through all the children of an object to perform actions on them.\n" +
		 "NOTE: To get to specific item use HashTableGet instead.")]
	public class HashTableGetNext : HashTableActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;

			
		[Tooltip("Set to true to force iterating from the first item. This variable will be set to false as it carries on iterating, force it back to true if you want to renter this action back to the first item.")]
		[UIHint(UIHint.Variable)]
		public FsmBool reset;
		
		[Tooltip("From where to start iteration, leave to 0 to start from the beginning")]
		public FsmInt startIndex;
		
		[Tooltip("When to end iteration, leave to 0 to iterate until the end")]
		public FsmInt endIndex;
		
		[Tooltip("Event to send to get the next item.")]
		public FsmEvent loopEvent;

		[Tooltip("Event to send when there are no more items.")]
		public FsmEvent finishedEvent;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the action fails ( likely and index is out of range exception)")]
		public FsmEvent failureEvent;
		
	
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmString key;
		
		[UIHint(UIHint.Variable)]
		public FsmVar result;
		
		
		private ArrayList _keys;
		
		// increment that index as we loop through item
		private int nextItemIndex = 0;
		
		
		public override void Reset()
		{
		
			gameObject = null;
			reference = null;
			
			reset = null;
			
			startIndex = null;
			endIndex = null;
			
			loopEvent = null;
			finishedEvent = null;
	
			failureEvent = null;
			
			result = null;
			
		}
		
		
		public override void OnEnter()
		{
			if (reset.Value)
			{
				reset.Value =  false;
				nextItemIndex = 0;
			}
			
			if (nextItemIndex == 0)
			{
				if ( ! SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				{
					Fsm.Event(failureEvent);
					
					Finish();
				}
				
				_keys = new ArrayList(proxy.hashTable.Keys);
					
				if (startIndex.Value>0)
				{
					nextItemIndex = startIndex.Value;
				}
			}
			
			DoGetNextItem();
			
			Finish();
		}
		

		void DoGetNextItem()
		{

			// no more children?
			// check first to avoid errors.

			if (nextItemIndex >= _keys.Count)
			{
				nextItemIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}

			// get next item

			GetItemAtIndex();


			// no more items?
			// check a second time to avoid process lock and possible infinite loop if the action is called again.
			// Practically, this enabled calling again this state and it will start again iterating from the first child.
			if (nextItemIndex >= _keys.Count)
			{
				nextItemIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}
			
			if (endIndex.Value>0 && nextItemIndex>= endIndex.Value)
			{
				nextItemIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}

			// iterate the next child
			nextItemIndex++;

			if (loopEvent != null)
			{
				Fsm.Event(loopEvent);
			}
		}
		
		
		public void GetItemAtIndex(){
			
			
			if (! isProxyValid())
				return;
		
	
			object element = null;
			
			try{
				element = proxy.hashTable[_keys[nextItemIndex]];
			}catch(System.Exception e){
				Debug.LogError(e.Message);
				Fsm.Event(failureEvent);
				return;
			}
			
			key.Value = (string)_keys[nextItemIndex];
			
			PlayMakerUtils.ApplyValueToFsmVar(Fsm,result,element);

		}
	}
}