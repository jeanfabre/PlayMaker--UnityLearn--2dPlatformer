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
	[Tooltip("Gets an item from a PlayMaker ArrayList Proxy component using a base index and a relative increment. This allows you to move to next or previous items granuraly")]
	public class ArrayListGetRelative : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
	
		[Tooltip("The index base to compute the item to get")]
		public FsmInt baseIndex;
		
		[Tooltip("The incremental value from the base index to get the value from. Overshooting the range will loop back on the list.")]
		public FsmInt increment;
		

		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmVar result;

		[Tooltip("The index of the result")]
		[UIHint(UIHint.Variable)]
		public FsmInt resultIndex;

		

		public override void Reset()
		{
			gameObject = null;
			reference = null;
				
			baseIndex = null;
			increment = null;
			

			result = null;
			resultIndex = null;
			
		}
		
		
		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				GetItemAtIncrement();

			Finish();
		}
		
		
		
		public void GetItemAtIncrement(){
			
			if (! isProxyValid())
				return;
		
			
			object element = null;
			
			int targetIndex = baseIndex.Value + increment.Value;
			if (targetIndex>=0)
			{
				resultIndex.Value = (baseIndex.Value + increment.Value) % proxy.arrayList.Count;
			}else{
				resultIndex.Value = proxy.arrayList.Count - Mathf.Abs(baseIndex.Value + increment.Value) % proxy.arrayList.Count;
			}
			
			
			element = proxy.arrayList[resultIndex.Value];

			
			PlayMakerUtils.ApplyValueToFsmVar(Fsm,result,element);
		}
	}
}