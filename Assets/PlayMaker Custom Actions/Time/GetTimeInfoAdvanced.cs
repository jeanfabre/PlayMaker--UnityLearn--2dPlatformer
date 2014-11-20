// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
					]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets various useful Time measurements.\n Advanced features allows selection of update type.")]
	public class GetTimeInfoAdvanced : FsmStateAction
	{
		public enum TimeInfo
		{
			DeltaTime,
			TimeScale,
			SmoothDeltaTime,
			TimeInCurrentState,
			TimeSinceStartup,
			TimeSinceLevelLoad,
			RealTimeSinceStartup,
			RealTimeInCurrentState
		}
		
		public TimeInfo getInfo;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;
		
		public bool everyFrame;

		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;

		public override void Reset()
		{
			getInfo = TimeInfo.TimeSinceLevelLoad;
			storeValue = null;
			everyFrame = false;
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
		}
		
		public override void Awake()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				Fsm.HandleFixedUpdate = true;
			}
		}
		
		public override void OnUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate)
			{
				DoGetTimeInfo();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnLateUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				DoGetTimeInfo();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				DoGetTimeInfo();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		void DoGetTimeInfo()
		{
			switch (getInfo) 
			{
				
			case TimeInfo.DeltaTime:
				storeValue.Value = Time.deltaTime;
				break;
				
			case TimeInfo.TimeScale:
				storeValue.Value = Time.timeScale;
				break;
				
			case TimeInfo.SmoothDeltaTime:
				storeValue.Value = Time.smoothDeltaTime;
				break;
				
			case TimeInfo.TimeInCurrentState:
				storeValue.Value = State.StateTime;
				break;
				
			case TimeInfo.TimeSinceStartup:
				storeValue.Value = Time.time;
				break;
				
			case TimeInfo.TimeSinceLevelLoad:
				storeValue.Value = Time.timeSinceLevelLoad;
				break;
				
			case TimeInfo.RealTimeSinceStartup:
				storeValue.Value = FsmTime.RealtimeSinceStartup;
				break;
				
			case TimeInfo.RealTimeInCurrentState:
				storeValue.Value = FsmTime.RealtimeSinceStartup - State.RealStartTime;
				break;
				
			default:
				storeValue.Value = 0f;
				break;
			}
		}
	}
}