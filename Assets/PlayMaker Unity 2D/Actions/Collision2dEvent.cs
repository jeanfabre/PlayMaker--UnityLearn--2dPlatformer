// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics2D)]
    [Tooltip("Detect collisions between the Owner of this FSM and other Game Objects that have RigidBody2D components.\nNOTE: The system events, COLLISION ENTER 2D, COLLISION STAY 2D, and COLLISION EXIT 2D are sent automatically on collisions with any object. Use this action to filter collisions by Tag.")]
    public class Collision2dEvent : FsmStateAction
    {
        [Tooltip("The type of collision to detect.")]
        public Collision2DType collision;

        [UIHint(UIHint.Tag)]
        [Tooltip("Filter by Tag.")]
        public FsmString collideTag;

        [Tooltip("Event to send if a collision is detected.")]
        public FsmEvent sendEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
        public FsmGameObject storeCollider;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the force of the collision. NOTE: Use Get Collision 2D Info to get more info about the collision.")]
        public FsmFloat storeForce;

        public override void Reset()
        {
            collision = Collision2DType.OnCollisionEnter2D;
            collideTag = "Untagged";
            sendEvent = null;
            storeCollider = null;
            storeForce = null;
        }

        public override void OnPreprocess()
        {
            switch (collision)
            {
                case Collision2DType.OnCollisionEnter2D:
                    Fsm.HandleCollisionEnter2D = true;
                    break;
                case Collision2DType.OnCollisionStay2D:
                    Fsm.HandleCollisionStay2D = true;
                    break;
                case Collision2DType.OnCollisionExit2D:
                    Fsm.HandleCollisionExit2D = true;
                    break;
            }

        }

        void StoreCollisionInfo(Collision2D collisionInfo)
        {
            storeCollider.Value = collisionInfo.gameObject;
            storeForce.Value = collisionInfo.relativeVelocity.magnitude;
        }

        public override void DoCollisionEnter2D(Collision2D collisionInfo)
        {
            if (collision == Collision2DType.OnCollisionEnter2D)
            {
                if (collisionInfo.collider.gameObject.tag == collideTag.Value)
                {
                    StoreCollisionInfo(collisionInfo);
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override void DoCollisionStay2D(Collision2D collisionInfo)
        {
            if (collision == Collision2DType.OnCollisionStay2D)
            {
                if (collisionInfo.collider.gameObject.tag == collideTag.Value)
                {
                    StoreCollisionInfo(collisionInfo);
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override void DoCollisionExit2D(Collision2D collisionInfo)
        {
            if (collision == Collision2DType.OnCollisionExit2D)
            {
                if (collisionInfo.collider.gameObject.tag == collideTag.Value)
                {
                    StoreCollisionInfo(collisionInfo);
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