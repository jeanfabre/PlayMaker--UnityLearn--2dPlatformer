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
	[Tooltip("Gets an item from a PlayMaker ArrayList Proxy component")]
	public class ArrayListGet : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
	
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The index to retrieve the item from")]
		public FsmInt atIndex;
		

		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmVar result;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the action fails ( likely and index is out of range exception)")]
		public FsmEvent failureEvent;
		

		public override void Reset()
		{
			atIndex = null;
			gameObject = null;
			
			failureEvent = null;
			
			result = null;
			
		}
		
		
		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				GetItemAtIndex();

			Finish();
		}
		
		
		
		public void GetItemAtIndex(){
			
			if (! isProxyValid())
				return;
		
			
			object element = null;
			
			try{
				element = proxy.arrayList[atIndex.Value];
			}catch(System.Exception e){
				Debug.Log(e.Message);
				Fsm.Event(failureEvent);
				return;
			}
			
			PlayMakerUtils.ApplyValueToFsmVar(Fsm,result,element);
		}
	}
}