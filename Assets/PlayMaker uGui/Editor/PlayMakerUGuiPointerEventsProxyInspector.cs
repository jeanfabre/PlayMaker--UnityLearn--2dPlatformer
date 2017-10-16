using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

using System.Collections;

/// <summary>
/// PlayMaker UGUI pointer events proxy inspector.
/// This is only to remove the first script pointer field, which is totally unnecessary and takes vertical space in the editor
/// </summary>
[CustomEditor(typeof(PlayMakerUGuiPointerEventsProxy))]
public class PlayMakerUGuiPointerEventsProxyInspector : Editor {
	
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

		SerializedProperty onClickEvent = serializedObject.FindProperty("onClickEvent");
		EditorGUILayout.PropertyField(onClickEvent);

		SerializedProperty onDownEvent = serializedObject.FindProperty("onDownEvent");
		EditorGUILayout.PropertyField(onDownEvent);

		SerializedProperty onEnterEvent = serializedObject.FindProperty("onEnterEvent");
		EditorGUILayout.PropertyField(onEnterEvent);

		SerializedProperty onExitEvent = serializedObject.FindProperty("onExitEvent");
		EditorGUILayout.PropertyField(onExitEvent);

		SerializedProperty onUpEvent = serializedObject.FindProperty("onUpEvent");
		EditorGUILayout.PropertyField(onUpEvent);


		serializedObject.ApplyModifiedProperties();
	}


}
