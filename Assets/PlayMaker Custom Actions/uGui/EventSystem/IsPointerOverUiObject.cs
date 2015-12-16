// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Checks if Pointer is over an uGui object and returns an event or bool, optionaly takes a pointer id, else use the current event")]
	public class IsPointerOverUiObject : FsmStateAction
	{

		[Tooltip("Optional PointerId. Leave to none to use the current event")]
		public FsmInt pointerId;

		[Tooltip("Event to send when the Pointer is over an uGui object.")]
		public FsmEvent pointerOverUI;
		
		[Tooltip("Event to send when the Pointer is NOT over an uGui object.")]
		public FsmEvent pointerNotOverUI;
		
		[UIHint(UIHint.Variable)]
		public FsmBool isPointerOverUI;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			pointerId = new FsmInt(){UseVariable=true};
			pointerOverUI = null;
			pointerNotOverUI = null;
			isPointerOverUI = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoCheckPointer();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoCheckPointer();
		}
		
		
		void DoCheckPointer()
		{
			bool isOver = false;

			if (pointerId.IsNone)
			{
				isOver = EventSystem.current.IsPointerOverGameObject();
			}else{

				if(EventSystem.current.currentInputModule is PointerInputModule) {
				
					PointerInputModule _module = EventSystem.current.currentInputModule as PointerInputModule;
					isOver = _module.IsPointerOverGameObject(pointerId.Value);
				}

			}

			isPointerOverUI.Value = isOver;

			if (isOver)
			{
				Fsm.Event(pointerOverUI);
			}
			else
			{
				Fsm.Event(pointerNotOverUI);
			}
			
			
			
		}
		
	}
}