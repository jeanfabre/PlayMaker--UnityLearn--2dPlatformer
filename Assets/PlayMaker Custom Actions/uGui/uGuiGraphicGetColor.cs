// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the color of a UGui graphic component.")]
	public class uGuiGraphicGetColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with the ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Color of the UI component")]
		public FsmColor color;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		UnityEngine.UI.Graphic _component;

		public override void Reset()
		{
			gameObject = null;
			color = null;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_component = _go.GetComponent<UnityEngine.UI.Graphic>();
			}

			DoGetColorValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetColorValue();
		}

		void DoGetColorValue()
		{
			if (_component!=null)
			{
				color.Value = _component.color;
			}
		}

	}
}