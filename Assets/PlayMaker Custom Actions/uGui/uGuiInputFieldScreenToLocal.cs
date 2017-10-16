// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
#define UNITY_PRE_5_3
#endif

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Rebuild a UGui InputField component.")]
	[Obsolete("This action is obsolete; use 'RectTransformScreenPointToLocalPointInRectangle' instead.")]
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
				#if UNITY_PRE_5_3
				local.Value = _inputField.ScreenToLocal(screen.Value);
				#else
				UnityEngine.Debug.LogError("uGuiInputFieldScreenToLocal is obsolete, please use 'RectTransformScreenPointToLocalPointInRectangle' instead"); 
				#endif
			}
		}
		
	}
}