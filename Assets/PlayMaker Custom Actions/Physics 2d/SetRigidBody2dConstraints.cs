// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// based on dudeBxl action

#if UNITY_5
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the constraints of a 2D rigidBody")]
	public class SetRigidBody2dConstraints : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The X Position constraint. Leave to none for no effect")]
		public FsmBool freezePositionX;

		[Tooltip("The Y Position constraint. Leave to none for no effect")]
		public FsmBool freezePositionY;

		[Tooltip("The Z Rotation constraint. Leave to none for no effect")]
		public FsmBool freezeRotationZ;

		[Tooltip("freezeAll option. Leave to none for no effect")]
		public FsmBool freezeAll;

		public override void Reset()
		{
			gameObject = null;
			freezePositionX = new FsmBool() {UseVariable=true};
			freezePositionY =  new FsmBool() {UseVariable=true};
			freezeRotationZ =  new FsmBool() {UseVariable=true};
			freezeAll =  new FsmBool() {UseVariable=true};
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

			Rigidbody2D _rb2d = go.GetComponent<Rigidbody2D>();
			if ( _rb2d == null)
			{
				LogError("RigidBody Component required");
				return;
			}



			_rb2d.constraints = RigidbodyConstraints2D.None;

			if (!freezeAll.IsNone)
			{
				_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezeAll;
			}

			if (!freezePositionX.IsNone)
			{
				_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezePositionX;
			}

			if (!freezePositionY.IsNone)
			{
				_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezePositionY;
			}
			
			if (!freezeRotationZ.IsNone)
			{
				_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezeRotation;
			}


			return;

		}
	}
}
#endif