// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]
	[Tooltip("Adds a XY values to Vector2 Variable.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1011")]
	public class Vector2AddXY : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;
		
		[Tooltip("The x component to add")]
		public FsmFloat addX;
		[Tooltip("The y component to add")]
		public FsmFloat addY;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
		
		[Tooltip("Add the value on a per second bases.")]
		public bool perSecond;

		public override void Reset()
		{
			vector2Variable = null;
			addX = 0;
			addY = 0;
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoVector2AddXYZ();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector2AddXYZ();
		}
		
		void DoVector2AddXYZ()
		{
			var vector = new Vector2(addX.Value, addY.Value);
			
			if(perSecond)
			{
				vector2Variable.Value += vector * Time.deltaTime;
			}
			else
			{
				vector2Variable.Value += vector;
			}
				
		}
	}
}

