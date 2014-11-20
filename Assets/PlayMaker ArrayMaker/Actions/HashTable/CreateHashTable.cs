//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// You do not need to have a pre made PlayMakerHashTableProxy component. Use this action to create one on the fly.
// Note: create a FsmObject of type PlayMakerHashTableProxy and a FsmString representing the reference name for this newly created PlayMakerHashTableProxy
// and you can then use this pair in related actions ( *HashTable* actions in Collections category)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Adds a PlayMakerHashTableProxy Component to a Game Object. Use this to create arrayList on the fly instead of during authoring.\n Optionally remove the HashTable component on exiting the state.\n Simply point to existing if the reference exists already.")]
	public class HashTableCreate : HashTableActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The Game Object to add the Hashtable Component to.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker arrayList proxy component ( necessary if several component coexists on the same GameObject")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;
		
		[Tooltip("Remove the Component when this State is exited.")]
		public FsmBool removeOnExit;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the hashtable exists already")]
		public FsmEvent alreadyExistsEvent;
		
		
		
		PlayMakerHashTableProxy addedComponent;

		public override void Reset()
		{
			gameObject = null;
			reference = null;
			alreadyExistsEvent = null;
			
		}

		public override void OnEnter()
		{
			DoAddPlayMakerHashTable();
			
			Finish();
		}

		public override void OnExit()
		{
			if (removeOnExit.Value && addedComponent != null)
			{
				Object.Destroy(addedComponent);
			}
		}

		private void DoAddPlayMakerHashTable()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			PlayMakerHashTableProxy proxy = GetHashTableProxyPointer(go,reference.Value,true);
		 
			
			if (proxy!=null)
			{
				Fsm.Event(alreadyExistsEvent);
			}else{
				addedComponent = (PlayMakerHashTableProxy)go.AddComponent("PlayMakerHashTableProxy");

				if (addedComponent == null)
				{
					Debug.LogError("Can't add PlayMakerHashTableProxy");
				}else{
					addedComponent.referenceName = reference.Value;
					
				}
			}		
			
		}// DoAddPlayMakerHashTable
		
	}
}