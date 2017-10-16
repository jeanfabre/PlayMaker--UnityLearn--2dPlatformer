// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// based on Sebastio work: http://hutonggames.com/playmakerforum/index.php?topic=8452.msg42858#msg42858
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set Graphic Color.")]
	public class uGuiGraphicSetColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The Color of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;
		
		[Tooltip("The red channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;
		
		[Tooltip("The green channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;
		
		[Tooltip("The blue channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;
		
		[Tooltip("The alpha channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		UnityEngine.UI.Graphic _component;

		Color _originalColor;

		public override void Reset()
		{
			gameObject = null;
			color = null;
			
			red = new FsmFloat(){UseVariable=true};
			green = new FsmFloat(){UseVariable=true};
			blue = new FsmFloat(){UseVariable=true};
			alpha = new FsmFloat(){UseVariable=true};
			
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_component = _go.GetComponent<UnityEngine.UI.Graphic>();
			}

			if (resetOnExit.Value)
			{
				_originalColor = _component.color;
			}

			DoSetColorValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetColorValue();
		}

		void DoSetColorValue()
		{
			if (_component!=null)
			{
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

				_component.color = _color;
			}
		}

		public override void OnExit()
		{
			if (_component==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_component.color = _originalColor;
			}
		}
		
	}
}