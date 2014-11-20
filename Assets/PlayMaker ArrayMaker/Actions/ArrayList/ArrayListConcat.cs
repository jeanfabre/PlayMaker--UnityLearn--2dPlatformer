//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Concat joins two or more arrayList proxy components. if a target is specified, the method use the target store the concatenation, else the ")]
	public class ArrayListConcat : ArrayListActions
	{
		[ActionSection("Storage")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component to store the concatenation ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[ActionSection("ArrayLists to concatenate")]
		
		[CompoundArray("ArrayLists", "ArrayList GameObject", "Reference")]
		
		[RequiredField]
		[Tooltip("The GameObject with the PlayMaker ArrayList Proxy component to copy to")]
		[ObjectType(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault[] arrayListGameObjectTargets;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component to copy to ( necessary if several component coexists on the same GameObject")]
		public FsmString[] referenceTargets;		
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			arrayListGameObjectTargets = null;
			referenceTargets = null;

		}

		
		public override void OnEnter()
		{

			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
					DoArrayListConcat(proxy.arrayList);
			
			Finish();
		}

		
		public void DoArrayListConcat(ArrayList source)
		{
			if (! isProxyValid()) 
				return;
			
			
		
			for(int i=0;i<arrayListGameObjectTargets.Length;i++){
				if ( SetUpArrayListProxyPointer( Fsm.GetOwnerDefaultTarget(arrayListGameObjectTargets[i]),referenceTargets[i].Value) ){
						if (isProxyValid()){
						
						foreach(object element in proxy.arrayList){
							
							source.Add(element);
							Debug.Log("count "+source.Count);
						}
							
					}
				}
			}
				
		}
	}
}