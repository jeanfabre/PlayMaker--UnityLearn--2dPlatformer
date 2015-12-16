// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Rebuild a UGui InputField component.")]
	public class uGuiInputFieldScreenToLocal: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The screen position")]
		public FsmVector2 screen;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting local position")]
		public FsmVector2 local;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.InputField _inputField;

		public override void Reset()
		{
			gameObject = null;
			screen = null;
			local = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}

			DoAction();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAction();
		}

		void DoAction()
		{
			if (_inputField!=null)
			{
				local.Value = _inputField.ScreenToLocal(screen.Value);
			}
		}

	}
}