// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the selection's color of a UGui InputField component. This is the color of the highlighter to show what characters are selected")]
	public class uGuiInputFieldSetSelectionColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The color of the highlighter to show what characters are selected for the UGui InputField component.")]
		public FsmColor selectionColor;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.InputField _inputField;
		Color _originalValue;

		public override void Reset()
		{
			gameObject = null;
			selectionColor = null;
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}

			if (resetOnExit.Value)
			{
				_originalValue = _inputField.selectionColor;
			}

			DoSetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValue();
		}
		
		void DoSetValue()
		{

			if (_inputField!=null)
			{
				_inputField.selectionColor = selectionColor.Value;
			}
		}

		public override void OnExit()
		{
			if (_inputField==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_inputField.selectionColor = _originalValue;
			}
		}
	}
}