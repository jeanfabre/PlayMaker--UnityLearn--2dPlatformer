// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Set the isTrigger option of a Collider2D. Optionally set all collider2D found on the gameobject Target.")]
	public class SetCollider2dIsTrigger : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Collider2D))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmBool isTrigger;
		

		public bool setAllColliders;

		public override void Reset()
		{
			gameObject = null;
			isTrigger = false;
			setAllColliders = false;
		}
		
		public override void OnEnter()
		{
			DoSetIsTrigger();
			Finish();
		}
		
		void DoSetIsTrigger()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			
			if (setAllColliders)
			{
				// Find all of the colliders on the gameobject and set them all to be triggers.
				Collider2D[] cols = go.GetComponents<Collider2D> ();
				foreach (Collider2D c in cols) {
						c.isTrigger = isTrigger.Value;
				}
			}else{
				if (go.GetComponent<Collider2D>() != null)go.GetComponent<Collider2D>().isTrigger  = isTrigger.Value;
			}
		}
	}
}

