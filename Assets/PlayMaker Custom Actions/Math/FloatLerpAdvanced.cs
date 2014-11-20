// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
					]
}
EcoMetaEnd
---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Advanced interpolation between 2 floats.\n Advanced features different interpolation modes and allows selection of update type.")]
	public class FloatLerpAdvanced : FsmStateAction
	{
		public enum LerpInterpolationType {Linear,Quadratic,EaseIn,EaseOut,Smoothstep,Smootherstep};

		[RequiredField]
		[Tooltip("First Vector.")]
		public FsmFloat fromFloat;
		
		[RequiredField]
		[Tooltip("Second Vector.")]
		public FsmFloat toFloat;
		
		[RequiredField]
		[HasFloatSlider(-1f, 1f)]
		[Tooltip("Interpolate between From Vector and ToVector by this amount. Value is clamped to -1 1 range. interpolation can be choosen below")]
		public FsmFloat amount;

		public LerpInterpolationType interpolation;

		[RequiredField]
		[UIHint(UIHint.Variable)]

		[Tooltip("Store the result in this float variable.")]
		public FsmFloat storeResult;
		
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;

		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;

		public override void Reset()
		{
			fromFloat = new FsmFloat { UseVariable = true };
			toFloat = new FsmFloat { UseVariable = true };

			interpolation = LerpInterpolationType.Linear;

			storeResult = null;
			everyFrame = true;
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
		}
		
		public override void Awake()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				Fsm.HandleFixedUpdate = true;
			}
		}
		
		public override void OnUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate)
			{
				DoLerp();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnLateUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				DoLerp();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				DoLerp();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		void DoLerp()
		{
			float t = amount.Value;

			float mult = t<0?-1:1;


			t = GetInterpolation(Mathf.Abs(t),interpolation);

			storeResult.Value = mult * Mathf.Lerp(fromFloat.Value, toFloat.Value, t);
		}

		float GetInterpolation(float t,LerpInterpolationType type)
		{
			switch(type)
			{
			case LerpInterpolationType.Quadratic:
				return t*t;
			case LerpInterpolationType.EaseIn:
				return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.EaseOut:
				return Mathf.Sin(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.Smoothstep:
				return t*t * (3f - 2f*t);
			case LerpInterpolationType.Smootherstep:
				return t*t*t * (t * (6f*t - 15f) + 10f);
			}

			return t;
		}




	}
}

