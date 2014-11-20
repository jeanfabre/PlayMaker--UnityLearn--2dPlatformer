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
	[Tooltip("Add an item to a PlayMaker Array List Proxy component")]
	public class ArrayListAdd : ArrayListActions
	{

		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component (necessary if several component coexists on the same GameObject)")]
		[UIHint(UIHint.FsmString)]
		public FsmString reference;
		
		[ActionSection("Data")]
		
		[RequiredField]
		[Tooltip("The variable to add.")]
		public FsmVar variable;
		
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			
			variable = null;
		}
		
		
		public override void OnEnter()
		{

			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				AddToArrayList();
			
			Finish();
		}
		
		
		public void AddToArrayList()
		{
			if (! isProxyValid() ) 
				return;
			
			proxy.Add(PlayMakerUtils.GetValueFromFsmVar(Fsm,variable),variable.Type.ToString());
		}
		
		
	}
}