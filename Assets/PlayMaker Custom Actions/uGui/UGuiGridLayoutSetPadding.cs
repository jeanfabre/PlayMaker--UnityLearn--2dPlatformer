// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made by : DjayDino From Jinxtergames

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("uGui")]
[Tooltip("change the Padding")]
public class UGuiGridLayoutSetPadding : FsmStateAction
{
        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
        [Tooltip("The GameObject with the Grid Layout Group component.")]
        public FsmOwnerDefault gameObject;

        public FsmInt left;
        public FsmInt right;
        public FsmInt top;
        public FsmInt bottom;
        public bool everyFrame;

        UnityEngine.UI.GridLayoutGroup _Grid;

        public override void Reset()
        {
            gameObject = null;
            left = new FsmInt { UseVariable = true };
            right = new FsmInt { UseVariable = true };
            top = new FsmInt { UseVariable = true };
            bottom = new FsmInt { UseVariable = true };
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
	    {
            DoSetPadding();

            if (!everyFrame) Finish();
        }
        // Code that runs every frame.
        public override void OnUpdate()
        {
            DoSetPadding();
        }

        private void DoSetPadding()
        {
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (_go == null) return;

            if (_go != null)
            {
                _Grid = _go.GetComponent<UnityEngine.UI.GridLayoutGroup>();
            }
            if (!left.IsNone) _Grid.padding.left = left.Value;
            if (!right.IsNone) _Grid.padding.right = right.Value;
            if (!top.IsNone) _Grid.padding.top = top.Value;
            if (!bottom.IsNone) _Grid.padding.bottom = bottom.Value;
        }
        
    }

}
