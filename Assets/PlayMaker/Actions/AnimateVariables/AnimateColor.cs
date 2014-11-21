// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Color Variable using an Animation Curve.")]
	public class AnimateColor : AnimateFsmAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor colorVariable;
		[RequiredField]
		public FsmAnimationCurve curveR;
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.r.")]
		public Calculation calculationR;
		[RequiredField]
		public FsmAnimationCurve curveG;
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.g.")]
		public Calculation calculationG;
		[RequiredField]
		public FsmAnimationCurve curveB;
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.b.")]
		public Calculation calculationB;
		[RequiredField]
		public FsmAnimationCurve curveA;
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.a.")]
		public Calculation calculationA;
				
		private bool finishInNextStep = false;
		
		Color clr;
				
		public override void Reset()
		{
			base.Reset();
			colorVariable = new FsmColor{UseVariable=true};
		}

		public override void OnEnter()
		{
			base.OnEnter();
			finishInNextStep = false;
			resultFloats = new float[4];
			fromFloats = new float[4];
			fromFloats[0] = colorVariable.IsNone ? 0f : colorVariable.Value.r;
			fromFloats[1] = colorVariable.IsNone ? 0f : colorVariable.Value.g;
			fromFloats[2] = colorVariable.IsNone ? 0f : colorVariable.Value.b;
			fromFloats[3] = colorVariable.IsNone ? 0f : colorVariable.Value.a;
			curves = new AnimationCurve[4];
			curves[0] = curveR.curve;
			curves[1] = curveG.curve;
			curves[2] = curveB.curve;
			curves[3] = curveA.curve;
			calculations = new Calculation[4];
			calculations[0] = calculationR;
			calculations[1] = calculationG;
			calculations[2] = calculationB;
			calculations[3] = calculationA;
			Init();
		}
		
		

		public override void OnUpdate()
		{
			base.OnUpdate();
			if(!colorVariable.IsNone && isRunning){
				clr = new Color(resultFloats[0], resultFloats[1],resultFloats[2],resultFloats[3]); 
				colorVariable.Value = clr;
			}
			
			if(finishInNextStep){
				if(!looping) {
					Finish();
					if(finishEvent != null)	Fsm.Event(finishEvent);
				}
			}
			
			if(finishAction && !finishInNextStep){
				if(!colorVariable.IsNone){
					clr = new Color(resultFloats[0], resultFloats[1],resultFloats[2],resultFloats[3]); 
					colorVariable.Value = clr;
				}
				finishInNextStep = true;
			}
		}
	}
}