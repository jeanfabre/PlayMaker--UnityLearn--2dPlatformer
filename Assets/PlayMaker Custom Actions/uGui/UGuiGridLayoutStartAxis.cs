// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made by : DjayDino From Jinxtergames

using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("uGui")]
    [Tooltip("Set the Start Axis")]
    public class UGuiGridLayoutStartAxis : FsmStateAction
    {
        


        public enum StartAxis
        {
            Horizontal,
            Vertical
        }

        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
        [Tooltip("The GameObject with the Grid Layout Group component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Start Axis selection")]
        public StartAxis startAxis = StartAxis.Horizontal;

        public bool everyFrame;

        UnityEngine.UI.GridLayoutGroup _Grid;

        public override void Reset()
        {
            gameObject = null;
            startAxis = StartAxis.Horizontal;
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {


            DoStartAxis();

            if (!everyFrame) Finish();
        }
        // Code that runs every frame.
        public override void OnUpdate()
        {
            DoStartAxis();
        }

        private void DoStartAxis()
        {
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (_go == null) return;

            if (_go != null)
            {
                _Grid = _go.GetComponent<UnityEngine.UI.GridLayoutGroup>();

                switch (startAxis)
                {
                    case StartAxis.Horizontal:
                        _Grid.startAxis = GridLayoutGroup.Axis.Horizontal;
                        break;
                    case StartAxis.Vertical:
                        _Grid.startAxis = GridLayoutGroup.Axis.Vertical;
                        break;

                }
            }
        }
    }
}
