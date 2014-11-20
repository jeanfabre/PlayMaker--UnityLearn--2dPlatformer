//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// Credits: FylingRobot: http://hutonggames.com/playmakerforum/index.php?topic=5116

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable


using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Returns the Gameobject within an arrayList which have the max float value in its FSM")]
	public class ArrayListGetGameobjectMaxFsmFloatIndex : ArrayListActions
	{
	
			[ActionSection("Set up")]
			
			[RequiredField]
			[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
			[CheckForComponent(typeof(PlayMakerArrayListProxy))]
			public FsmOwnerDefault gameObject;
			
			[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
			public FsmString reference;
			
			[UIHint(UIHint.FsmName)]
			[Tooltip("Optional name of FSM on Game Object")]
			public FsmString fsmName;
		
			[RequiredField]
			[UIHint(UIHint.FsmFloat)]
			public FsmString variableName;
			
			public bool everyframe;
			
			[ActionSection("Result")]
		
			[UIHint(UIHint.Variable)]
			public FsmFloat storeMaxValue;
		
			[UIHint(UIHint.Variable)]
			public FsmGameObject maxGameObject;
			
			[UIHint(UIHint.Variable)]
			public FsmInt maxIndex;
		
			GameObject goLastFrame;
			PlayMakerFSM fsm;
			
			
			public override void Reset()
			{
			
				gameObject = null;
				reference = null;
				maxGameObject = null;
				maxIndex = null;
				
				everyframe = true;
				fsmName = "";
				storeMaxValue = null;
			}
			
			
			public override void OnEnter()
			{
	
				if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				{
					Finish();
				}
				
				DoFindMaxGo();
				
				if (!everyframe)
				{
					Finish();
				}
				
			}
			
			public override void OnUpdate()
			{
				DoFindMaxGo();
			}
	
	
			void DoFindMaxGo()
			{
				float compValue = 0;	
			
				if (storeMaxValue.IsNone) return;
	
				if (! isProxyValid())
				{
					return;
				}
			
				int _index = 0;
	
				foreach(GameObject _go in proxy.arrayList)
				{
					
					if (_go!=null) 
					{
							
						fsm = ActionHelpers.GetGameObjectFsm(_go, fsmName.Value);
					
						if (fsm == null) return;
				
						FsmFloat fsmFloat = fsm.FsmVariables.GetFsmFloat(variableName.Value);
				
						if (fsmFloat == null) return;
					
						if(fsmFloat.Value > compValue)
						{
						storeMaxValue.Value = fsmFloat.Value;
						compValue = fsmFloat.Value;
						maxGameObject.Value = _go;
						maxIndex.Value = _index;
						}	
					
					}
					_index++;
				}
	
			}
			
	}
	
}

