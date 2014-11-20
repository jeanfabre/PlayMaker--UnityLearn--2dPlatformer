//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// root of any Collections Actions ( arrayList, HashTable for now)

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{

	[Tooltip("Collections base action - don't use!")]
	public abstract class CollectionsActions : FsmStateAction
	{
		// Really, this should go as a built in system for playmaker to expose an interface selector for a given fsmVariable type.
		// right now, I can't find another way then have the user select the type, and expose all variables type.
		// I am sure it's there... just could not find it, find one with an Unknown entry tho, not just the usable ones.
		public enum FsmVariableEnum{
		 FsmGameObject,FsmInt,FsmFloat,FsmString,FsmBool,FsmVector2,FsmVector3,FsmRect,FsmQuaternion,FsmColor,FsmMaterial,FsmTexture,FsmObject
		}
		
		
		protected PlayMakerHashTableProxy GetHashTableProxyPointer(GameObject aProxy,string nameReference,bool silent){
		
			if (aProxy==null){
				if (!silent) Debug.LogError("Null Proxy");
				return null;
			}
			
			PlayMakerHashTableProxy[] proxies = aProxy.GetComponents<PlayMakerHashTableProxy>();
			
			if (proxies.Length>1){
				
				if (nameReference == ""){
					if (!silent) Debug.LogWarning("Several HashTable Proxies coexists on the same GameObject and no reference is given to find the expected HashTable");
				}
				
				foreach (PlayMakerHashTableProxy iProxy in proxies) {
	        		if (iProxy.referenceName == nameReference){
						return iProxy;
					}
   			 	}
				
				if (nameReference != ""){
					if (!silent) Debug.LogError("HashTable Proxy not found for reference <"+nameReference+">");
					return null;
				}
				
				
			}else if (proxies.Length>0){
				
				if (nameReference!="" && nameReference != proxies[0].referenceName){
					if (!silent) Debug.LogError("HashTable Proxy reference do not match");
					return null;
				}
				return proxies[0];	
			}
			
			if (!silent) 
			{
				Debug.LogError("HashTable not found");
			}
			return null;
		}// getHashTableProxyPointer	
		
		
		
		protected PlayMakerArrayListProxy GetArrayListProxyPointer(GameObject aProxy,string nameReference,bool silent){
					
				if (aProxy==null){
					if (!silent) Debug.LogError("Null Proxy");
					return null;
				}
				
			
				PlayMakerArrayListProxy[] proxies = aProxy.GetComponents<PlayMakerArrayListProxy>();
				if (proxies.Length>1){
				
					if (nameReference == ""){
						if (!silent) Debug.LogError("Several ArrayList Proxies coexists on the same GameObject and no reference is given to find the expected ArrayList");
					}
					
					foreach (PlayMakerArrayListProxy iProxy in proxies) {
		        		if (iProxy.referenceName == nameReference){
							return iProxy;
						}
	   			 	}
	
					if (nameReference != ""){
						if (!silent) LogError("ArrayList Proxy not found for reference <"+nameReference+">");
						return null;
					}
						
				}else if (proxies.Length>0){
					if (nameReference!="" && nameReference != proxies[0].referenceName){
						if (!silent) Debug.LogError("ArrayList Proxy reference do not match");
						return null;
					}
					
					return proxies[0];
						
				}
				
				if (!silent)
				{
					LogError("ArrayList proxy not found");
				}
				return null;
			}// GetArrayListProxyPointer		

		
		
	}
}