//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable


// the shuffle routine is a Knuth-Fisher-Yates algorithm explained at : http://www.4guysfromrolla.com/articles/070208-1.aspx

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Shuffle elements from an ArrayList Proxy component")]
	public class ArrayListShuffle : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component to shuffle")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component to copy from ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("Optional start Index for the shuffling. Leave it to 0 for no effect")]
		public FsmInt startIndex;
		
		[Tooltip("Optional range for the shuffling, starting at the start index if greater than 0. Leave it to 0 for no effect, that is will shuffle the whole array")]
		public FsmInt shufflingRange;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			
			startIndex = 0;
			shufflingRange = 0;
			
		}

		
		public override void OnEnter()
		{
			
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				DoArrayListShuffle(proxy.arrayList);
			}
			
			Finish();
		}

		
		public void DoArrayListShuffle(ArrayList source)
		{
			if (! isProxyValid()) 
			{
				return;
			}
			
			int start = 0;
			int end = proxy.arrayList.Count-1;
			
			if (startIndex.Value>0)
			{
				start = Mathf.Min(startIndex.Value,end);
			}
			
			if (shufflingRange.Value>0)
			{
				end = Mathf.Min(proxy.arrayList.Count-1,start + shufflingRange.Value);
				
			}
			Debug.Log(start);
			Debug.Log(end);
			// Knuth-Fisher-Yates algo

		//	for (int i = proxy.arrayList.Count - 1; i > 0; i--)
			for (int i = end; i > start; i--)
			{
			   // Set swapWithPos a random position such that 0 <= swapWithPos <= i
			   int swapWithPos = Random.Range(start,i + 1);
			
			   // Swap the value at the "current" position (i) with value at swapWithPos
			   System.Object tmp = proxy.arrayList[i];
			   proxy.arrayList[i] = proxy.arrayList[swapWithPos];
			   proxy.arrayList[swapWithPos] = tmp;
			}
			//
			
		}
	}
}