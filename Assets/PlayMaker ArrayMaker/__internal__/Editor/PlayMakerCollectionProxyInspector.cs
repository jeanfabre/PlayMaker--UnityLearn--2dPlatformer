//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

using UnityEditor;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using System.Collections;
using System.Collections.Generic;

public class PlayMakerCollectionProxyInspector : Editor {
	
	protected static int maxPreviewContent = 30;
	
	private bool duplicatedKey = false;

	protected void BuildPreviewInspectorHeaderGUI(int count)
	{		
		PlayMakerCollectionProxy proxy = (PlayMakerCollectionProxy)target;
	
		proxy.showContent = EditorGUILayout.Foldout(proxy.showContent, new GUIContent("Content: "+count, "Content of this collection"));	
		if (!proxy.showContent)
		{
			// reserve the space to avoid height changes when playing stopping. Don't like that.
			EditorGUILayout.LabelField("","");
			return;
		}else{
			EditorGUI.indentLevel = 1;
			proxy.contentPreviewStartIndex = EditorGUILayout.IntSlider("Start Index",proxy.contentPreviewStartIndex,0,count-1);
	
			proxy.contentPreviewMaxRows = EditorGUILayout.IntSlider("Max Rows",proxy.contentPreviewMaxRows,1,maxPreviewContent);
			proxy.liveUpdate= EditorGUILayout.Toggle(new GUIContent("Live update", "Refresh the content: use when the system set values on the fly"), proxy.liveUpdate);
			
			
			
		}
	//	FsmEditorGUILayout.LightDivider(); // can't use throws errors
		
	}// BuildPreviewInspectorHeaderGUI
	
	
	
	protected void BuildPreFillInspectorGUI(bool withKeys)
	{
		PlayMakerCollectionProxy proxy = (PlayMakerCollectionProxy)target;
		
		string preFillSuffix = "";
		if (!proxy.showContent){
		
			preFillSuffix = proxy.preFillType.ToString() + " ("+proxy.preFillCount + ")";
		}

			if (duplicatedKey)
			{
				duplicatedKey = false;
			}
		
		proxy.showContent = EditorGUILayout.Foldout(proxy.showContent, new GUIContent("PreFilled data : "+preFillSuffix , "Author time content definition"));

		GUI.contentColor = Color.white;
		
		if (proxy.showContent){

			EditorGUI.indentLevel = 1;
			
			proxy.preFillType =  (PlayMakerHashTableProxy.VariableEnum)EditorGUILayout.EnumPopup("Prefill type", proxy.preFillType);               
			int newPrefillCount  = Mathf.Max(0,EditorGUILayout.IntField("Prefill count",proxy.preFillCount));
			if (proxy.preFillCount != newPrefillCount)
			{
				//Debug.Log("new prefill count");
				proxy.preFillCount = newPrefillCount;
			 	proxy.cleanPrefilledLists();
			}
			
			if (withKeys){
				proxy.condensedView  = (bool)EditorGUILayout.Toggle("Condensed view", proxy.condensedView);	
			}
			
			if (proxy.preFillType==PlayMakerCollectionProxy.VariableEnum.Texture)
				proxy.TextureElementSmall= EditorGUILayout.Toggle("Small thumbs", proxy.TextureElementSmall);
			
			proxy.liveUpdate= EditorGUILayout.Toggle("Live update", proxy.liveUpdate);
			//FsmEditorGUILayout.LightDivider();
			// debug to check the real array count
			//EditorGUILayout.LabelField("preFill real count",proxy.preFillVector3List.Count.ToString());
			
			switch (proxy.preFillType){
					case (PlayMakerCollectionProxy.VariableEnum.Bool):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillBoolList.Count<(i+1)){
								proxy.preFillBoolList.Add(false);
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
									buildKeyField(proxy,i);
									proxy.preFillBoolList[i]= EditorGUILayout.Toggle(proxy.preFillBoolList[i]);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillBoolList[i]= EditorGUILayout.Toggle("Item "+i, proxy.preFillBoolList[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.Color):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillColorList.Count<(i+1)){
								proxy.preFillColorList.Add(new Color(1.0f,1.0f,1.0f));
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
								
									buildKeyField(proxy,i);
									proxy.preFillColorList[i]= EditorGUILayout.ColorField(proxy.preFillColorList[i]);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
									proxy.preFillColorList[i]= EditorGUILayout.ColorField("Item "+i, proxy.preFillColorList[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.Float):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillFloatList.Count<(i+1)){
								proxy.preFillFloatList.Add(0f);
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
								
									buildKeyField(proxy,i);
									proxy.preFillFloatList[i]= EditorGUILayout.FloatField(proxy.preFillFloatList[i]);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillFloatList[i]= EditorGUILayout.FloatField("Item "+i, proxy.preFillFloatList[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.GameObject):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillGameObjectList.Count<(i+1)){
								proxy.preFillGameObjectList.Add(null);
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
									buildKeyField(proxy,i);
									proxy.preFillGameObjectList[i]= (GameObject)EditorGUILayout.ObjectField(proxy.preFillGameObjectList[i],typeof(GameObject),true);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillGameObjectList[i]= (GameObject)EditorGUILayout.ObjectField("Item "+i, proxy.preFillGameObjectList[i],typeof(GameObject),true);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.Int):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillIntList.Count<(i+1)){
								proxy.preFillIntList.Add(0);
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
								
									buildKeyField(proxy,i);
									proxy.preFillIntList[i]= EditorGUILayout.IntField(proxy.preFillIntList[i]);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillIntList[i]= EditorGUILayout.IntField("Item "+i, proxy.preFillIntList[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.Material):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillMaterialList.Count<(i+1)){
								proxy.preFillMaterialList.Add(null);
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){

									buildKeyField(proxy,i);
									proxy.preFillMaterialList[i]= (Material)EditorGUILayout.ObjectField(proxy.preFillMaterialList[i],typeof(Material),false);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillMaterialList[i]= (Material)EditorGUILayout.ObjectField("Item "+i, proxy.preFillMaterialList[i],typeof(Material),false);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					/*
					case (PlayMakerCollectionProxy.VariableEnum.Object):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillObjectList.Count<(i+1)){
								proxy.preFillObjectList.Add(null);
							}
							System.Type type = Types.GetType("material","");
					
							//proxy.preFillObjectTypeIndex =EditorGUILayout.  EditorGUILayout.Popup(proxy.preFillObjectTypeIndex,);
							//System.Type type = FsmEditorUtility.ObjectTypeList[preFillObjectTypeIndex];
						//	proxy.preFillObjectList[i]= (typeof("material"))EditorGUILayout.ObjectField("Item "+i, proxy.preFillGameObjectList[i],type,true);
						}
						break;
					*/
					case (PlayMakerCollectionProxy.VariableEnum.Quaternion):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillQuaternionList.Count<(i+1)){
								proxy.preFillQuaternionList.Add(Quaternion.identity);
							}
							Quaternion q = proxy.preFillQuaternionList[i];
							Vector4 quat = new Vector4(q[0],q[1],q[2],q[3]);
								
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
					
							if( proxy.condensedView && withKeys){
								
									buildKeyField(proxy,i);
									
									EditorGUILayout.LabelField("x","",GUILayout.MinWidth(16),GUILayout.Width(16));
									quat[0] = EditorGUILayout.FloatField(quat[0],GUILayout.MinWidth(0),GUILayout.Width(30));
									EditorGUILayout.LabelField("y","",GUILayout.MinWidth(16),GUILayout.Width(16));
									quat[1] = EditorGUILayout.FloatField(quat[1],GUILayout.MinWidth(0),GUILayout.Width(30));
									EditorGUILayout.LabelField("z","",GUILayout.MinWidth(16),GUILayout.Width(16));
									quat[2] = EditorGUILayout.FloatField(quat[2],GUILayout.MinWidth(0),GUILayout.Width(30));
									EditorGUILayout.LabelField("w","",GUILayout.MinWidth(16),GUILayout.Width(16));
									quat[3] = EditorGUILayout.FloatField(quat[3],GUILayout.MinWidth(0),GUILayout.Width(30));
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								quat = EditorGUILayout.Vector4Field("Item "+i,quat );
							}
							EditorGUILayout.EndHorizontal();
							
							q[0] = quat[0];
							q[1] = quat[1];
							q[2] = quat[2];
							q[3] = quat[3];
							proxy.preFillQuaternionList[i] = q;
		
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.Rect):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillRectList.Count<(i+1)){
								proxy.preFillRectList.Add(new Rect(0f,0f,0f,0f));
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
									buildKeyField(proxy,i);
									proxy.preFillRectList[i]= EditorGUILayout.RectField(proxy.preFillRectList[i]);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillRectList[i]= EditorGUILayout.RectField("Item "+i, proxy.preFillRectList[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.String):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillStringList.Count<(i+1)){
								proxy.preFillStringList.Add("");
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
							
									buildKeyField(proxy,i);
									proxy.preFillStringList[i]= EditorGUILayout.TextField(proxy.preFillStringList[i]);
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillStringList[i]= EditorGUILayout.TextField("Item "+i, proxy.preFillStringList[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
					case (PlayMakerCollectionProxy.VariableEnum.Texture):
						
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillTextureList.Count<(i+1)){
								proxy.preFillTextureList.Add(null);
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
					
					
							if( proxy.condensedView && withKeys){
								
								buildKeyField(proxy,i);
								if (proxy.TextureElementSmall){
									GUILayout.BeginHorizontal();
										GUILayout.Label(proxy.preFillTextureList[i]);
										
										proxy.preFillTextureList[i]= (Texture2D)EditorGUILayout.ObjectField(proxy.preFillTextureList[i],typeof(Texture2D),false);
									GUILayout.EndHorizontal();
									
								}else{
								
									GUILayout.FlexibleSpace();
									proxy.preFillTextureList[i]= (Texture2D)EditorGUILayout.ObjectField("", proxy.preFillTextureList[i],typeof(Texture2D),false);
									GUILayout.Space(5);
								
								}
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}

								
									GUILayout.BeginHorizontal();
						
										if (!withKeys)
										{
											GUILayout.Space(15);
											GUILayout.Label("item "+i);	
										}
										
										if (proxy.TextureElementSmall){
											
										if (withKeys)
										{
											EditorGUILayout.LabelField("");	
										}
											proxy.preFillTextureList[i]= (Texture2D)EditorGUILayout.ObjectField(proxy.preFillTextureList[i],typeof(Texture2D),false);
											
										}else{
											GUILayout.FlexibleSpace();
											proxy.preFillTextureList[i]= (Texture2D)EditorGUILayout.ObjectField("",proxy.preFillTextureList[i],typeof(Texture2D),false);
											GUILayout.Space(5);
										}
								
									GUILayout.EndHorizontal();

							}

							EditorGUILayout.EndHorizontal();
						}
						break;
				
					case (PlayMakerCollectionProxy.VariableEnum.Vector2):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillVector2List.Count<(i+1)){
								proxy.preFillVector2List.Add(new Vector2(0f,0f));
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
								
									buildKeyField(proxy,i);
									
									Vector2 tmpVector2 = proxy.preFillVector2List[i];
									EditorGUILayout.LabelField("x","",GUILayout.MinWidth(16),GUILayout.Width(16));
									tmpVector2.x = EditorGUILayout.FloatField(tmpVector2.x,GUILayout.MinWidth(0),GUILayout.Width(30));
									EditorGUILayout.LabelField("y","",GUILayout.MinWidth(16),GUILayout.Width(16));
									tmpVector2.y = EditorGUILayout.FloatField(tmpVector2.y,GUILayout.MinWidth(0),GUILayout.Width(30));
									proxy.preFillVector2List[i] = tmpVector2;
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
						
								proxy.preFillVector2List[i]= EditorGUILayout.Vector2Field("Item "+i, proxy.preFillVector2List[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
				
					case (PlayMakerCollectionProxy.VariableEnum.Vector3):
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillVector3List.Count<(i+1)){
								proxy.preFillVector3List.Add(new Vector3(0f,0f,0f));
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
								
									buildKeyField(proxy,i);
									
									Vector3 tmpVector3 = proxy.preFillVector3List[i];
									EditorGUILayout.LabelField("x","",GUILayout.MinWidth(16),GUILayout.Width(16));
									tmpVector3.x = EditorGUILayout.FloatField(tmpVector3.x,GUILayout.MinWidth(0),GUILayout.Width(30));
									EditorGUILayout.LabelField("y","",GUILayout.MinWidth(16),GUILayout.Width(16));
									tmpVector3.y = EditorGUILayout.FloatField(tmpVector3.y,GUILayout.MinWidth(0),GUILayout.Width(30));
									EditorGUILayout.LabelField("z","",GUILayout.MinWidth(16),GUILayout.Width(16));
									tmpVector3.z = EditorGUILayout.FloatField(tmpVector3.z,GUILayout.MinWidth(0),GUILayout.Width(30));
									proxy.preFillVector3List[i] = tmpVector3;
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
						
								proxy.preFillVector3List[i]= EditorGUILayout.Vector3Field("Item "+i, proxy.preFillVector3List[i]);
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
				
				
				case (PlayMakerCollectionProxy.VariableEnum.AudioClip):
						
						for(int i=0;i<proxy.preFillCount;i++){
							if (proxy.preFillAudioClipList.Count<(i+1)){
								proxy.preFillAudioClipList.Add(null);
							}
							EditorGUILayout.BeginHorizontal();
							buildItemSelector(i);
							if( proxy.condensedView && withKeys){
								
									buildKeyField(proxy,i);
									
									proxy.preFillAudioClipList[i]= (AudioClip)EditorGUILayout.ObjectField(proxy.preFillAudioClipList[i],typeof(AudioClip),false);
									
								
							}else{
								if (withKeys) {
									buildKeyField(proxy,i);
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.BeginHorizontal();
								}
								proxy.preFillAudioClipList[i]= (AudioClip)EditorGUILayout.ObjectField("Item "+i, proxy.preFillAudioClipList[i],typeof(AudioClip),false);
								
							}
							EditorGUILayout.EndHorizontal();
						}
						break;
				
					default:
						//ERROR
						break;
				}
			if (duplicatedKey)
			{
				//FsmEditorGUILayout.Divider(); //.LightDivider();
			Color col = GUI.color;
				GUI.color = Color.red;
				GUILayout.Label("WARNING: Keys must be unique",EditorStyles.whiteLargeLabel);
				GUI.color = col;
			}
				EditorGUI.indentLevel = 0;
		}
		

	}// BuildPreFillInspectorGUI
	
	
	
	protected void buildItemSelector(int i){
	//	EditorGUILayout.Toggle(true,GUILayout.MaxWidth(14));
	}
	
	
	
	protected void buildKeyField(PlayMakerCollectionProxy proxy,int i){
		

		
		if (proxy.preFillKeyList.Count<(i+1)){
			proxy.preFillKeyList.Add("key "+i);
		}
		
		string key = proxy.preFillKeyList[i];
		// there is probably a better way to check for duplicates. maybe actually make an actual hastable list for real.
		List<string> results = proxy.preFillKeyList.FindAll(delegate(string aString)
            {
                return aString == key;
            });
		
		
		if (!duplicatedKey)
			duplicatedKey = results.Count>1;

		
		
		Color col = GUI.color;
		
		if (proxy.condensedView){
			if (results.Count>1)
			{
				
				GUI.color = Color.red;
				//EditorGUILayout.LabelField("!!!","",GUILayout.Width(20));
				   
			}
			proxy.preFillKeyList[i]= EditorGUILayout.TextField(proxy.preFillKeyList[i],GUILayout.MinWidth(30));//,GUILayout.Width(50));
		}else{
			if (results.Count>1)
			{
				GUI.color = Color.red;
			}
			proxy.preFillKeyList[i]= EditorGUILayout.TextField("key "+i,proxy.preFillKeyList[i]);	
		}
		
		if (results.Count>1){
			GUI.color = col;
		}
		

		
		// check if the key doesn't exists alread
		//proxy.preFillKeyList[i] = key;
	}
	
//	protected void buildHashTableElement(out IList aList, int it){
		
	//	aList[i].GetType
		
	//}

}
