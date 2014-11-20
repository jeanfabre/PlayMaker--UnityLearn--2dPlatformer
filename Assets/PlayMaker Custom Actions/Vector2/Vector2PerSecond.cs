// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]
	[Tooltip("Multiplies a Vector2 variable by Time.deltaTime. Useful for frame rate independent motion.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1021")]
	public class Vector2PerSecond : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2")]
		public FsmVector2 vector2Variable;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			vector2Variable.Value = vector2Variable.Value * Time.deltaTime;
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			vector2Variable.Value = vector2Variable.Value * Time.deltaTime;
		}
	}
}

