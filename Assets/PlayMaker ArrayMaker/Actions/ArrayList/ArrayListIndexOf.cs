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
	[Tooltip("Return the index of an item from a PlayMaker Array List Proxy component. Can search within a range")]
	public class ArrayListIndexOf : ArrayListActions
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
		
		[Tooltip("The index of the item described below")]
		[UIHint(UIHint.Variable)]
		public FsmInt indexOf;
			
		[Tooltip("Optional Event sent if this arrayList contains that element ( described below)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent itemFound;	

		[Tooltip("Optional Event sent if this arrayList does not contains that element ( described below)")]
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
			itemFound = null;
			itemNotFound = null;
			
			variable = null;
			
		}
		
		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer( Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				DoArrayListIndexOf();
			
			Finish();
		}
		
		
		public void DoArrayListIndexOf()
		{
			if (! isProxyValid() ) 
				return;
			
			object item = PlayMakerUtils.GetValueFromFsmVar(Fsm,variable);
		
			int indexOfResult = -1;
			
			try{
				if (startIndex.IsNone){
					UnityEngine.Debug.Log("hello");
					indexOfResult = PlayMakerUtils_Extensions.IndexOf(proxy.arrayList,item);
		
				}else if (count.IsNone || count.Value == 0 ){
					if (startIndex.Value<0 || startIndex.Value>=proxy.arrayList.Count){
						LogError("start index out of range");
						return;
					}
						indexOfResult = PlayMakerUtils_Extensions.IndexOf(proxy.arrayList,item);
					
				}else{
					if (startIndex.Value<0 || startIndex.Value>=(proxy.arrayList.Count-count.Value)){
						LogError("start index and count out of range");
						return;
					}
					indexOfResult = PlayMakerUtils_Extensions.IndexOf(proxy.arrayList,item);
				}
			}catch(System.Exception e){
				Debug.LogError(e.Message);
				Fsm.Event(failureEvent);
				return;
			}
			
			
			indexOf.Value = indexOfResult;
			if (indexOfResult==-1){
				Fsm.Event(itemNotFound);
			}else{
				Fsm.Event(itemFound);
			}
			
		}
	}
}