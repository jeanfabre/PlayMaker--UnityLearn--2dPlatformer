// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Detect 2D trigger collisions between the Owner of this FSM and other Game Objects that have RigidBody2D components.\nNOTE: The system events, TRIGGER ENTER 2D, TRIGGER STAY 2D, and TRIGGER EXIT 2D are sent automatically on collisions triggers with any object. Use this action to filter collision triggers by Tag.")]
    public class Trigger2dEvent : FsmStateAction
    {
        [Tooltip("The type of trigger event to detect.")]
        public Trigger2DType trigger;

        [UIHint(UIHint.Tag)]
        [Tooltip("Filter by Tag.")]
        public FsmString collideTag;

        [Tooltip("Event to send if the trigger event is detected.")]
        public FsmEvent sendEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
        public FsmGameObject storeCollider;

        public override void Reset()
        {
            trigger = Trigger2DType.OnTriggerEnter2D;
            collideTag = "Untagged";
            sendEvent = null;
            storeCollider = null;
        }

        public override void OnPreprocess()
        {
            switch (trigger)
            {
                case Trigger2DType.OnTriggerEnter2D:
                    Fsm.HandleTriggerEnter2D = true;
                    break;
                case Trigger2DType.OnTriggerStay2D:
                    Fsm.HandleTriggerStay2D = true;
                    break;
                case Trigger2DType.OnTriggerExit2D:
                    Fsm.HandleTriggerExit2D = true;
                    break;
            }
        }

        void StoreCollisionInfo(Collider2D collisionInfo)
        {
            storeCollider.Value = collisionInfo.gameObject;
        }

        public override void DoTriggerEnter2D(Collider2D other)
        {
            if (trigger == Trigger2DType.OnTriggerEnter2D)
            {
                if (other.gameObject.tag == collideTag.Value)
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override void DoTriggerStay2D(Collider2D other)
        {
            if (trigger == Trigger2DType.OnTriggerStay2D)
            {
                if (other.gameObject.tag == collideTag.Value)
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override void DoTriggerExit2D(Collider2D other)
        {
            if (trigger == Trigger2DType.OnTriggerExit2D)
            {
                if (other.gameObject.tag == collideTag.Value)
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override string ErrorCheck()
        {
            return ActionHelpers.CheckOwnerPhysics2dSetup(Owner);
        }
    }

}