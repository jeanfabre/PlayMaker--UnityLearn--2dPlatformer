// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made by : DjayDino From Jinxtergames

using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("uGui")]
    [Tooltip("Set the Start Corner")]
    public class UGuiGridLayoutStartCorner : FsmStateAction
    {
        


        public enum StartCorner
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

        [Tooltip("Start Corner selection")]
        public StartCorner startCorner = StartCorner.UpperLeft;

        public bool everyFrame;

        UnityEngine.UI.GridLayoutGroup _Grid;

        public override void Reset()
        {
            gameObject = null;
            startCorner = StartCorner.UpperLeft;
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

                switch (startCorner)
                {
                    case StartCorner.UpperLeft:
                        _Grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
                        break;
                    case StartCorner.UpperRight:
                        _Grid.startCorner = GridLayoutGroup.Corner.UpperRight;
                        break;
                    case StartCorner.LowerLeft:
                        _Grid.startCorner = GridLayoutGroup.Corner.LowerLeft;
                        break;
                    case StartCorner.LowerRight:
                        _Grid.startCorner = GridLayoutGroup.Corner.LowerRight;
                        break;
                }
            }
        }
    }
}
