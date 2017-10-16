// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the placeHolder GameObject of a UGui InputField component.")]
	public class uGuiInputFieldGetPlaceHolder : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The placeholder of the UGui InputField component.")]
		public FsmGameObject placeHolder;

		[Tooltip("true if placeholder is found")]
		public FsmBool placeHolderDefined;

		[Tooltip("Event sent if no placeholder is defined")]
		public FsmEvent foundEvent;

		[Tooltip("Event sent if a placeholder is defined")]
		public FsmEvent notFoundEvent;
		
		private UnityEngine.UI.InputField _inputField;
		
		public override void Reset()
		{
			placeHolder = null;
			placeHolderDefined = null;
			foundEvent = null;
			notFoundEvent = null;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_inputField = _go.GetComponent<UnityEngine.UI.InputField>();
			}
			
			DoGetValue();
			
			Finish();
		}

		void DoGetValue()
		{
			if (_inputField!=null)
			{
				UnityEngine.UI.Graphic _placeholder = _inputField.placeholder;

				placeHolderDefined.Value = _placeholder!=null;
				if (_placeholder!=null)
				{
					placeHolder.Value = _placeholder.gameObject;
					Fsm.Event(foundEvent);
				}else{
					Fsm.Event(notFoundEvent);
				}
			}
		}
		
	}
}