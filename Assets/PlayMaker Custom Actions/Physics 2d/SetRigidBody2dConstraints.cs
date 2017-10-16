// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// keywords: constraint

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the constraints of a 2D rigidBody")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12365.0")]
	public class SetRigidBody2dConstraints : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
		
		public FsmBool freezePositionX;
		
		public FsmBool freezePositionY;

		
		public FsmBool freezeRotationZ;

		public FsmBool freezeAll;


		public override void Reset()
		{
			gameObject = null;
			freezePositionX = false;
			freezePositionY = false;

			
			freezeRotationZ = false;

			freezeAll = false;
		}

		public override void OnEnter()
		{
			DoSetConstraints();
			
			
			Finish();		
		}

		void DoSetConstraints()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				LogError("gameObject is null");
				return;
			}
			
			
			if (go.GetComponent<Rigidbody2D>() == null)
			{
				LogError("RigidBody Component required");
				return;
			}
			
			go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

			if (freezeAll.Value)
			{
				go.GetComponent<Rigidbody2D>().constraints = go.GetComponent<Rigidbody2D>().constraints | RigidbodyConstraints2D.FreezeAll ;
			}

			if (freezePositionX.Value)
			{
				go.GetComponent<Rigidbody2D>().constraints = go.GetComponent<Rigidbody2D>().constraints | RigidbodyConstraints2D.FreezePositionX ;
			}
			if (freezePositionY.Value)
			{
				go.GetComponent<Rigidbody2D>().constraints = go.GetComponent<Rigidbody2D>().constraints | RigidbodyConstraints2D.FreezePositionY ;
			}
			
			if (freezeRotationZ.Value)
			{
				go.GetComponent<Rigidbody2D>().constraints = go.GetComponent<Rigidbody2D>().constraints | RigidbodyConstraints2D.FreezeRotation ;
			}

			return;

		}
	}
}
