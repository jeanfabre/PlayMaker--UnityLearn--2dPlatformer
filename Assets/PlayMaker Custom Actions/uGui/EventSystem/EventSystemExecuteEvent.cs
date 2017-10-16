// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("The eventType will be executed on all components on the GameObject that can handle it.")]
	public class EventSystemExecuteEvent : FsmStateAction
	{

		public enum EventHandlers {
			Submit,
			beginDrag,
			cancel,
			deselectHandler,
			dragHandler,
			dropHandler,
			endDragHandler,
			initializePotentialDrag,
			pointerClickHandler,
			pointerDownHandler,
			pointerEnterHandler,
			pointerExitHandler,
			pointerUpHandler,
			scrollHandler,
			submitHandler,
			updateSelectedHandler
		};
	
		[RequiredField]
		[Tooltip("The GameObject with  an IEventSystemHandler component ( a UI button for example).")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Type of handler to execute")]
		[ObjectType(typeof(EventHandlers))]
		public FsmEnum  eventHandler;

		[Tooltip("Event Sent if execution was possible on GameObject")]
		public FsmEvent success;

		[Tooltip("Event Sent if execution was NOT possible on GameObject because it can not handle the eventHandler selected")]
		public FsmEvent canNotHandleEvent;

		GameObject go;


		public override void Reset()
		{
			gameObject = null;
			eventHandler = EventHandlers.Submit;
			success = null;
			canNotHandleEvent = null;
		}
		
		public override void OnEnter()
		{
			if (ExecuteEvent())
			{
				Fsm.Event(success);
			}else{
				Fsm.Event(canNotHandleEvent);
			}

			Finish();
		}

		bool ExecuteEvent()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go==null)
			{
				LogError("Missing GameObject ");
				return false;
			}

			EventHandlers _handlerType = (EventHandlers)eventHandler.Value;

			if (_handlerType == EventHandlers.Submit)
			{
				if (!ExecuteEvents.CanHandleEvent<ISubmitHandler>(go)) return false;
	
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
				
			}else if (_handlerType == EventHandlers.beginDrag)
			{
				if (!ExecuteEvents.CanHandleEvent<IBeginDragHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.beginDragHandler);

			}else if (_handlerType == EventHandlers.cancel)
			{
				if (!ExecuteEvents.CanHandleEvent<ICancelHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.cancelHandler);
			}else if (_handlerType == EventHandlers.deselectHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IDeselectHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.deselectHandler);
			}
			if (_handlerType == EventHandlers.dragHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IDragHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.dragHandler);
			}
			if (_handlerType == EventHandlers.dropHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IDropHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.dropHandler);
			}if (_handlerType == EventHandlers.endDragHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IEndDragHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.endDragHandler);
			}if (_handlerType == EventHandlers.initializePotentialDrag)
			{
				if (!ExecuteEvents.CanHandleEvent<IInitializePotentialDragHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.initializePotentialDrag);
			}if (_handlerType == EventHandlers.pointerClickHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerClickHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			}if (_handlerType == EventHandlers.pointerDownHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerDownHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
			}if (_handlerType == EventHandlers.pointerUpHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerUpHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
			}if (_handlerType == EventHandlers.pointerEnterHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerEnterHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
			}if (_handlerType == EventHandlers.pointerExitHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerExitHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
			}
			if (_handlerType == EventHandlers.scrollHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IScrollHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.scrollHandler);
			}if (_handlerType == EventHandlers.updateSelectedHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IUpdateSelectedHandler>(go)) return false;

				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.updateSelectedHandler);
			}

				return true;

		}
		
	}
}