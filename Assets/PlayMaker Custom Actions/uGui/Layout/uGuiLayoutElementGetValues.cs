// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets various values of a UGui Layout Element component.")]
	public class uGuiLayoutElementGetValues : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.LayoutElement))]
		[Tooltip("The GameObject with the Layout Element component.")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Values")]

		[Tooltip("Is this element use Layout constraints")]
		[UIHint(UIHint.Variable)]
		public FsmBool ignoreLayout;

		[Tooltip("The minimum width enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool minWidthEnabled;

		[Tooltip("The minimum width this layout element should have.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat minWidth;

		[Tooltip("The minimum height enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool minHeightEnabled;

		[Tooltip("The minimum height this layout element should have.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat minHeight;

		[Tooltip("The preferred width enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool preferredWidthEnabled;

		[Tooltip("The preferred width this layout element should have before additional available width is allocated.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat preferredWidth;

		[Tooltip("The preferred height enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool preferredHeightEnabled;

		[Tooltip("The preferred height this layout element should have before additional available height is allocated.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat preferredHeight;

		[Tooltip("The flexible width enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool flexibleWidthEnabled;

		[Tooltip("The relative amount of additional available width this layout element should fill out relative to its siblings.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat flexibleWidth;

		[Tooltip("The flexible height enabled state")]
		[UIHint(UIHint.Variable)]
		public FsmBool flexibleHeightEnabled;

		[Tooltip("The relative amount of additional available height this layout element should fill out relative to its siblings.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat flexibleHeight;

		[ActionSection("Options")]
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
		
		private UnityEngine.UI.LayoutElement _layoutElement;
	
		public override void Reset()
		{
			gameObject = null;
			ignoreLayout = null;
			minWidthEnabled = null;
			minHeightEnabled = null;
			preferredWidthEnabled = null;
			preferredHeightEnabled = null;
			flexibleWidthEnabled = null;
			flexibleHeightEnabled = null;
			minWidth = null;
			minHeight = null;
			preferredWidth = null;
			preferredHeight = null;
			flexibleWidth = null;
			flexibleHeight = null;

		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_layoutElement = _go.GetComponent<UnityEngine.UI.LayoutElement>();
			}
			

			DoGetValues();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetValues();
		}
		
		void DoGetValues()
		{
			
			if (_layoutElement!=null)
			{
				if (!ignoreLayout.IsNone)
				{
					ignoreLayout.Value = _layoutElement.ignoreLayout;
				}

				if (!minWidthEnabled.IsNone)
				{
					minWidthEnabled.Value = _layoutElement.minWidth == 0;
				}

				if (!minWidth.IsNone)
				{
					minWidth.Value = _layoutElement.minWidth;
				}

				if (!minHeightEnabled.IsNone)
				{
					minHeightEnabled.Value = _layoutElement.minHeight == 0;
				}

				if (!minHeight.IsNone)
				{
					minHeight.Value = _layoutElement.minHeight;
				}

				if (!preferredWidthEnabled.IsNone)
				{
					preferredWidthEnabled.Value = _layoutElement.preferredWidth == 0;
				}

				if (!preferredWidth.IsNone)
				{
					preferredWidth.Value = _layoutElement.preferredWidth;
				}

				if (!preferredHeightEnabled.IsNone)
				{
					preferredHeightEnabled.Value = _layoutElement.preferredHeight == 0;
				}

				if (!preferredHeight.IsNone)
				{
					preferredHeight.Value = _layoutElement.preferredHeight;
				}

				if (!flexibleWidthEnabled.IsNone)
				{
					flexibleWidthEnabled.Value = _layoutElement.flexibleWidth == 0;
				}

				if (!flexibleWidth.IsNone)
				{
					flexibleWidth.Value = _layoutElement.flexibleWidth;
				}

				if (!flexibleHeightEnabled.IsNone)
				{
					flexibleHeightEnabled.Value = _layoutElement.flexibleHeight == 0;
				}

				if (!flexibleHeight.IsNone)
				{
					flexibleHeight.Value = _layoutElement.flexibleHeight;
				}
			}
		}

	}
}