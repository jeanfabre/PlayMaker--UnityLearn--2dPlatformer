// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// based on Sebastio work: http://hutonggames.com/playmakerforum/index.php?topic=8452.msg42858#msg42858
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set Group Alpha.")]
	public class uGuiCanvasGroupSetAlpha : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasGroup))]
		[Tooltip("The GameObject with an Unity UI CanvasGroup component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The alpha of the UI component.")]
		public FsmFloat alpha;
		
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
		
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		CanvasGroup _component;

		float _originalValue;

		public override void Reset()
		{
			gameObject = null;
			alpha = null;
			
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_component = _go.GetComponent<CanvasGroup>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _component.alpha;
			}

			DoSetValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetValue();
		}

		void DoSetValue()
		{
			if (_component!=null)
			{
				_component.alpha = alpha.Value;
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
				_component.alpha = _originalValue;
			}
		}
		
	}
}