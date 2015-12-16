// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// not sure if it's needed...

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Rebuild a UGui Graphics component.")]
	public class uGuiInputFieldRebuild: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The GameObject with the ui canvas Element component.")]
		public FsmOwnerDefault gameObject;

		public UnityEngine.UI.CanvasUpdate canvasUpdate;

		[Tooltip("Only Rebuild when state exits.")]
		public bool rebuildOnExit;

		private UnityEngine.UI.Graphic _graphic;

		public override void Reset()
		{
			gameObject = null;
			canvasUpdate = UnityEngine.UI.CanvasUpdate.LatePreRender;
			rebuildOnExit = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_graphic = _go.GetComponent<UnityEngine.UI.Graphic>();
			}

			if(!rebuildOnExit)
			{
				DoAction();
			}

			Finish();
		}

		void DoAction()
		{
			if (_graphic!=null)
			{
				_graphic.Rebuild(canvasUpdate);
			}
		}

		public override void OnExit()
		{
			if (rebuildOnExit)
			{
				DoAction();
			}
		}

	}
}