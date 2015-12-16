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
	[Tooltip("Get the offset of the lower left corner of the rectangle relative to the lower left anchor")]
	public class RectTransformGetOffsetMin : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The offsetMin")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 offsetMin;
		
		[Tooltip("The x component of the offsetMin")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;
		
		[Tooltip("The y component of the offsetMin")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			offsetMin = null;
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
			
			if (!offsetMin.IsNone) offsetMin.Value = _rt.offsetMin;
			if (!x.IsNone) x.Value = _rt.offsetMin.x;
			if (!y.IsNone) y.Value = _rt.offsetMin.y;
		}
	}
}