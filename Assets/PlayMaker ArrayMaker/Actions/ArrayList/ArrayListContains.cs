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
	[Tooltip("Check if an item is contains in a particula PlayMaker ArrayList Proxy component")]
	public class ArrayListContains : ArrayListActions
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
		[Tooltip("The variable to check.")]
		public FsmVar variable;
		
		[ActionSection("Result")]
		
		[Tooltip("Store in a bool wether it contains or not that element (described below)")]
		[UIHint(UIHint.Variable)]
		public FsmBool isContained;	
		
		[Tooltip("Event sent if this arraList contains that element ( described below)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isContainedEvent;	

		[Tooltip("Event sent if this arraList does not contains that element ( described below)")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isNotContainedEvent;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			variable = null;
			
			isContained = null;
			isContainedEvent = null;
			isNotContainedEvent = null;
		}
		
		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				doesArrayListContains();
			
			
			Finish();
		}
		
		
		public void doesArrayListContains()
		{

			if (! isProxyValid() ) 
				return;
			
			bool elementContained = false;
	
			 PlayMakerUtils.RefreshValueFromFsmVar(Fsm,variable);
			
			switch (variable.Type){
				case (VariableType.Bool):
					elementContained = proxy.arrayList.Contains(variable.boolValue);
					break;
				case (VariableType.Color):
					elementContained = proxy.arrayList.Contains(variable.colorValue);
					break;
				case (VariableType.Float):
					elementContained = proxy.arrayList.Contains(variable.floatValue);
					break;
				case (VariableType.GameObject):
					elementContained = proxy.arrayList.Contains(variable.gameObjectValue);
					break;
				case (VariableType.Int):
					elementContained = proxy.arrayList.Contains(variable.intValue);
					break;
				case (VariableType.Material):
					elementContained = proxy.arrayList.Contains(variable.materialValue);
					break;
				case (VariableType.Object):
					elementContained = proxy.arrayList.Contains(variable.objectReference);
					break;
				case (VariableType.Quaternion):
					elementContained = proxy.arrayList.Contains(variable.quaternionValue);
					break;
				case (VariableType.Rect):
					elementContained = proxy.arrayList.Contains(variable.rectValue);
					break;
				case (VariableType.String):
					elementContained = proxy.arrayList.Contains(variable.stringValue);
					break;
				case (VariableType.Texture):
					elementContained = proxy.arrayList.Contains(variable.textureValue);
					break;
				case (VariableType.Vector3):
					elementContained = proxy.arrayList.Contains(variable.vector3Value);
					break;
					case (VariableType.Vector2):
					elementContained = proxy.arrayList.Contains(variable.vector2Value);
					break;
				default:
					//ERROR
					break;
			}
			
			
			//UnityEngine.Debug.Log(elementContained.ToString());
			isContained.Value = elementContained;
			if(elementContained){
				Fsm.Event(isContainedEvent);
			}else{
				Fsm.Event(isNotContainedEvent);
			}
			
		}
		
		
	}
}