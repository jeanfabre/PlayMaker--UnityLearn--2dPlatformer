// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the direction of a UGui Scrollbar component.")]
	public class uGuiScrollbarGetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The direction of the UGui Scrollbar component.")]
		[ObjectType(typeof(Scrollbar.Direction))]
		public FsmEnum direction;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		Scrollbar _scrollbar;

		public override void Reset()
		{
			gameObject = null;
			direction = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_scrollbar = _go.GetComponent<Scrollbar>();
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
				direction.Value = _scrollbar.direction ;
			}
		}
	}
}