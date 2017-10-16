// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets pointer data Input Button on the last System event.")]
	public class GetLastPointerEventDataInputButton : FsmStateAction
	{

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PointerEventData.InputButton))]
		public FsmEnum inputButton;

		public FsmEvent leftClick;

		public FsmEvent middleClick;

		public FsmEvent rightClick;


		public override void Reset()
		{

			inputButton = PointerEventData.InputButton.Left;
			leftClick = null;
			middleClick = null;
			rightClick = null;

		}
		
		public override void OnEnter()
		{
			ExecuteAction();

			Finish();
		}

		void ExecuteAction()
		{
			if (GetLastPointerDataInfo.lastPointeEventData==null)
			{
				return;
			}

			if (!inputButton.IsNone)
			{
				inputButton.Value = (PointerEventData.InputButton)GetLastPointerDataInfo.lastPointeEventData.button;
			}
			
			if (!string.IsNullOrEmpty(leftClick.Name) && GetLastPointerDataInfo.lastPointeEventData.button == PointerEventData.InputButton.Left)
			{
				Fsm.Event(leftClick);
				return;
			}

			if (!string.IsNullOrEmpty(middleClick.Name) && GetLastPointerDataInfo.lastPointeEventData.button == PointerEventData.InputButton.Middle)
			{
				Fsm.Event(middleClick);
				return;
			}

			if (!string.IsNullOrEmpty(rightClick.Name) && GetLastPointerDataInfo.lastPointeEventData.button == PointerEventData.InputButton.Right)
			{
				Fsm.Event(rightClick);
				return;
			}
		}
	}
}