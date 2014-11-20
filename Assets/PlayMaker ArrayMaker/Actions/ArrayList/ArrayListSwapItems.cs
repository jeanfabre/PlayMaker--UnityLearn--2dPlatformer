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
	[Tooltip("Swap two items at a specified indexes of a PlayMaker ArrayList Proxy component")]
	public class ArrayListSwapItems: ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The first index to swap")]
		public FsmInt index1;
		
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The second index to swap")]
		public FsmInt index2;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the removeAt throw errors")]
		public FsmEvent failureEvent;
		
		public override void Reset()
		{
			gameObject = null;
			failureEvent = null;
			reference = null;
			index1 = null;
			index2 = null;
		}

		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				doArrayListSwap();
			
			Finish();
		}

		
		public void doArrayListSwap()
		{
			if (! isProxyValid()) 
				return;
			
			try
			{
				var _item2 = proxy.arrayList[index2.Value];
				
				proxy.arrayList[index2.Value] = proxy.arrayList[index1.Value];
				proxy.arrayList[index1.Value] = _item2;
				
			}catch(System.Exception e){
				Debug.LogError(e.Message);
				Fsm.Event(failureEvent);
			}
			
		}
	}
}