// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Check if an AudioSource is playing.")]
	public class GetAudioIsPlaying : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the audioSource is playing")]
		public FsmBool isPlaying;
		
		[Tooltip("Event to send if the audioSource is playing")]
		public FsmEvent isPlayingEvent;
		
		[Tooltip("Event to send if the audioSource is not playing")]
		public FsmEvent isNotPlayingEvent;

		public override void Reset()
		{ 
			gameObject = null;
			isPlaying =null;
			isPlayingEvent = null;
			isNotPlayingEvent = null;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			

			if (go.audio == null)
			{
				LogError("SwapSingleSprite: Missing AudioSource!");
				return;
			}

			bool _isPlaying = go.audio.isPlaying;
			isPlaying.Value = _isPlaying;

			Fsm.Event(_isPlaying ? isPlayingEvent : isNotPlayingEvent);

			Finish();
		}
	}
}