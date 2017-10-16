// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the text value of a UGui InputField component.")]
	public class uGuiInputFieldGetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value of the UGui InputField component.")]
		public FsmString text;
		
		public bool everyFrame;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			text = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}
			
			DoGetTextValue();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetTextValue();
		}
		
		void DoGetTextValue()
		{
			if (_inputField!=null)
			{
				text.Value = _inputField.text;
			}
		}
		
	}
}