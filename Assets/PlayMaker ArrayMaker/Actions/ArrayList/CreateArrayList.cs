//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// You do not need to have a pre made PlayMakerArrayListProxy component. Use this action to create one on the fly.
// Note: create a FsmObject of type PlayMakerArrayListProxy and a FsmString representing the reference name for this newly created PlayMakerArrayListProxy
// and you can then use this pair in related actions ( *ArrayList* actions in Collections category )

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Adds a PlayMakerArrayList Component to a Game Object. Use this to create arrayList on the fly instead of during authoring.\n Optionally remove the ArrayList component on exiting the state.\n Simply point to existing if the reference exists already.")]
	public class ArrayListCreate : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject to add the PlayMaker ArrayList Proxy component to")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker arrayList proxy component ( necessary if several component coexists on the same GameObject")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;

		[Tooltip("Remove the Component when this State is exited.")]
		public FsmBool removeOnExit;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the arrayList exists already")]
		public FsmEvent alreadyExistsEvent;
			
		PlayMakerArrayListProxy addedComponent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			alreadyExistsEvent = null;
			
		}

		public override void OnEnter()
		{
			DoAddPlayMakerArrayList();
			
			Finish();
		}

		public override void OnExit()
		{
			if (removeOnExit.Value && addedComponent != null)
			{
				Object.Destroy(addedComponent);
			}
		}

		void DoAddPlayMakerArrayList()
		{
			
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
		
			PlayMakerArrayListProxy proxy = GetArrayListProxyPointer(go,reference.Value,true);
		 
			
			if (proxy!=null)
			{
				
				Fsm.Event(alreadyExistsEvent);
			}else{
				
				addedComponent = (PlayMakerArrayListProxy)go.AddComponent("PlayMakerArrayListProxy");
	
				if (addedComponent == null)
				{
					LogError("Can't add PlayMakerArrayListProxy");
				}else{
					addedComponent.referenceName = reference.Value;
					
				}
			}
			
			
		}
	}
}