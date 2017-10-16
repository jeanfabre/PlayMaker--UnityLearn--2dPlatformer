// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui Text component.")]
	public class uGuiTextSetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UGui Text component.")]
		public FsmString text;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Text _text;
		string _originalString;

		public override void Reset()
		{
			gameObject = null;
			text = null;
			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_text = _go.GetComponent<UnityEngine.UI.Text>();
			}

			if (resetOnExit.Value)
			{
				_originalString = _text.text;
			}

			DoSetTextValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetTextValue();
		}
		
		void DoSetTextValue()
		{

			if (_text!=null)
			{
				_text.text = text.Value;
			}
		}

		public override void OnExit()
		{
			if (_text==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_text.text = _originalString;
			}
		}
	}
}