// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

#if UNITY_5_3_OR_NEWER
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Add multiple options to the options of the Dropdown uGui Component")]
	public class uGuiDropDownAddOptions : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Dropdown))]
		[Tooltip("The GameObject with the DropDown ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Options.")]
		[CompoundArray("Options","Text","Image")]
		public FsmString[] optionText;
		[ObjectType(typeof(Sprite))]
		public FsmObject[] optionImage;


		UnityEngine.UI.Dropdown _dropDown;
		List<UnityEngine.UI.Dropdown.OptionData> _options;

		public override void Reset()
		{
			gameObject = null;
			optionText = new FsmString[1];
			optionImage = new FsmObject[1];
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_dropDown = _go.GetComponent<UnityEngine.UI.Dropdown>();
			}


			DoAddOptions ();
			
			Finish();
			
		}

		
		void DoAddOptions()
		{

			if (_dropDown==null)
			{
				return;
			}
			_options = new List<UnityEngine.UI.Dropdown.OptionData> ();

			int i = 0;
			foreach (FsmString _text in optionText) {
			
				_options.Add(
								new UnityEngine.UI.Dropdown.OptionData()
								{ 	
									text=_text.Value,
									image= optionImage[i].RawValue as Sprite
								}
							);
					i++;
			}

			_dropDown.AddOptions (_options);
		}
	}
}
#endif