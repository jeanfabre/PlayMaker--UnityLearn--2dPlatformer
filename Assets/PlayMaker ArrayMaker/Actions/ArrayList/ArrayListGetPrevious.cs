//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Each time this action is called it gets the previous item from a PlayMaker ArrayList Proxy component. \n" +
		"This lets you quickly loop backward through all the children of an object to perform actions on them.\n" +
		 "NOTE: To get to specific item use ArrayListGet instead.")]
	public class ArrayListGetPrevious : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
	
		[Tooltip("Set to true to force iterating from the last item. This variable will be set to false as it carries on iterating, force it back to true if you want to renter this action back to the last item.")]
		[UIHint(UIHint.Variable)]
		public FsmBool reset;
		
		[Tooltip("From where to start iteration, leave to 0 to start from the end. index is relative to the last item, so if the start index is 2, this will start 2 items before the last item.")]
		public FsmInt startIndex;
		
		[Tooltip("When to end iteration, leave to 0 to iterate until the beginning, index is relative to the last item.")]
		public FsmInt endIndex;
		
		[Tooltip("Event to send to get the previous item.")]
		public FsmEvent loopEvent;

		[Tooltip("Event to send when there are no more items.")]
		public FsmEvent finishedEvent;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the action fails ( likely and index is out of range exception)")]
		public FsmEvent failureEvent;
		
	
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmInt currentIndex;
		
		[UIHint(UIHint.Variable)]
		public FsmVar result;
		
		
		// increment that index as we loop through item
		private int nextItemIndex = 0;
		
		private int countBase = 0;
		
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
			
			currentIndex= null;
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
				if ( ! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				{
					Fsm.Event(failureEvent);
					
					Finish();
				}
				
				countBase = proxy.arrayList.Count-1;
				
				if (startIndex.Value>0)
				{
					nextItemIndex = startIndex.Value;
				}
				
			}
			
			DoGetPreviousItem();
			
			Finish();
		}
		

		void DoGetPreviousItem()
		{

			// no more children?
			// check first to avoid errors.

			if (nextItemIndex > countBase)
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
			if (nextItemIndex >= countBase)
			{
				nextItemIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}
			
			if (endIndex.Value>0 && nextItemIndex > (countBase - endIndex.Value))
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
		
			currentIndex.Value = countBase - nextItemIndex;
	
			object element = null;
			
			try{
				element = proxy.arrayList[currentIndex.Value];
			}catch(System.Exception e){
				Debug.LogError(e.Message);
				Fsm.Event(failureEvent);
				return;
			}
				
			PlayMakerUtils.ApplyValueToFsmVar(Fsm,result,element);

		}
	}
}