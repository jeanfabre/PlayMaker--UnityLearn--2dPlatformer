//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Return the farthest GameObject within an arrayList from a transform or position.")]
	public class ArrayListGetFarthestGameObject : ArrayListActions
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("Compare the distance of the items in the list to the position of this gameObject")]
		public FsmGameObject distanceFrom;
		
		[Tooltip("If DistanceFrom declared, use OrDistanceFromVector3 as an offset")]
		public FsmVector3 orDistanceFromVector3;
		
		public bool everyframe;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmGameObject farthestGameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmInt farthestIndex;
		
		
		public override void Reset()
		{
		
			gameObject = null;
			reference = null;
			distanceFrom = null;
			orDistanceFromVector3 = null;
			farthestGameObject = null;
			farthestIndex = null;
			
			everyframe = true;
		}
		
		
		public override void OnEnter()
		{

			if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				Finish();
			}
			
			DoFindFarthestGo();
			
			if (!everyframe)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoFindFarthestGo();
		}
		
		void DoFindFarthestGo()
		{
			
			if (! isProxyValid())
			{
				return;
			}
			
			Vector3 root = orDistanceFromVector3.Value;
			
			GameObject _rootGo = distanceFrom.Value;
			if (_rootGo!=null)
			{
				root += _rootGo.transform.position;
			}
			
			float sqrDist = Mathf.Infinity;
		
			int _index = 0;
			float sqrDistTest;
			foreach(GameObject _go in proxy.arrayList)
			{
				
				if (_go!=null) 
				{
					sqrDistTest = (_go.transform.position - root).sqrMagnitude;
					if (sqrDistTest<= sqrDist)
					{
						sqrDist = sqrDistTest;
						farthestGameObject.Value = _go;
						farthestIndex.Value = _index;
					}
				}
				_index++;
			}

		}
		
	}
}