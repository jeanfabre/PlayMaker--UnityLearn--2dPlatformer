// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made By : DjayDino

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set The Fill Amount on a uGui image")]
	public class uGuiImageGetFillAmount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		Image _image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_image = _go.GetComponent<UnityEngine.UI.Image>();
			}
			
			DoGetFillAmount();
		
			if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate ()
		{
			DoGetFillAmount();
		}
		
		void DoGetFillAmount()
		{
			if (_image!=null)
			{
				ImageFillAmount.Value =_image.fillAmount;
			}
		}

		
	}
}