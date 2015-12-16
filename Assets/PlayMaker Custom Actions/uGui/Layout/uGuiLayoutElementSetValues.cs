// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets various values of a UGui Layout Element component.")]
	public class uGuiLayoutElementSetValues : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.LayoutElement))]
		[Tooltip("The GameObject with the Layout Element component.")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Values")]

		[Tooltip("The minimum width this layout element should have.")]
		public FsmFloat minWidth;

		[Tooltip("The minimum height this layout element should have.")]
		public FsmFloat minHeight;

		[Tooltip("The preferred width this layout element should have before additional available width is allocated.")]
		public FsmFloat preferredWidth;

		[Tooltip("The preferred height this layout element should have before additional available height is allocated.")]
		public FsmFloat preferredHeight;

		[Tooltip("The relative amount of additional available width this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleWidth;

		[Tooltip("The relative amount of additional available height this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleHeight;

		[ActionSection("Options")]
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
		
		private UnityEngine.UI.LayoutElement _layoutElement;
	
		public override void Reset()
		{
			gameObject = null;
			minWidth = new FsmFloat(){UseVariable=true};
			minHeight = new FsmFloat(){UseVariable=true};
			preferredWidth = new FsmFloat(){UseVariable=true};
			preferredHeight = new FsmFloat(){UseVariable=true};
			flexibleWidth = new FsmFloat(){UseVariable=true};
			flexibleHeight = new FsmFloat(){UseVariable=true};

		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_layoutElement = _go.GetComponent<UnityEngine.UI.LayoutElement>();
			}
			

			DoSetValues();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValues();
		}
		
		void DoSetValues()
		{
			
			if (_layoutElement!=null)
			{
				if (!minWidth.IsNone)
				{
					_layoutElement.minWidth = minWidth.Value;
				}

				if (!minHeight.IsNone)
				{
					_layoutElement.minHeight = minHeight.Value;
				}

				if (!preferredWidth.IsNone)
				{
					_layoutElement.preferredWidth = preferredWidth.Value;
				}

				if (!preferredHeight.IsNone)
				{
					_layoutElement.preferredHeight = preferredHeight.Value;
				}

				if (!flexibleWidth.IsNone)
				{
					_layoutElement.flexibleWidth = flexibleWidth.Value;
				}

				if (!flexibleHeight.IsNone)
				{
					_layoutElement.flexibleHeight = flexibleHeight.Value;
				}
			}
		}

	}
}