// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]
	[Tooltip("Sets the value of a Vector2 Variable.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1008")]
	public class SetVector2Value : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;
		
		[RequiredField]
		[Tooltip("The vector2 source")]
		public FsmVector2 vector2Value;
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			vector2Value = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			vector2Variable.Value = vector2Value.Value;
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			vector2Variable.Value = vector2Value.Value;
		}
	}
}

