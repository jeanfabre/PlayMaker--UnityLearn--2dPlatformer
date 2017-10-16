// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the selection's color of a UGui InputField component. This is the color of the highlighter to show what characters are selected")]
	public class uGuiInputFieldGetSelectionColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("This is the color of the highlighter to show what characters are selected of the UGui InputField component.")]
		public FsmColor selectionColor;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			selectionColor = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}
			
			DoGetValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetValue();
		}
		
		void DoGetValue()
		{
			if (_inputField!=null)
			{
				selectionColor.Value = _inputField.selectionColor;
			}
		}
		
	}
}