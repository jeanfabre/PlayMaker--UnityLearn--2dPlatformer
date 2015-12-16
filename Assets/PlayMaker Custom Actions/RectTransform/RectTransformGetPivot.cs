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
	[Tooltip("Get the normalized position in this RectTransform that it rotates around.")]
	public class RectTransformGetPivot : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The pivot")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 pivot;
		
		[Tooltip("The x component of the pivot")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;
		
		[Tooltip("The y component of the pivot")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			pivot = null;
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
			
			if (!pivot.IsNone) pivot.Value = _rt.pivot;
			if (!x.IsNone) x.Value = _rt.pivot.x;
			if (!y.IsNone) y.Value = _rt.pivot.y;
		}
	}
}