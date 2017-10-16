// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// keywords: Cross fade tween

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Tweens the alpha of the CanvasRenderer color associated with this Graphic.")]
	public class uGuiGraphicCrossFadeAlpha : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The alpha target")]
		public FsmFloat alpha;
	
		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoredTimeScale;

		UnityEngine.UI.Graphic _component;


		public override void Reset()
		{
			gameObject = null;
			alpha = null;

			duration = 1f;
		
			ignoredTimeScale = null;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_component = _go.GetComponent<UnityEngine.UI.Graphic>();
			}

			_component.CrossFadeAlpha(alpha.Value,duration.Value,ignoredTimeScale.Value);

			Finish();

		}


		
	}
}