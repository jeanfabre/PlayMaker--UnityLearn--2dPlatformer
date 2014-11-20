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
	[Tooltip("Remove the specified range of elements from a PlayMaker ArrayList Proxy component")]
	public class ArrayListRemoveRange : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The zero-based index of the first element of the range of elements to remove. This value is between 0 and the array.count minus count (inclusive)")]
		public FsmInt index;

		[UIHint(UIHint.FsmInt)]
		[Tooltip("The number of elements to remove. This value is between 0 and the difference between the array.count minus the index ( inclusive )")]
		public FsmInt count;		
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the removeRange throw errors")]
		public FsmEvent failureEvent;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			index = null;
			count = null;
			failureEvent = null;
		}

		
		public override void OnEnter()
		{		
				if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				doArrayListRemoveRange();
			
			Finish();
		}
		
		
		public void doArrayListRemoveRange()
		{
			if (! isProxyValid()) 
				return;
			
			try
			{
				proxy.arrayList.RemoveRange(index.Value,count.Value);
			}catch(System.Exception e){
				Debug.LogError(e.Message);
				Fsm.Event(failureEvent);
			}

		}
	}
}