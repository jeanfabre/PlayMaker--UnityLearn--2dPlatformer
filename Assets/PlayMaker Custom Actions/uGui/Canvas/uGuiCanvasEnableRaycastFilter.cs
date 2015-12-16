// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker uGui/Proxies/PlayMakerUGuiCanvasRaycastFilterEventsProxy.cs"
					]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Enable or disable the raycasting of events via PlayMakerUGuiCanvasRaycastFilterEventsProxy component. Optionally reset on state exit")]
	public class uGuiCanvasEnableRaycastFilter: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(PlayMakerUGuiCanvasRaycastFilterEventsProxy))]
		[Tooltip("The GameObject with the PlayMakerUGuiCanvasRaycastFilterEventsProxy component.")]
		public FsmOwnerDefault gameObject;

		public FsmBool enableRaycasting;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		public bool everyFrame;

		private PlayMakerUGuiCanvasRaycastFilterEventsProxy _comp;
		bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			enableRaycasting = false;
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_comp = _go.GetComponent<PlayMakerUGuiCanvasRaycastFilterEventsProxy>();

				_originalValue = _comp.RayCastingEnabled;
			}


			DoAction();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAction();
		}

		void DoAction()
		{
			if (_comp!=null)
			{
				_comp.RayCastingEnabled = enableRaycasting.Value;
			}
		}
		
		public override void OnExit()
		{
			if (_comp==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_comp.RayCastingEnabled = _originalValue;
			}
		}
		
	}
}