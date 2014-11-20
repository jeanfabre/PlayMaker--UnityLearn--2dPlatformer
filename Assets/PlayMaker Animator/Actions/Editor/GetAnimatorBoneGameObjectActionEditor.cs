// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMakerEditor
{
	[CustomActionEditor(typeof(GetAnimatorBoneGameObject))]
	public class GetAnimatorBoneGameObjectActionEditor : CustomActionEditor
	{
		
		public override bool OnGUI()
		{
			GetAnimatorBoneGameObject _target = (GetAnimatorBoneGameObject)target;
			
			if (_target.boneAsString==null)
			{
				_target.boneAsString = new HutongGames.PlayMaker.FsmString(){UseVariable=false};
			}


			EditField("gameObject");
		

			if (_target.boneAsString.UseVariable)
			{
				EditField("boneAsString");
				
			}else{
				GUILayout.BeginHorizontal();
				_target.bone = (HumanBodyBones)EditorGUILayout.EnumPopup("Bone", _target.bone);
				
				if (PlayMakerEditor.FsmEditorGUILayout.MiniButtonPadded(PlayMakerEditor.FsmEditorContent.VariableButton))
				{
					_target.boneAsString.UseVariable = true;
				}
				GUILayout.EndHorizontal();
			}

			EditField("boneGameObject");

			return GUI.changed;
		}
		
	}
}