//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net


using UnityEditor;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using System.Collections;

[CustomEditor(typeof(PlayMakerArrayListProxy))]
public class PlayMakerArrayListProxyInspector : PlayMakerCollectionProxyInspector
{
	
	// let the user easily add a arrayList without having to know where it is located in the assets
    [MenuItem ("PlayMaker/Addons/ArrayMaker/Add ArrayList Proxy to selected Objects")]
    static void AddPlayMakerArrayListProxyComponent () {
			 foreach (GameObject go in Selection.gameObjects) {
                go.AddComponent("PlayMakerArrayListProxy");
            }
    }

	public void OnEnable () {
		PlayMakerArrayListProxy proxy = (PlayMakerArrayListProxy)target;
		proxy.cleanPrefilledLists();
	}
	
	public override void OnInspectorGUI()
	{
		PlayMakerArrayListProxy proxy = (PlayMakerArrayListProxy)target;

		proxy.referenceName =	EditorGUILayout.TextField(new GUIContent("Reference", "Unique Reference of this ArrayList. Use it if more than one ArrayList is dropped on this game Object"), proxy.referenceName);

		
		BuildEventsInspectorGUI();
		
		// Switch between the two, because we don't need them when they can't be of any help and also would be misleading since changes would not persists
		if (Application.isPlaying){
			BuildPreviewInspectorGUI();
		}else{
			BuildPreFillInspectorGUI(false);
		}
		
		if (GUI.changed)
		{
            EditorUtility.SetDirty(proxy);
		}
	}// OnInspectorGUI
	
	
	
	
		
	private void BuildEventsInspectorGUI()
	{
		PlayMakerArrayListProxy proxy = (PlayMakerArrayListProxy)target;

		string eventsEnabledString = "";
		if (!proxy.showEvents){
		
			if (!proxy.enablePlayMakerEvents){
				eventsEnabledString = " (disabled)";
			}else{
				eventsEnabledString = " (enabled)";
			}
		}
		
		proxy.showEvents = EditorGUILayout.Foldout(proxy.showEvents, new GUIContent("PLayMaker events" + eventsEnabledString, "Manage PlayMaker events dispatch"));
		if (proxy.showEvents){
			EditorGUI.indentLevel = 1;
			proxy.enablePlayMakerEvents = EditorGUILayout.Toggle(new GUIContent("Enable Events", "Broadcast events when item is added, set or removed"),proxy.enablePlayMakerEvents);
			
			if (proxy.enablePlayMakerEvents)
			{
				proxy.addEvent = EditorGUILayout.TextField(new GUIContent("Add Event", "Sent when an item is added. Event data filled with the item value"), proxy.addEvent);
				proxy.setEvent = EditorGUILayout.TextField(new GUIContent("Set Event", "Sent when an item is changed. Event data filled with the index value"), proxy.setEvent);			
				proxy.removeEvent = EditorGUILayout.TextField(new GUIContent("Remove Event", "Sent when an item is removed. Event data filled with the item value"), proxy.removeEvent);
			}
			EditorGUI.indentLevel = 0;
		}
			
	}// BuildEventsInspectorGUI
	

	
	private void BuildPreviewInspectorGUI()
	{
		PlayMakerArrayListProxy proxy = (PlayMakerArrayListProxy)target;
		
		if (proxy.arrayList ==null)
			return;
		
		int count = proxy.arrayList.Count;
		
		BuildPreviewInspectorHeaderGUI(count);
			

		if (!proxy.showContent)
		{
			return;
		}

		if (proxy.arrayList !=null)
		{
			
			int start = proxy.contentPreviewStartIndex;
			int last = Mathf.Min(count,proxy.contentPreviewStartIndex+proxy.contentPreviewMaxRows);

			string label;
			for(int i=start;i<last;i++)
			{
				label = "Item "+ i;
				EditorGUILayout.BeginHorizontal();
				
				if (proxy.arrayList[i] != null )
				{
					
					if (proxy.arrayList[i].GetType() == typeof(bool)) {
							proxy.arrayList[i] = (bool)EditorGUILayout.Toggle(label, (bool)proxy.arrayList[i]);	
					}else if (proxy.arrayList[i].GetType() == typeof(Color)) {
						proxy.arrayList[i]= (Color)EditorGUILayout.ColorField(label, (Color)proxy.arrayList[i]);
					}else if (proxy.arrayList[i].GetType() == typeof(float)) {
							proxy.arrayList[i]= (float)EditorGUILayout.FloatField(label, (float)proxy.arrayList[i]);
					}else if (proxy.arrayList[i].GetType() == typeof(GameObject)) {
							proxy.arrayList[i]= (GameObject)EditorGUILayout.ObjectField(label,(GameObject)proxy.arrayList[i],typeof(GameObject),true);
					}else if (proxy.arrayList[i].GetType() == typeof(int)) {
							proxy.arrayList[i]= (int)EditorGUILayout.IntField(label, (int)proxy.arrayList[i]);
					}else if (proxy.arrayList[i].GetType() == typeof(Material)) {
							proxy.arrayList[i]= (Material)EditorGUILayout.ObjectField(label,(Material)proxy.arrayList[i],typeof(Material),false);
					}else if (proxy.arrayList[i].GetType() == typeof(Object)) {
							proxy.arrayList[i]= (Object)EditorGUILayout.ObjectField(label, (Object)proxy.arrayList[i],typeof(Object),true);
					}else if (proxy.arrayList[i].GetType() == typeof(Quaternion)) {
								Quaternion q = (Quaternion)proxy.arrayList[i];
								Vector4 quat = new Vector4(q[0],q[1],q[2],q[3]);
								quat = EditorGUILayout.Vector4Field(label,quat );
								q[0] = quat[0];
								q[1] = quat[1];
								q[2] = quat[2];
								q[3] = quat[3];
								proxy.arrayList[i] = q;
					}else if (proxy.arrayList[i].GetType() == typeof(Rect)) {
							proxy.arrayList[i]= (Rect)EditorGUILayout.RectField(label, (Rect)proxy.arrayList[i]);
					}else if (proxy.arrayList[i].GetType() == typeof(string)) {
							proxy.arrayList[i]= (string)EditorGUILayout.TextField(label, (string)proxy.arrayList[i]);
					}else if (proxy.arrayList[i].GetType() == typeof(Texture2D)) {
						
						GUILayout.BeginHorizontal();
						GUILayout.Space(15);
							GUILayout.Label(label);
							GUILayout.FlexibleSpace();
							if (proxy.TextureElementSmall)
							{
								proxy.arrayList[i]= (Texture2D)EditorGUILayout.ObjectField((Texture2D)proxy.arrayList[i],typeof(Texture2D),false);
							}else{
								proxy.arrayList[i]= (Texture2D)EditorGUILayout.ObjectField("",(Texture2D)proxy.arrayList[i],typeof(Texture2D),false);
							}
							GUILayout.Space(5);
						GUILayout.EndHorizontal();

						
						
					}else if (proxy.arrayList[i].GetType() == typeof(Vector2)) {
						proxy.arrayList[i]= (Vector2)EditorGUILayout.Vector2Field(label, (Vector2)proxy.arrayList[i]);
					}else if (proxy.arrayList[i].GetType() == typeof(Vector3)) {
						proxy.arrayList[i]= (Vector3)EditorGUILayout.Vector3Field(label, (Vector3)proxy.arrayList[i]);
					}else if (proxy.arrayList[i].GetType() == typeof(AudioClip)) {
							proxy.arrayList[i]= (AudioClip)EditorGUILayout.ObjectField(label, (AudioClip)proxy.arrayList[i],typeof(AudioClip),true);
					}else{
						//(FsmBool)proxy.arrayList[i].Value = (bool)EditorGUILayout.Toggle(label, (FsmBool)proxy.arrayList[i].Value);	
						// OUPS
						Debug.Log(proxy.arrayList[i].GetType());
							//EditorGUILayout.TextField(label, (string)proxy.arrayList[i]);
					}
					
					
				}else{
						
					EditorGUILayout.LabelField(label,"-- NULL --");
				}
				EditorGUILayout.EndHorizontal();
				if (Application.isPlaying &&  GUI.changed){
					proxy.InspectorEdit(i);
				}
			}
			

		}else{
			EditorGUILayout.LabelField("-- Empty --","");
		}
		
		if (proxy.liveUpdate){
			Repaint();
		}	
	}
	
	
}
