// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Made By : DjayDino

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set The Fill Amount on a uGui image")]
	public class uGuiImageSetFillAmount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[HasFloatSlider(0,1)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		Image _image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = 1;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_image = _go.GetComponent<UnityEngine.UI.Image>();
			}
			
			DoSetFillAmount();
		
			if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate ()
		{
			DoSetFillAmount();
		}
		
		void DoSetFillAmount()
		{
			if (_image!=null)
			{
				_image.fillAmount = ImageFillAmount.Value;
			}
		}

		
	}
}