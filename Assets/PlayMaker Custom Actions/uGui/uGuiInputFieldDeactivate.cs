// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Deactivate to begin processing Events for a UGui InputField component. Optionally Activate on state exit")]
	public class uGuiInputFieldDeactivate: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Activate when exiting this state.")]
		public FsmBool activateOnExit;

		private UnityEngine.UI.InputField _inputField;

		public override void Reset()
		{
			gameObject = null;
			activateOnExit = null;
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
				_inputField.DeactivateInputField();
			}
		}

		public override void OnExit()
		{
			if (_inputField==null)
			{
				return;
			}
			
			if (activateOnExit.Value)
			{
				_inputField.ActivateInputField();
			}
		}

	}
}