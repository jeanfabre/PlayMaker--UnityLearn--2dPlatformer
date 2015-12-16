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
	[Tooltip("Given a rect transform, return the corner points in pixel accurate coordinates.")]
	public class RectTransformPixelAdjustRect : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[CheckForComponent(typeof(Canvas))]
		[Tooltip("The canvas. Leave to none to use the canvas of the gameObject")]
		public FsmGameObject canvas;

		[ActionSection("Result")]
		[RequiredField]
		[Tooltip("Pixel adjusted rect.")]
		[UIHint(UIHint.Variable)]
		public FsmRect pixelRect;

		RectTransform _rt;
		Canvas _canvas;

		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			canvas = new FsmGameObject(){UseVariable=true};

			pixelRect = null;

		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			GameObject goCanvas = canvas.Value;
			if (goCanvas != null)
			{
				_canvas = goCanvas.GetComponent<Canvas>();
			}

			if (_canvas==null && go!=null)
			{
				var _ui = go.GetComponent<UnityEngine.UI.Graphic>();
				if (_ui!=null)
				{
					_canvas = _ui.canvas;
				}
			}
			
			DoAction();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoAction();
		}
		
		
		void DoAction()
		{
			pixelRect.Value = RectTransformUtility.PixelAdjustRect(_rt,_canvas);
		}
	
	}
}