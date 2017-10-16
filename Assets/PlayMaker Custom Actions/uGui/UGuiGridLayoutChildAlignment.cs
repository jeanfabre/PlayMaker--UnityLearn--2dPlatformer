// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made by : DjayDino From Jinxtergames

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("uGui")]
    [Tooltip("set the Child Alignment")]
    public class UGuiGridLayoutChildAlignment : FsmStateAction
    {
        


        public enum ChildAlignment
        {
            UpperLeft,
            UpperRight,
            LowerLeft,
            LowerRight
        }

        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
        [Tooltip("The GameObject with the Grid Layout Group component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Child Alignment selection")]
        public ChildAlignment childAlignment = ChildAlignment.UpperLeft;

        public bool everyFrame;

        UnityEngine.UI.GridLayoutGroup _Grid;

        public override void Reset()
        {
            gameObject = null;
            childAlignment = ChildAlignment.UpperLeft;
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {


            DoStartCorner();

            if (!everyFrame) Finish();
        }
        // Code that runs every frame.
        public override void OnUpdate()
        {
            DoStartCorner();
        }

        private void DoStartCorner()
        {
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (_go == null) return;

            if (_go != null)
            {
                _Grid = _go.GetComponent<UnityEngine.UI.GridLayoutGroup>();

               switch (childAlignment)
                {
                    case ChildAlignment.UpperLeft:
                        _Grid.childAlignment = TextAnchor.UpperLeft;
                        break;
                    case ChildAlignment.UpperRight:
                        _Grid.childAlignment = TextAnchor.UpperRight;
                        break;
                    case ChildAlignment.LowerLeft:
                        _Grid.childAlignment = TextAnchor.LowerLeft;
                        break;
                    case ChildAlignment.LowerRight:
                        _Grid.childAlignment = TextAnchor.LowerRight;
                        break;
                }

            }
        }
    }
}
