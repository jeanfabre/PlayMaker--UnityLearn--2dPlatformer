using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

using System.Collections;

/// <summary>
/// PlayMaker UGUI Drop events proxy inspector.
/// This is only to remove the first script pointer field, which is totally unnecessary and takes vertical space in the editor
/// </summary>
[CustomEditor(typeof(PlayMakerUGuiDropEventsProxy))]
public class PlayMakerUGuiDropEventsProxyInspector : Editor {
	
	public override void OnInspectorGUI()
	{
		#if UNITY_5_6_OR_NEWER
		serializedObject.UpdateIfRequiredOrScript();
		#else
		serializedObject.UpdateIfDirtyOrScript();
		#endif

		SerializedProperty debug = serializedObject.FindProperty("debug");
		EditorGUILayout.PropertyField(debug);

		SerializedProperty eventTarget = serializedObject.FindProperty("eventTarget");
		EditorGUILayout.PropertyField(eventTarget);

		SerializedProperty onDropEvent = serializedObject.FindProperty("onDropEvent");
		EditorGUILayout.PropertyField(onDropEvent);

		serializedObject.ApplyModifiedProperties();
	}


}
