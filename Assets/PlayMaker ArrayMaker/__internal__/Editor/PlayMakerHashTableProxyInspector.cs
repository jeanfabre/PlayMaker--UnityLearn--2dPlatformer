//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net


using UnityEditor;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
//using HutongGames.PlayMakerEditor;
using System.Collections;

[CustomEditor(typeof(PlayMakerHashTableProxy))]
public class PlayMakerHashTableProxyInspector : PlayMakerCollectionProxyInspector
{
	
		// let the user easily add an HashTable without having to know where it is located in the assets
    [MenuItem ("PlayMaker/Addons/ArrayMaker/Add HashTable Proxy to selected Objects")]
    static void AddPlayMakerHashTableProxyComponent () {
			 foreach (Transform transform in Selection.transforms) {
                transform.gameObject.AddComponent("PlayMakerHashTableProxy");
            }
    }
	
	
	public void OnEnable () {
		PlayMakerHashTableProxy proxy = (PlayMakerHashTableProxy)target;
		proxy.cleanPrefilledLists();
	}
	
	
	
	public override void OnInspectorGUI()
	{
		PlayMakerHashTableProxy proxy = (PlayMakerHashTableProxy)target;

		proxy.referenceName =	EditorGUILayout.TextField(new GUIContent("Reference", "Unique Reference of this Hashtable. Use it if more than one Hashtable is dropped on this game Object"), proxy.referenceName);
		
		
		
		//BuildEventsInspectorGUI();
		
		// Switch between the two, because we don't need them when they can't be of any help and also would be misleading since changes would not persists
		if (Application.isPlaying){
			BuildPreviewInspectorGUI();
		}else{
			BuildPreFillInspectorGUI(true); // build prefill inspector with keys
		}
		
				
		if (GUI.changed)
		{
            EditorUtility.SetDirty(proxy);
		}
		
	}// OnInspectorGUI
	
	
	
	public void BuildPreviewInspectorGUI()//public override void OnInspectorGUI()
	{
		PlayMakerHashTableProxy proxy = (PlayMakerHashTableProxy)target;
		
		if (proxy.hashTable ==null)
			return;
		
		int count = proxy.hashTable.Count;
		
		BuildPreviewInspectorHeaderGUI(count);
			

		if (!proxy.showContent)
		{
			return;
		}
		if (proxy.hashTable !=null)
		{
			
			int start = proxy.contentPreviewStartIndex;
			int last = Mathf.Min(count,proxy.contentPreviewStartIndex+proxy.contentPreviewMaxRows);
			
			ArrayList keysList = new ArrayList(proxy.hashTable.Keys);
			
			string label;
			for(int i=start;i<last;i++)
			{
				
				label = keysList[i].ToString();
				
				if (proxy.hashTable[keysList[i]] != null )
				{	
					EditorGUILayout.BeginHorizontal();
					if (proxy.hashTable[keysList[i]].GetType() == typeof(bool)) {
						proxy.hashTable[keysList[i]] = (bool)EditorGUILayout.Toggle(label, (bool)proxy.hashTable[keysList[i]]);	
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Color)) {
						proxy.hashTable[keysList[i]]= (Color)EditorGUILayout.ColorField(label, (Color)proxy.hashTable[keysList[i]]);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(float)) {
						proxy.hashTable[keysList[i]]= (float)EditorGUILayout.FloatField(label, (float)proxy.hashTable[keysList[i]]);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(GameObject)) {
						proxy.hashTable[keysList[i]]= (GameObject)EditorGUILayout.ObjectField(label,(GameObject)proxy.hashTable[keysList[i]],typeof(GameObject),true);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(int)) {
						proxy.hashTable[keysList[i]]= (int)EditorGUILayout.IntField(label, (int)proxy.hashTable[keysList[i]]);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Material)) {
						proxy.hashTable[keysList[i]]= (Material)EditorGUILayout.ObjectField(label,(Material)proxy.hashTable[keysList[i]],typeof(Material),false);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Object)) {
						proxy.hashTable[keysList[i]]= (Object)EditorGUILayout.ObjectField(label, (Object)proxy.hashTable[keysList[i]],typeof(Object),true);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Quaternion)) {
						Quaternion q = (Quaternion)proxy.hashTable[keysList[i]];
						Vector4 quat = new Vector4(q[0],q[1],q[2],q[3]);
						quat = EditorGUILayout.Vector4Field(label,quat );
						q[0] = quat[0];
						q[1] = quat[1];
						q[2] = quat[2];
						q[3] = quat[3];
						proxy.hashTable[keysList[i]] = q;
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Rect)) {
						proxy.hashTable[keysList[i]]= (Rect)EditorGUILayout.RectField(label, (Rect)proxy.hashTable[keysList[i]]);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(string)) {
						proxy.hashTable[keysList[i]]= (string)EditorGUILayout.TextField(label, (string)proxy.hashTable[keysList[i]]);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Texture2D)) {
						
						GUILayout.BeginHorizontal();
							GUILayout.Space(15);
							GUILayout.Label(label);
							GUILayout.FlexibleSpace();
						
							if (proxy.TextureElementSmall)
							{
									proxy.hashTable[keysList[i]]= (Texture2D)EditorGUILayout.ObjectField((Texture2D)proxy.hashTable[keysList[i]],typeof(Texture2D),false);
							}else{
									proxy.hashTable[keysList[i]]= (Texture2D)EditorGUILayout.ObjectField("",(Texture2D)proxy.hashTable[keysList[i]],typeof(Texture2D),false);
								
							}
							GUILayout.Space(10);
						GUILayout.EndHorizontal(); 
						
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Vector2)) {
						proxy.hashTable[keysList[i]]= (Vector2)EditorGUILayout.Vector2Field(label, (Vector2)proxy.hashTable[keysList[i]]);
					}else if (proxy.hashTable[keysList[i]].GetType() == typeof(Vector3)) {
						proxy.hashTable[keysList[i]]= (Vector3)EditorGUILayout.Vector3Field(label, (Vector3)proxy.hashTable[keysList[i]]);
						}else if (proxy.hashTable[keysList[i]].GetType() == typeof(AudioClip)) {
						proxy.hashTable[keysList[i]]= (AudioClip)EditorGUILayout.ObjectField(label, (AudioClip)proxy.hashTable[keysList[i]],typeof(AudioClip),true);
					}else{
						// OUPS
						Debug.Log(proxy.hashTable[keysList[i]].GetType());
							//EditorGUILayout.TextField(label, (string)proxy.hashTable[keysList[i]]);
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
			EditorGUILayout.LabelField("","");
		}
		
		if (proxy.liveUpdate){
			Repaint();
		}	
	}
	
}
