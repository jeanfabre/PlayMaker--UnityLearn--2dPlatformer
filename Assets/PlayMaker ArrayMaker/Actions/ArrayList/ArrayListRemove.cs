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
	[Tooltip("Remove an element of a PlayMaker Array List Proxy component")]
	public class ArrayListRemove : ArrayListActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;
		
		[ActionSection("Data")]
		
		[Tooltip("The type of Variable to remove.")]
		public FsmVar variable;
		
		[ActionSection("Result")]
		
		[Tooltip("Event sent if this arraList does not contains that element ( described below)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent notFoundEvent;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			notFoundEvent = null;
			variable = null;
		}
		
		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				DoRemoveFromArrayList();
			
			Finish();
		}
		
		
		public void DoRemoveFromArrayList()
		{
			if (! isProxyValid() ) 
				return;
			
			bool success = proxy.Remove(PlayMakerUtils.GetValueFromFsmVar(Fsm,variable),variable.Type.ToString());
			
			if (!success){
				Fsm.Event(notFoundEvent);
			}
		}
		
		
	}
}