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

		[Tooltip("freezeAll option. Leave to none for no effect")]
		public FsmBool freezeAll;

		[Tooltip("The Position (XY) constraint. Leave to none for no effect")]
		public FsmBool freezePosition;

		[Tooltip("The X Position constraint. Leave to none for no effect")]
		public FsmBool freezePositionX;

		[Tooltip("The Y Position constraint. Leave to none for no effect")]
		public FsmBool freezePositionY;

		[Tooltip("The Z Rotation constraint. Leave to none for no effect")]
		public FsmBool freezeRotationZ;



		public override void Reset()
		{
			gameObject = null;
			freezePositionX = new FsmBool() {UseVariable=true};
			freezePositionY =  new FsmBool() {UseVariable=true};
			freezeRotationZ =  new FsmBool() {UseVariable=true};
			freezeAll =  new FsmBool() {UseVariable=true};
			freezePosition =  new FsmBool() {UseVariable=true};
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


			if (!freezeAll.IsNone)
			{
				if (freezeAll.Value) {
					_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezeAll;
				}else{

					_rb2d.constraints = _rb2d.constraints & ~RigidbodyConstraints2D.FreezeAll;	
				}
			}

			if (!freezePosition.IsNone)
			{
				if (freezePosition.Value) {
					_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezePosition;
				}else{

					_rb2d.constraints = _rb2d.constraints & ~RigidbodyConstraints2D.FreezePosition;	
				}
			}

			if (!freezePositionX.IsNone)
			{
				if (freezePositionX.Value) {
					_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezePositionX;
				}else{

					_rb2d.constraints = _rb2d.constraints & ~RigidbodyConstraints2D.FreezePositionX;	
				}
			}

			if (!freezePositionY.IsNone)
			{
				if (freezePositionY.Value) {
					_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezePositionY;
				}else{

					_rb2d.constraints = _rb2d.constraints & ~RigidbodyConstraints2D.FreezePositionY;	
				}
			}
			
			if (!freezeRotationZ.IsNone)
			{
				if (freezeRotationZ.Value) {
					_rb2d.constraints = _rb2d.constraints | RigidbodyConstraints2D.FreezeRotation;
				}else{

					_rb2d.constraints = _rb2d.constraints & ~RigidbodyConstraints2D.FreezeRotation;	
				}
			}


			return;

		}
	}
}
#endif