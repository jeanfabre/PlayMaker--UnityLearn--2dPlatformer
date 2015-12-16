using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

using System.Collections;

/// <summary>
/// PlayMaker UGUI pointer Drag proxy inspector.
/// This is only to remove the first script pointer field, which is totally unnecessary and takes vertical space in the editor
/// </summary>
[CustomEditor(typeof(PlayMakerUGuiDragEventsProxy))]
public class PlayMakerUGuiDragEventsProxyInspector : Editor {
	
	public override void OnInspectorGUI()
	{
		serializedObject.UpdateIfDirtyOrScript();

		SerializedProperty debug = serializedObject.FindProperty("debug");
		EditorGUILayout.PropertyField(debug);

		SerializedProperty eventTarget = serializedObject.FindProperty("eventTarget");
		EditorGUILayout.PropertyField(eventTarget);

		SerializedProperty onBeginDragEvent = serializedObject.FindProperty("onBeginDragEvent");
		EditorGUILayout.PropertyField(onBeginDragEvent);

		SerializedProperty onDragEvent = serializedObject.FindProperty("onDragEvent");
		EditorGUILayout.PropertyField(onDragEvent);

		SerializedProperty onEndDragEvent = serializedObject.FindProperty("onEndDragEvent");
		EditorGUILayout.PropertyField(onEndDragEvent);

		serializedObject.ApplyModifiedProperties();
	}


}
