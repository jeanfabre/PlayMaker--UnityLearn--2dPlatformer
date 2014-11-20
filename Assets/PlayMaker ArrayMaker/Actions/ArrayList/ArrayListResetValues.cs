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
	[Tooltip("Sets all element to to a given value of a PlayMaker ArrayList Proxy component")]
	public class ArrayListResetValues : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("The value to reset all the arrayList with")]
		public FsmVar resetValue;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			resetValue = null;
		}

		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				ResetArrayList();
			
			Finish();
		}

		
		public void ResetArrayList()
		{
			if (! isProxyValid()) 
				return;
			
			object _val = PlayMakerUtils.GetValueFromFsmVar(this.Fsm,resetValue);
			
			for(int i=0; i<proxy.arrayList.Count; i++)
			{
				proxy.arrayList[i] = _val;
			}
		}
	}
}