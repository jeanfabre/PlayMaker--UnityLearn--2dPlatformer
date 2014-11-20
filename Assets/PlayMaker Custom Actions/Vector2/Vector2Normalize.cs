// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]
	[Tooltip("Normalizes a Vector2 Variable.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1019")]
	public class Vector2Normalize : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector to normalize")]
		public FsmVector2 vector2Variable;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			vector2Variable.Value = vector2Variable.Value.normalized;
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnUpdate()
		{
			vector2Variable.Value = vector2Variable.Value.normalized;
		}
	}
}

