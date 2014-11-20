//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

using System;
using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker;
using HutongGames.PlayMakerEditor;
using Object = UnityEngine.Object;


[ObjectPropertyDrawer(typeof(PlayMakerArrayListProxy))]
public class PlayMakerArrayListProxyObjectPropertyDrawer : ObjectPropertyDrawer 
{
    public override Object OnGUI(GUIContent label, Object obj, bool isSceneObject, params object[] attributes)
    {
        GUILayout.BeginVertical();

        obj = EditorGUILayout.ObjectField(label, obj, typeof(PlayMakerArrayListProxy), isSceneObject);

        GUILayout.Label("This is a custom object property drawer!");

        GUILayout.EndVertical();

        return obj;
    }
}
