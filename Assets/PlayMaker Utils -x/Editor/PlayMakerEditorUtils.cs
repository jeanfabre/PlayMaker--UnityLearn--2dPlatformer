using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;

using HutongGames;
using HutongGames.PlayMaker;
using HutongGames.PlayMakerEditor;


public class PlayMakerEditorUtils : Editor {
	
	
	[MenuItem ("PlayMaker/Tools/Addons/Export Current Scene",false,100)]
	public static void ExportCurrentScene()
	{
		if (!EditorApplication.SaveCurrentSceneIfUserWantsTo())
		{
			return;
		}
		
		EditorUtility.DisplayDialog("PlayMaker dll export","Just a reminder that PlayMaker.dll file is not redistributable,\n\nMake sure you uncheck: \n'Assets/PlayMaker/PlayMaker.dll'\n\nwhen exporting a package to sharing with others.","Ok");
		
		
		var _sel =  Selection.objects;
		
		
		if (EditorUtility.DisplayDialog("Export Globals?","If your scene if using global variables, it will need to be included in the package as well.","Export Globals","Ignore Globals"))
		{
			EditorApplication.ExecuteMenuItem("PlayMaker/Tools/Export Globals");
			var _globalAsset = AssetDatabase.LoadAssetAtPath("Assets/PlaymakerGlobals_EXPORTED.asset",typeof(UnityEngine.Object));
			ArrayUtility.Add<UnityEngine.Object>(ref _sel ,_globalAsset);
			Selection.objects = _sel;
		}
		
	
		SelectSceneCustomAction();
		
		
	
		var _scene = AssetDatabase.LoadAssetAtPath(EditorApplication.currentScene,typeof(UnityEngine.Object));
		_sel =  Selection.objects;
		ArrayUtility.Add<UnityEngine.Object>(ref _sel ,_scene);
		Selection.objects = _sel;
		EditorApplication.ExecuteMenuItem("Assets/Export Package...");
	}
	
	
	[MenuItem ("PlayMaker/Tools/Addons/Export Current Scene", true)]
	public static bool CheckExportCurrentScene() {
	    return !String.IsNullOrEmpty(EditorApplication.currentScene);
	}
	
	
	[MenuItem ("PlayMaker/Tools/Addons/Select Current Scene Used Custom Actions")]
	public static void SelectSceneCustomAction()
	{
		UnityEngine.Object[] _list = GetSceneCustomActionDependencies();
		
		var _sel =  Selection.objects;
		ArrayUtility.AddRange<UnityEngine.Object>(ref _sel ,_list);
		Selection.objects = _sel;	
	}

	public static UnityEngine.Object[] GetSceneCustomActionDependencies()
	{
		
		UnityEngine.Object[] list = new UnityEngine.Object[0];
		
		FsmEditor.RebuildFsmList();

		List<PlayMakerFSM> fsms = FsmEditor.FsmComponentList;
		
//		List<System.Type> PlayMakerActions = FsmEditorUtility.Actionslist;

		foreach(PlayMakerFSM fsm in fsms)
		{
			
			//Debug.Log(FsmEditorUtility.GetFullFsmLabel(fsm));
			
			//if (fsm.FsmStates != null) fsm.FsmStates.Initialize();
			
			for (int s = 0; s<fsm.FsmStates.Length; ++s)
			{
				
					fsm.FsmStates[s].LoadActions();
				
					Debug.Log(fsm.FsmStates[s].Name+" is loaded:"+fsm.FsmStates[s].ActionsLoaded);
				
					// Show SendEvent and SendMessage as we find them
					foreach(FsmStateAction action in fsm.FsmStates[s].Actions)
					{
						UnityEngine.Object _asset = FsmEditorUtility.GetActionScriptAsset(action);
						string _name = action.Name;
						if (String.IsNullOrEmpty(_name))
						{
							if (_asset!=null)
							{
								_name = _asset.name;
							}else
							{
								_name = FsmEditorUtility.GetActionLabel(action) + "[WARNING: FILE NOT FOUND]";
							}
							
						}
					
						if (Enum.IsDefined(typeof(WikiPages),_name))
						{
						//	Debug.Log(_name+" : official action");
						}else{
						//	Debug.Log(_name+" : custom action");
						
							if (_asset!=null)
							{
								ArrayUtility.Add<UnityEngine.Object>(ref list ,_asset);
							}
						}
							
					}
			}
			
			
		}
		
		return list;
		
	}// GetSceneCustomActionDependencies
	
	
}
