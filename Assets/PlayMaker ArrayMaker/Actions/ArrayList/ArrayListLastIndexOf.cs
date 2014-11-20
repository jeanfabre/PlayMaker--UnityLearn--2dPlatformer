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
	[Tooltip("Return the Last index occurence of an item from a PlayMaker Array List Proxy component. Can search within a range")]
	public class ArrayListLastIndexOf : ArrayListActions
	{
		
		[ActionSection("Set up")]
	
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		
		[Tooltip("Optional start index to search from: set to 0 to ignore")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt startIndex;
		
		[Tooltip("Optional amount of elements to search within: set to 0 to ignore")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt count;

		
		[ActionSection("Data")]
		
		[RequiredField]
		[Tooltip("The variable to get the index of.")]
		public FsmVar variable;
		
		[ActionSection("Result")]
				
		[RequiredField]
		[Tooltip("The index of the last item described below")]
		public FsmInt lastIndexOf;
		
				
		[Tooltip("Event sent if this arraList contains that element ( described below)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent itemFound;	

		[Tooltip("Event sent if this arraList does not contains that element ( described below)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent itemNotFound;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Optional Event to trigger if the action fails ( likely an out of range exception when using wrong values for index and/or count)")]
		public FsmEvent failureEvent;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			startIndex = null;
			count = null;
			lastIndexOf = null;
			itemFound = null;
			itemNotFound = null;
			failureEvent = null;
		}
		
		
		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				DoArrayListLastIndex();
			
			Finish();
		}
		
		
		
		public void DoArrayListLastIndex()
		{
			if (! isProxyValid() ) 
				return;
			
			object item = PlayMakerUtils.GetValueFromFsmVar(Fsm,variable);
			
			int index = -1;
			
			try{
				if (startIndex.Value == 0 && count.Value == 0){
					
					index = PlayMakerUtils_Extensions.LastIndexOf(proxy.arrayList,item);
		
				}else if (count.Value == 0 ){
					if (startIndex.Value<0 || startIndex.Value>=proxy.arrayList.Count){
						Debug.LogError("start index out of range");
						return;
					}
						index = PlayMakerUtils_Extensions.LastIndexOf(proxy.arrayList,item,startIndex.Value);
					
				}else{
					if (startIndex.Value<0 || startIndex.Value>=(proxy.arrayList.Count-count.Value)){
						Debug.LogError("start index and count out of range");
						return;
					}
					index = PlayMakerUtils_Extensions.LastIndexOf(proxy.arrayList,item,startIndex.Value,count.Value);
				}
			}catch(System.Exception e){
				Debug.LogError(e.Message);
				Fsm.Event(failureEvent);
				return;
			}
			
			lastIndexOf.Value = index;
			if (index==-1){
				Fsm.Event(itemNotFound);
			}else{
				Fsm.Event(itemFound);
			}
			
		}
	}
}