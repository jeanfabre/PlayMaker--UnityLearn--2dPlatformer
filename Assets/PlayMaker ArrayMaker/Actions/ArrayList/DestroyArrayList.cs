//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Use this action to destroy a PlayMakerArrayListProxy component.
// Note: you need to reference an FsmObject of type PlayMakerArrayListProxy and a FsmString representing the reference name for a given PlayMakerArrayListProxy
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Destroys a PlayMakerArrayListProxy Component of a Game Object.")]
	public class DestroyArrayList : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList proxy component ( necessary if several component coexists on the same GameObject")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;
				
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the ArrayList proxy component is destroyed")]
		public FsmEvent successEvent;

		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the ArrayList proxy component was not found")]
		public FsmEvent notFoundEvent;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			successEvent = null;
			notFoundEvent = null;
	
		}

		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
			{
				DoDestroyArrayList();
			}else{
				Fsm.Event(notFoundEvent);
			}
			
			Finish();
		}

		void DoDestroyArrayList()
		{
			PlayMakerArrayListProxy[] proxies = proxy.GetComponents<PlayMakerArrayListProxy>();
			foreach (PlayMakerArrayListProxy iProxy in proxies) {
        		if (iProxy.referenceName == reference.Value){
					Object.Destroy(iProxy);
					Fsm.Event(successEvent);
					return;
				}
		 	}
			
			Fsm.Event(notFoundEvent);
		}
	}
}