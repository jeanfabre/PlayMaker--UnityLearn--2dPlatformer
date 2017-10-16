// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Get the ScaleFactor of a CanvasScaler.")]
	public class uGuiCanvasScalerGetScaleFactor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasScaler))]
		[Tooltip("The GameObject with an Unity UI CanvasScaler component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The scaleFactor of the a CanvasScaler component.")]
		public FsmFloat scaleFactor;
		
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		CanvasScaler _component;

		public override void Reset()
		{
			gameObject = null;
			scaleFactor = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_component = _go.GetComponent<CanvasScaler>();
			}

			DoGetValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetValue();
		}

		void DoGetValue()
		{
			if (_component!=null)
			{
				scaleFactor.Value = _component.scaleFactor;
			}
		}
	}
}