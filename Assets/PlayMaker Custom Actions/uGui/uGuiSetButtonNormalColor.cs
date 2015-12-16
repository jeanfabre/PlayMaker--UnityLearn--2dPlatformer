// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.


using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the button normal color value of a UGui button component. With reset on exit option ")]
	public class uGuiSetButtonNormalColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Button))]
		[Tooltip("The GameObject with the button ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The new color of the UGui Button component.")]
		public FsmColor normalColor;

		[Tooltip("Reset when exiting this state.")]
		public bool resetOnExit;
		
		[Tooltip("Bypass button to drive the action by bool. Action will not be performed if False")]
		public FsmBool enabled = true;

		[Tooltip("Runs everyframe. Useful to animate values over time.")]
		public bool everyFrame;

		private uUI.Button _Button;
		private Color _OriginalNormalColor;

		public override void Reset()
		{
			normalColor = null;
			resetOnExit = false;
			everyFrame = false;
			enabled = true;
		}
		
		public override void OnEnter()
		{
			Initialize(Fsm.GetOwnerDefaultTarget(gameObject));

			if (_Button!=null && resetOnExit)
			{
				_OriginalNormalColor = _Button.colors.normalColor;
			}

			DoSetButtonColor();
	
			if (!everyFrame)
				Finish();
		}
		
		
		public override void OnUpdate()
		{
			DoSetButtonColor();
		}

		
		public override void OnExit()
		{
			if (resetOnExit)
			{
				DoSetOldColorValue();
			}
		}
		
		// JFF: initialize should never call Finish(), or at least it should check then the everyframe option. the only place should be at the end of the OnEnter indeed.
		// this is why calling a method to do all the caching makes the onEnter method a lot easier to write and understand.
		void Initialize(GameObject go)
		{
			if (go == null)
			{
				LogError("Missing Button Component!");
				return;
			}

			// get the component
			_Button = go.GetComponent<uUI.Button>();

			if (_Button==null)
			{
				LogError("Missing UI.Button on "+go.name);
				return;
			}
		}
		
		void DoSetButtonColor()
		{
			if (_Button!=null && enabled.Value)
			{	
				// Do the actual action stuff here.	
				uUI.ColorBlock _colorBlock = _Button.colors; 
				_colorBlock.normalColor = normalColor.Value;
				_Button.colors = _colorBlock;
			}
		}
		
		void DoSetOldColorValue()
		{
			if (_Button!=null && enabled.Value) // JFF: enabled should be consistent, if enabled is false, it should not do anything at all.
			{
				// reset
				uUI.ColorBlock _colorBlock = _Button.colors; 
				_colorBlock.normalColor = _OriginalNormalColor;
				_Button.colors = _colorBlock;
				// if you reset using the whole Colorblock taken from the start, other actions that may change other properties of the same ColorBlock will be corrupted.
				// that's why I take the latest ColorBlock at the time I need to change the normal Color, and straight away reassign it. nothing else will be affected. Same in DoSetButtonColor()
			}
		}
		
	}
}