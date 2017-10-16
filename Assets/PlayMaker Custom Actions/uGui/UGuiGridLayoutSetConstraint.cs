// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made by : DjayDino From Jinxtergames

using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("uGui")]
    [Tooltip("change the grid layout cell size")]
    public class UGuiGridLayoutSetConstraint : FsmStateAction
    {
        


        public enum Constraint
        {
            Flexible,
            FixedColumnCount,
            FixedRowCount
        }

        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
        [Tooltip("The GameObject with the Grid Layout Group component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Contstraint selection")]
        public Constraint constraint = Constraint.Flexible;

        public bool everyFrame;

        UnityEngine.UI.GridLayoutGroup _Grid;

        public override void Reset()
        {
            gameObject = null;
            constraint = Constraint.Flexible;
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {


            DoConstraint();

            if (!everyFrame) Finish();
        }
        // Code that runs every frame.
        public override void OnUpdate()
        {
            DoConstraint();
        }

        private void DoConstraint()
        {
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (_go == null) return;

            if (_go != null)
            {
                _Grid = _go.GetComponent<UnityEngine.UI.GridLayoutGroup>();

                switch (constraint)
                {
                    case Constraint.Flexible:
                        _Grid.constraint = GridLayoutGroup.Constraint.Flexible;
                        break;

                    case Constraint.FixedRowCount:
                        _Grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                        break;

                    case Constraint.FixedColumnCount:
                        _Grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                        break;
                }
            }
        }
    }
}
