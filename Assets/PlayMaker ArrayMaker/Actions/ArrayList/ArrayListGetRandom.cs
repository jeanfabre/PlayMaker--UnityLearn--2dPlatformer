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
	[Tooltip("Gets a random item from a PlayMaker ArrayList Proxy component")]
	public class ArrayListGetRandom : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;		

		[ActionSection("Result")]
		
		[Tooltip("The random item data picked from the array")]
		[UIHint(UIHint.Variable)]
		public FsmVar randomItem;
		
		[Tooltip("The random item index picked from the array")]
		[UIHint(UIHint.Variable)]
		public FsmInt randomIndex;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the action fails ( likely and index is out of range exception)")]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			gameObject = null;

			failureEvent = null;
			
			randomItem = null;
			randomIndex = null;
		}



		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				GetRandomItem();

			Finish();
		}



		public void GetRandomItem()
		{

			if (! isProxyValid())
			{
				return;
			}
			
			int index = Random.Range(0,proxy.arrayList.Count);// IS THIS TRUE? if I do count-1 I never get the last item picked...
			

			object element = null;
			
			try{

				element = proxy.arrayList[index];
			}catch(System.Exception e){
				Debug.LogWarning(e.Message);
				Fsm.Event(failureEvent);
				return;
			}
			
			randomIndex.Value = index;
			
			bool ok = PlayMakerUtils.ApplyValueToFsmVar(Fsm,randomItem,element);
			
			if (!ok)
			{
				Debug.LogWarning("ApplyValueToFsmVar failed");
				Fsm.Event(failureEvent);
				return;
			}
		}
	}
}