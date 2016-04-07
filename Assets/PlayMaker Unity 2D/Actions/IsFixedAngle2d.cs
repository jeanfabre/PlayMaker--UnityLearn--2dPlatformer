// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Should the rigidbody2D be prevented from rotating?")]
    public class IsFixedAngle2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Event sent if the Rigidbody2D does have fixed angle")]
		public FsmEvent trueEvent;

		[Tooltip("Event sent if the Rigidbody2D doesn't have fixed angle")]
		public FsmEvent falseEvent;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the fixedAngle flag")]
		public FsmBool store;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			trueEvent = null;
			falseEvent = null;
			store = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoIsFixedAngle();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoIsFixedAngle();
		}
		
		void DoIsFixedAngle()
		{
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (!UpdateCache(go))
            {
                return;
            }

			bool isfixedAngle =  (rigidbody2d.constraints & RigidbodyConstraints2D.FreezeRotation) == RigidbodyConstraints2D.FreezeRotation;

			store.Value = isfixedAngle;

			Fsm.Event(isfixedAngle ? trueEvent : falseEvent);
		}
	}
}

