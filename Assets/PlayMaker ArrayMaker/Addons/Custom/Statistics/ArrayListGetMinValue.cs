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
	[Tooltip("Return the minimum value within an arrayList. It can use float, int, vector2 and vector3 ( uses magnitude), rect ( uses surface), gameobject ( using bounding box volume), and string ( use lenght)")]
	public class ArrayListGetMinValue : ArrayListActions
	{
		
		static VariableType[] supportedTypes = new VariableType[] {
			VariableType.Float,
			VariableType.Int,
			VariableType.Rect,
			VariableType.Vector2,
			VariableType.Vector3,
			VariableType.GameObject,
			VariableType.String};
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("Performs every frame. WARNING, it could be affecting performances.")]
		public bool everyframe;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Minimum Value")]
		public FsmVar minimumValue;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The index of the Maximum Value within that arrayList")]
		public FsmInt minimumValueIndex;

		public override void Reset()
		{
		
			gameObject = null;
			reference = null;
			
			minimumValue = null;
			minimumValueIndex = null;
			
			everyframe = true;
		}
		
		
		public override void OnEnter()
		{

			if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				Finish();
			}
			
			DoFindMinimumValue();
			
			if (!everyframe)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoFindMinimumValue();
		}
		
		void DoFindMinimumValue()
		{
			
			if (! isProxyValid())
			{
				return;
			}
			
			VariableType _targetType = minimumValue.Type;
			if (!supportedTypes.Contains(minimumValue.Type) )
			{
				return ;
			}
			
			float min = float.PositiveInfinity;
			
			int minIndex = 0;
			
			int index = 0;
			foreach(object _obj in proxy.arrayList)
			{
				try{
					float _val = PlayMakerUtils.GetFloatFromObject(_obj,_targetType,true);
					if (min>_val)
					{
						min = _val;
						minIndex = index;
					}
			  	}finally{ }
				
				index++;
			}
			
			minimumValueIndex.Value = minIndex;
			PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,minimumValue,proxy.arrayList[minIndex]);
		}
		
		
		
		public override string ErrorCheck()
        {
			if (!supportedTypes.Contains(minimumValue.Type) )
			{
				return "A "+minimumValue.Type+" can not be processed as a minimum";
			}
			
			return "";
		}
		
	}
}