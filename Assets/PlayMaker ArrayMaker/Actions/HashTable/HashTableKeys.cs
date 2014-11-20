//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerHashTableProxy script onto a GameObject, and define a unique name for reference if several PlayMakerHashTableProxy coexists on that GameObject.
// In this Action interface, link that GameObject in "hashTableObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/HashTable")]
	[Tooltip("Store all the keys of a PlayMaker HashTable Proxy component (PlayMakerHashTableProxy) into a PlayMaker arrayList Proxy component (PlayMakerArrayListProxy).")]
	public class HashTableKeys : HashTableActions
	{
		
		[ActionSection("Set up")]

		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker HashTable Proxy component")]
		[CheckForComponent(typeof(PlayMakerHashTableProxy))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Author defined Reference of the PlayMaker HashTable Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component that will store the keys")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault arrayListGameObject;

		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component that will store the keys ( necessary if several component coexists on the same GameObject")]
		public FsmString arrayListReference;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			
			arrayListGameObject = null;
			arrayListReference = null;

		}
		
		
		public override void OnEnter()
		{
			if (SetUpHashTableProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value))
				doHashTableKeys();

			Finish();
		}
		
		
		public void doHashTableKeys()
		{
			if (!isProxyValid()) 
				return;
			
			// now get the arrayList proxy.
			
			GameObject ArrayListGO = Fsm.GetOwnerDefaultTarget(arrayListGameObject);
			
			if (ArrayListGO == null){
					return ;
			}
			 PlayMakerArrayListProxy arrayListproxy = GetArrayListProxyPointer(ArrayListGO,arrayListReference.Value,false);

			if (arrayListproxy != null)

				arrayListproxy.arrayList.AddRange(proxy.hashTable.Keys);
					
		
		}
		
	}
}