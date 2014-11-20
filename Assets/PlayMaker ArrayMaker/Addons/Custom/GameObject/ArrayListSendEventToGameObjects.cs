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
	[Tooltip("Send event to all the GameObjects within an arrayList.")]
	public class ArrayListSendEventToGameObjects : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;
		
		public FsmBool excludeSelf;
		
		public FsmBool sendToChildren;
	
		public override void Reset()
		{
		
			gameObject = null;
			reference = null;
			sendEvent = null;
			excludeSelf = false;
			sendToChildren = false;
		}
		
		
		public override void OnEnter()
		{

			if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				Finish();
			}
			
			DoSendEvent();

		}

		void DoSendEvent()
		{
			
			if (! isProxyValid())
			{
				return;
			}
			
			
			foreach(GameObject _go in proxy.arrayList)
			{
				sendEventToGO(_go);
			}
			
		}
		
		void sendEventToGO(GameObject _go)
		{
			FsmEventTarget _eventTarget = new FsmEventTarget();
			_eventTarget.excludeSelf = excludeSelf.Value;
			FsmOwnerDefault owner = new FsmOwnerDefault();
			owner.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			owner.GameObject = new FsmGameObject();
			owner.GameObject.Value = this.Owner;
			_eventTarget.gameObject = owner;
			_eventTarget.target = FsmEventTarget.EventTarget.GameObject;	
				
			_eventTarget.sendToChildren = sendToChildren.Value;
			
			Fsm.Event(_eventTarget,sendEvent);
		}
		
	}
}