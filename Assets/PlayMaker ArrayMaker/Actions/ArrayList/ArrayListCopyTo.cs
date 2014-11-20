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
	[Tooltip("Copy elements from one PlayMaker ArrayList Proxy component to another")]
	public class ArrayListCopyTo : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component to copy from")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component to copy from ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component to copy to")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObjectTarget;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component to copy to ( necessary if several component coexists on the same GameObject")]
		public FsmString referenceTarget;		
		
		[Tooltip("Optional start index to copy from the source, if not set, starts from the beginning")]
		public FsmInt startIndex;
		
		[Tooltip("Optional amount of elements to copy, If not set, will copy all from start index.")]
		public FsmInt count;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			gameObjectTarget = null;
			referenceTarget = null;
			
			startIndex = new FsmInt() {UseVariable=true};
			count = new FsmInt() {UseVariable=true};
		}

		
		public override void OnEnter()
		{
			
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				DoArrayListCopyTo(proxy.arrayList);
			
			Finish();
		}

		
		public void DoArrayListCopyTo(ArrayList source)
		{
			if (! isProxyValid()) 
				return;
			
			// now we check the target is defined as well
			if ( !SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObjectTarget),referenceTarget.Value) )
				return;
			
			if (! isProxyValid()) 
				return;
			
			
			int _start = startIndex.Value;
			int _end = source.Count;
			int _count = source.Count;
			
			if (!count.IsNone)
			{
				_count = count.Value;
			}
			
			if (_start<0 || _start>=source.Count){
				LogError("start index out of range");
				return;
			}
			if (count.Value<0 ){
				LogError("count can not be negative");
				return;
			}
			
			_end = Mathf.Min(_start + _count,source.Count);
			
			for (int i=_start;i<_end;i++)
			{
				proxy.arrayList.Add(source[i]);
			}
			
		}
	}
}