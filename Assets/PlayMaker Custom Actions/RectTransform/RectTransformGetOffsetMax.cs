// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Get the offset of the upper right corner of the rectangle relative to the upper right anchor")]
	public class RectTransformGetOffsetMax : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The offsetMax")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 offsetMax;
		
		[Tooltip("The x component of the offsetMax")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;
		
		[Tooltip("The y component of the offsetMax")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			offsetMax = null;
			x = null;
			y = null;
			
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoGetValues();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoGetValues();
		}
		
		void DoGetValues()
		{
			
			if (!offsetMax.IsNone) offsetMax.Value = _rt.offsetMax;
			if (!x.IsNone) x.Value = _rt.offsetMax.x;
			if (!y.IsNone) y.Value = _rt.offsetMax.y;
		}
	}
}