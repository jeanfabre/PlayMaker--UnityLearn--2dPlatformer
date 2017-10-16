// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the source image sprite of a UGui Image component.")]
	public class uGuiImageSetSprite : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The source sprite of the UGui Image component.")]
		[ObjectType(typeof(UnityEngine.Sprite))]
		public FsmObject sprite;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		UnityEngine.UI.Image _image;
		UnityEngine.Sprite _originalSprite;


		public override void Reset()
		{
			gameObject = null;
			resetOnExit = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_image = _go.GetComponent<UnityEngine.UI.Image>();
			}
			
			DoSetImageSourceValue();

			Finish();
		}

		void DoSetImageSourceValue()
		{
			if (_image==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_originalSprite = _image.sprite;
			}

			_image.sprite = (UnityEngine.Sprite)sprite.Value;
		}

		public override void OnExit()
		{
			if (_image==null)
			{
				return;
			}

			if (resetOnExit.Value)
			{
				_image.sprite = _originalSprite;
			}
		}
		
	}
}