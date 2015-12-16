// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the playceholder of a UGui InputField component. Optionally reset on exit")]
	public class uGuiInputFieldSetPlaceHolder : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Graphic))]
		[Tooltip("The placeholder ( any graphic extended uGui Component) for the UGui InputField component.")]
		public FsmGameObject placeholder;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private UnityEngine.UI.InputField _inputField;

		UnityEngine.UI.Graphic _originalValue;

		public override void Reset()
		{
			gameObject = null;
			placeholder = null;
			resetOnExit = null;
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
				_originalValue = _inputField.placeholder;
			}

			DoSetValue();
			
			Finish();
		}

		void DoSetValue()
		{

			if (_inputField!=null)
			{
				GameObject _placeHolderGo = placeholder.Value;

				if (_placeHolderGo==null)
				{
					_inputField.placeholder = null;
					return;
				}

				_inputField.placeholder = _placeHolderGo.GetComponent<UnityEngine.UI.Graphic>();
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
				_inputField.placeholder = _originalValue;
			}
		}
	}
}