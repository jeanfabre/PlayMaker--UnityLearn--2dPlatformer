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
	[Tooltip("Check if an ArrayList Proxy component exists.")]
	public class ArrayListExists : ArrayListActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		[ActionSection("Result")]
		
		[Tooltip("Store in a bool wether it exists or not")]
		[UIHint(UIHint.Variable)]
		public FsmBool doesExists;	
		
		[Tooltip("Event sent if this arrayList exists ")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent doesExistsEvent;	

		[Tooltip("Event sent if this arrayList does not exists")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent doesNotExistsEvent;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;

			doesExists = null;
			doesExistsEvent = null;
			doesNotExistsEvent = null;
		}
		
		public override void OnEnter()
		{
			PlayMakerArrayListProxy _proxy = GetArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value,true);
			
			bool exists = _proxy!=null;
			
			doesExists.Value = exists;
			if(exists){
				Fsm.Event(doesExistsEvent);
			}else{
				Fsm.Event(doesNotExistsEvent);
			}
			
			Finish();
		}
		
	}
}