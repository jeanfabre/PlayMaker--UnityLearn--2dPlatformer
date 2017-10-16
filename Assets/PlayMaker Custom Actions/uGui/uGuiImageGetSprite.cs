// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the source image sprite of a UGui Image component.")]
	public class uGuiImageGetSprite : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The source sprite of the UGui Image component.")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(UnityEngine.Sprite))]
		public FsmObject sprite;

		UnityEngine.UI.Image _image;

		public override void Reset()
		{
			gameObject = null;
			sprite = null;
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
			if (_image!=null)
			{
				sprite.Value = (Sprite)_image.sprite;
			}

		}
	}
}