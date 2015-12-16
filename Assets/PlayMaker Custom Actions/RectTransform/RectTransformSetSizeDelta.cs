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
	[Tooltip("Set the size of this RectTransform relative to the distances between the anchors. this is the 'Width' and 'Height' values in the RectTransform inspector.")]
	public class RectTransformSetSizeDelta : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("TheVector2 sizeDelta. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 sizeDelta;

		[Tooltip("Setting only the x value. Overides sizeDelta x value if set. Set to none for no effect")]
		public FsmFloat x;

		[Tooltip("Setting only the x value. Overides sizeDelta y value if set. Set to none for no effect")]
		public FsmFloat y;
		
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			sizeDelta = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoSetSizeDelta();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetSizeDelta();
		}
		
		void DoSetSizeDelta()
		{
			// init position	
			Vector2 _sizeDelta = _rt.sizeDelta;

			if (!sizeDelta.IsNone)
			{
				_sizeDelta = sizeDelta.Value;
			}

			// override any axis
			if (!x.IsNone) _sizeDelta.x = x.Value;
			if (!y.IsNone) _sizeDelta.y = y.Value;
			
			// apply
			_rt.sizeDelta = _sizeDelta;
		}
	}
}