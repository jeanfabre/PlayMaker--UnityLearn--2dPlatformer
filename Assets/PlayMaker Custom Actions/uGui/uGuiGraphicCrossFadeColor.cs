// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// keywords: Cross fade tween

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Tweens the color of the CanvasRenderer color associated with this Graphic.")]
	public class uGuiGraphicCrossFadeColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The Color target of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;
		
		[Tooltip("The red channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;
		
		[Tooltip("The green channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;
		
		[Tooltip("The blue channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;
		
		[Tooltip("The alpha channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;
	
		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoredTimeScale;

		[Tooltip("Should also Tween the alpha channel?")]
		public FsmBool useAlpha;

		UnityEngine.UI.Graphic _component;


		public override void Reset()
		{
			gameObject = null;
			color = null;
			
			red = new FsmFloat(){UseVariable=true};
			green = new FsmFloat(){UseVariable=true};
			blue = new FsmFloat(){UseVariable=true};
			alpha = new FsmFloat(){UseVariable=true};

			useAlpha = null;

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

			Color _color = _component.color;
			
			if (!color.IsNone)
			{
				_color = color.Value;
			}
			
			if (!red.IsNone)
			{
				_color.r = red.Value;
			}
			if (!green.IsNone)
			{
				_color.g = green.Value;
			}
			if (!blue.IsNone)
			{
				_color.b = blue.Value;
			}
			if (!alpha.IsNone)
			{
				_color.a = alpha.Value;
			}

			_component.CrossFadeColor(_color,duration.Value,ignoredTimeScale.Value,useAlpha.Value);

			Finish();

		}


		
	}
}