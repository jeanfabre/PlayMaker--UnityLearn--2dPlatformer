// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Activate a UGui InputField component to begin processing Events. Optionally Deactivate on state exit")]
	public class uGuiInputFieldActivate: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool deactivateOnExit;

		private UnityEngine.UI.InputField _inputField;

		public override void Reset()
		{
			gameObject = null;
			deactivateOnExit = null;
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
				_inputField.ActivateInputField();
			}
		}

		public override void OnExit()
		{
			if (_inputField==null)
			{
				return;
			}
			
			if (deactivateOnExit.Value)
			{
				_inputField.DeactivateInputField();
			}
		}

	}
}