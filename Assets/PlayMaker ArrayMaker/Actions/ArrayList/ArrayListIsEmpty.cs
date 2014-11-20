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
	[Tooltip("Check if an ArrayList Proxy component is empty.")]
	public class ArrayListIsEmpty : ArrayListActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		[ActionSection("Result")]
		
		[Tooltip("Store in a bool wether it is empty or not")]
		[UIHint(UIHint.Variable)]
		public FsmBool isEmpty;	
		
		[Tooltip("Event sent if this arrayList is empty ")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isEmptyEvent;	

		[Tooltip("Event sent if this arrayList is not empty")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isNotEmptyEvent;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;

			isEmpty = null;
			isNotEmptyEvent = null;
			isEmptyEvent = null;
		}
		
		public override void OnEnter()
		{
			PlayMakerArrayListProxy _proxy = GetArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value,true);
			
			bool _isEmpty = _proxy.arrayList.Count==0;
			
			isEmpty.Value = _isEmpty;
			
			if(_isEmpty){
				Fsm.Event(isEmptyEvent);
			}else{
				Fsm.Event(isNotEmptyEvent);
			}
			
			Finish();
		}
		
	}
}