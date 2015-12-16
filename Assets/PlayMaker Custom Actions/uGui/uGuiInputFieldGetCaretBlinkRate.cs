// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the caret's blink rate of a UGui InputField component.")]
	public class uGuiInputFieldGetCaretBlinkRate : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The caret's blink rate for the UGui InputField component.")]
		public FsmFloat caretBlinkRate;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			caretBlinkRate = null;
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
				caretBlinkRate.Value = _inputField.caretBlinkRate;

			}
		}
		
	}
}