// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made by : DjayDino From Jinxtergames

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("uGui")]
[Tooltip("change the grid layout cell size")]
public class UGuiGridLayoutSetCellSize : FsmStateAction
{
        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
        [Tooltip("The GameObject with the Grid Layout Group component.")]
        public FsmOwnerDefault gameObject;

        public FsmFloat cellSizeX;
        public FsmFloat cellSizeY;
        public FsmVector2 cellSize;
        public bool everyFrame;

        UnityEngine.UI.GridLayoutGroup _Grid;
        private Vector2 newCellSize;

        public override void Reset()
        {
            gameObject = null;
            cellSizeX = new FsmFloat{ UseVariable = true};
            cellSizeY = new FsmFloat{ UseVariable = true};
            cellSize = new FsmVector2 { UseVariable = true };
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
	{


            DoSetCellSize();

            if (!everyFrame) Finish();
        }
        // Code that runs every frame.
        public override void OnUpdate()
        {
            DoSetCellSize();
        }

        private void DoSetCellSize()
        {
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (_go == null) return;

            if (_go != null)
            {
                _Grid = _go.GetComponent<UnityEngine.UI.GridLayoutGroup>();
            }
            if (!cellSize.IsNone) newCellSize = cellSize.Value;
            if(!cellSizeX.IsNone) newCellSize.x = cellSizeX.Value;
            if (!cellSizeY.IsNone) newCellSize.y = cellSizeY.Value;

            _Grid.cellSize = newCellSize;
        }

    }

}
