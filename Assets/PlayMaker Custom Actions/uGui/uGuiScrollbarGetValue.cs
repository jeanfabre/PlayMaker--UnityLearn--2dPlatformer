// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the value of a UGui Scrollbar component.")]
	public class uGuiScrollbarGetValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The value of the UGui Scrollbar component.")]
		public FsmFloat value;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private UnityEngine.UI.Scrollbar _scrollbar;

		public override void Reset()
		{
			gameObject = null;
			value = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_scrollbar = _go.GetComponent<UnityEngine.UI.Scrollbar>();
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
			if (_scrollbar!=null)
			{
				value.Value = _scrollbar.value ;
			}
		}
	}
}