// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made by : DjayDino From Jinxtergames

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("uGui")]
[Tooltip("change the Spacing")]
public class UGuiGridLayoutSetSpacing : FsmStateAction
{
        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
        [Tooltip("The GameObject with the Grid Layout Group component.")]
        public FsmOwnerDefault gameObject;

        public FsmFloat spacingX;
        public FsmFloat spacingY;
        public FsmVector2 spacing;
        public bool everyFrame;

        UnityEngine.UI.GridLayoutGroup _Grid;
        private Vector2 newSpacing;

        public override void Reset()
        {
            gameObject = null;
            spacingX = new FsmFloat { UseVariable = true };
            spacingY = new FsmFloat { UseVariable = true };
            spacing = new FsmVector2 { UseVariable = true };
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
	{


            DoSetSpacing();

            if (!everyFrame) Finish();
        }
        // Code that runs every frame.
        public override void OnUpdate()
        {
            DoSetSpacing();
        }

        private void DoSetSpacing()
        {
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (_go == null) return;

            if (_go != null)
            {
                _Grid = _go.GetComponent<UnityEngine.UI.GridLayoutGroup>();
            }
            if (!spacing.IsNone) newSpacing = spacing.Value;
            if(!spacingX.IsNone) newSpacing.x = spacingX.Value;
            if (!spacingY.IsNone) newSpacing.y = spacingY.Value;

            _Grid.spacing = newSpacing;
        }

    }

}
