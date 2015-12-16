// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Move Caret to text end on a UGui InputField component. Optionaly select from the current caret position")]
	public class uGuiInputFieldMoveCaretToTextEnd: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Define if we select or not from the current caret position. Default is true = no selection")]
		public FsmBool shift;

		private UnityEngine.UI.InputField _inputField;

		public override void Reset()
		{
			gameObject = null;
			shift = true;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}

			DoAction();
			
			Finish();
		}

		void DoAction()
		{

			if (_inputField!=null)
			{
				_inputField.MoveTextEnd(shift.Value);
			}
		}

	}
}