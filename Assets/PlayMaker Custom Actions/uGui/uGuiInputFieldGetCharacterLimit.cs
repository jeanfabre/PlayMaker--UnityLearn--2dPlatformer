// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the Character Limit value of a UGui InputField component. This is the maximum number of characters that the user can type into the field.")]
	public class uGuiInputFieldGetCharacterLimit : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The maximum number of characters that the user can type into the UGui InputField component.")]
		public FsmInt characterLimit;

		[Tooltip("Event sent if limit is infinite (equal to 0)")]
		public FsmEvent hasNoLimitEvent;

		[Tooltip("Event sent if limit is more than 0")]
		public FsmEvent isLimitedEvent;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			characterLimit = null;
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
				characterLimit.Value = _inputField.characterLimit;

				if (_inputField.characterLimit>0)
				{
					Fsm.Event(isLimitedEvent);
				}else{
					Fsm.Event(hasNoLimitEvent);
				}
			}
		}
		
	}
}