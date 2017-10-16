// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
#else
#define UNITY_5_2_OR_NEWER
#endif

using UnityEngine;
using uUI = UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;
using HutongGames.PlayMaker;

public class PlayMakerUGuiComponentProxy : MonoBehaviour {


	#region internal variables helpers
	public bool debug = false;
	public enum ActionType {SendFsmEvent,SetFsmVariable};
	public enum PlayMakerProxyEventTarget {Owner,GameObject,BroadCastAll,FsmComponent};
	public enum PlayMakerProxyVariableTarget {Owner,GameObject,GlobalVariable,FsmComponent};

	[System.Serializable]
	public struct FsmVariableSetup
	{
		public PlayMakerProxyVariableTarget target;
		public GameObject gameObject;
		public PlayMakerFSM fsmComponent;

		public int fsmIndex;
		public int variableIndex;

		public VariableType variableType;
		public string variableName;
	}

	[System.Serializable]
	public struct FsmEventSetup
	{
		public PlayMakerProxyEventTarget target;
		public GameObject gameObject;
		public PlayMakerFSM fsmComponent;
		public string customEventName;
		public string builtInEventName;
		public bool sendtoChildren;
	}

	string error;

	#endregion

	#region set up variables

	public OwnerDefaultOption UiTargetOption;
	public GameObject UiTarget;

	public ActionType action;

	// Variable target
	public FsmVariableSetup fsmVariableSetup;
	FsmFloat fsmFloatTarget;
	FsmBool fsmBoolTarget;
	FsmVector2 fsmVector2Target;
	FsmString fsmStringTarget;
	FsmInt fsmIntTarget;

	// event target
	public FsmEventSetup fsmEventSetup;
	FsmEventTarget fsmEventTarget;	

	bool WatchInputField;
	uUI.InputField inputField;
	string lastInputFieldValue;

	#endregion

	#region MonoBehavior
	void Start()
	{
		if (action == ActionType.SetFsmVariable)
		{
			SetupVariableTarget();
		}else{
			SetupEventTarget();
		}

		SetupUiListeners();


	}

	void Update()
	{
		if (WatchInputField && inputField!=null)
		{
			if ( !inputField.text.Equals(lastInputFieldValue))
			{
				lastInputFieldValue = inputField.text;
				SetFsmVariable(lastInputFieldValue);
			}
		}
	}

	#endregion

	#region Initial Setup

	void SetupEventTarget()
	{
		if (fsmEventTarget==null)
		{
			fsmEventTarget = new FsmEventTarget();
		}

		// BROADCAST
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.BroadCastAll)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.BroadcastAll;
			fsmEventTarget.excludeSelf = false;
		}

		// FSM COMPONENT
		else if ( fsmEventSetup.target == PlayMakerProxyEventTarget.FsmComponent)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.FSMComponent;
			fsmEventTarget.fsmComponent = fsmEventSetup.fsmComponent;
		}

		// GAMEOBJECT
		else if(fsmEventSetup.target == PlayMakerProxyEventTarget.GameObject)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.GameObject;
			fsmEventTarget.gameObject = new FsmOwnerDefault();
			fsmEventTarget.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			fsmEventTarget.gameObject.GameObject.Value = fsmEventSetup.gameObject;
		}

		// OWNER
		else if ( fsmEventSetup.target == PlayMakerProxyEventTarget.Owner)
		{
			fsmEventTarget.ResetParameters();
			fsmEventTarget.target = FsmEventTarget.EventTarget.GameObject;
			fsmEventTarget.gameObject = new FsmOwnerDefault();
			fsmEventTarget.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			fsmEventTarget.gameObject.GameObject.Value = this.gameObject;

		}


	}

	void SetupVariableTarget()
	{
			// GLOBAL VARIABLE
			if (fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.GlobalVariable)
			{
				if (fsmVariableSetup.variableType == VariableType.Bool)
				{
					fsmBoolTarget = FsmVariables.GlobalVariables.FindFsmBool(fsmVariableSetup.variableName);
					
				}else if (fsmVariableSetup.variableType == VariableType.Float)
				{
					fsmFloatTarget = FsmVariables.GlobalVariables.FindFsmFloat(fsmVariableSetup.variableName);
				}else if (fsmVariableSetup.variableType == VariableType.Vector2)
				{
					fsmVector2Target = FsmVariables.GlobalVariables.FindFsmVector2(fsmVariableSetup.variableName);

				}else if (fsmVariableSetup.variableType == VariableType.String)
				{
					fsmStringTarget = FsmVariables.GlobalVariables.FindFsmString(fsmVariableSetup.variableName);
				}

			}
			
			// FSM COMPONENT
			else if (fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.FsmComponent)
			{
				if (fsmVariableSetup.fsmComponent!=null)
				{
					if (fsmVariableSetup.variableType == VariableType.Bool)
					{
						fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
						
					}else if (fsmVariableSetup.variableType == VariableType.Float)
					{
						fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);

					}else if (fsmVariableSetup.variableType == VariableType.Vector2)
					{
						fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);

					}else if (fsmVariableSetup.variableType == VariableType.String)
					{
						fsmStringTarget =fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
					}
				}else{
					Debug.LogError("set to target a FsmComponent but fsmEventTarget.target is null");
				}
			}
			
			// OWNER
			else if (fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.Owner)
			{
				if (fsmVariableSetup.gameObject!=null)
				{
					if (fsmVariableSetup.fsmComponent!=null)
					{
						if (fsmVariableSetup.variableType == VariableType.Bool)
						{
							fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
							
						}else if (fsmVariableSetup.variableType == VariableType.Float)
						{
							fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);

						}else if (fsmVariableSetup.variableType == VariableType.Vector2)
						{
							fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);

						}else if (fsmVariableSetup.variableType == VariableType.String)
						{
							fsmStringTarget =fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
						}
					}
					
				}else{
					Debug.LogError("set to target Owbner but fsmEventTarget.target is null");
				}
			}

			// GAMEOBJECT
			else if (fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.GameObject)
			{
				if (fsmVariableSetup.gameObject!=null)
				{
					if (fsmVariableSetup.fsmComponent!=null)
					{
						if (fsmVariableSetup.variableType == VariableType.Bool)
						{
							fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
							
						}else if (fsmVariableSetup.variableType == VariableType.Float)
						{
							fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);

						}else if (fsmVariableSetup.variableType == VariableType.Vector2)
						{
							fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);
						}else if (fsmVariableSetup.variableType == VariableType.String)
						{
							fsmStringTarget =fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
						}
					}
					
				}else{
					Debug.LogError("set to target a Gameobject but fsmEventTarget.target is null");
				}
		}
	}

	void SetupUiListeners()
	{
		if (UiTarget.GetComponent<uUI.Button>()!=null)
		{
			UiTarget.GetComponent<uUI.Button>().onClick.AddListener(OnClick);
		}

		if (UiTarget.GetComponent<uUI.Toggle>()!=null)
		{
			UiTarget.GetComponent<uUI.Toggle>().onValueChanged.AddListener(OnValueChanged);
			// force the value because it's not fired when starting ( Unity said they may implement it)
			if (action== ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<uUI.Toggle>().isOn);
			}
		}
		if (UiTarget.GetComponent<uUI.Slider>()!=null)
		{
			UiTarget.GetComponent<uUI.Slider>().onValueChanged.AddListener(OnValueChanged);
			
			// force the value because it's not fired when starting ( Unity said they may implement it)
			if (action== ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<uUI.Slider>().value);
			}
		}
		if (UiTarget.GetComponent<uUI.Scrollbar>()!=null)
		{
			UiTarget.GetComponent<uUI.Scrollbar>().onValueChanged.AddListener(OnValueChanged);
			// force the value because it's not fired when starting ( Unity said they may implement it)
			if (action== ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<uUI.Scrollbar>().value);
			}
		}
		if (UiTarget.GetComponent<uUI.ScrollRect>()!=null)
		{
			UiTarget.GetComponent<uUI.ScrollRect>().onValueChanged.AddListener(OnValueChanged);
			// force the value because it's not fired when starting ( Unity said they may implement it)
			if (action== ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<uUI.ScrollRect>().normalizedPosition);
			}
		}
		if (UiTarget.GetComponent<uUI.InputField>()!=null)
		{
			inputField = UiTarget.GetComponent<uUI.InputField>();
			UiTarget.GetComponent<uUI.InputField>().onEndEdit.AddListener(onEndEdit);
			if (action== ActionType.SetFsmVariable)
			{
				WatchInputField = true;
				lastInputFieldValue = "";
			}
		}

		#if UNITY_5_2_OR_NEWER
		
			if (UiTarget.GetComponent<uUI.Dropdown>()!=null)
			{
				UiTarget.GetComponent<uUI.Dropdown>().onValueChanged.AddListener(OnValueChanged);
				// force the value because it's not fired when starting ( Unity said they may implement it)
				if (action== ActionType.SetFsmVariable)
				{
					SetFsmVariable(UiTarget.GetComponent<uUI.Dropdown>().value);
				}
			}
		#endif
		

	}
	#endregion

	#region UI Listeners


	protected void OnClick()
	{
		if (debug) Debug.Log("OnClick");

		FsmEventData _eventData = HutongGames.PlayMaker.Fsm.EventData;
		this.FirePlayMakerEvent(_eventData);
	}

	protected void OnValueChanged(bool value)
	{
		if (debug) Debug.Log("OnValueChanged(bool): "+value);

		if (action== ActionType.SendFsmEvent)
		{
			FsmEventData _eventData = HutongGames.PlayMaker.Fsm.EventData;
			_eventData.BoolData = value;
			FirePlayMakerEvent(_eventData);
		}else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(int value)
	{
		if (debug) Debug.Log("OnValueChanged(int): "+value);
		
		if (action== ActionType.SendFsmEvent)
		{
			FsmEventData _eventData = HutongGames.PlayMaker.Fsm.EventData;
			_eventData.IntData = value;
			FirePlayMakerEvent(_eventData);
		}else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(float value)
	{
		if (debug) Debug.Log("OnValueChanged(float): "+value);

		if (action== ActionType.SendFsmEvent)
		{
			FsmEventData _eventData = HutongGames.PlayMaker.Fsm.EventData;
			_eventData.FloatData = value;
			FirePlayMakerEvent(_eventData);
		}else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(Vector2 value)
	{
		if (debug) Debug.Log("OnValueChanged(vector2): "+value);
		
		if (action== ActionType.SendFsmEvent)
		{
			FsmEventData _eventData = HutongGames.PlayMaker.Fsm.EventData;
			_eventData.Vector2Data = value;
			FirePlayMakerEvent(_eventData);
		}else
		{
			SetFsmVariable(value);
		}
	}

	protected void onEndEdit(string value)
	{
		if (debug) Debug.Log("onEndEdit(string): "+value);

		if (action== ActionType.SendFsmEvent)
		{
			FsmEventData _eventData = HutongGames.PlayMaker.Fsm.EventData;
			_eventData.StringData = value;
			_eventData.BoolData = inputField.wasCanceled;
			FirePlayMakerEvent(_eventData);
		}else
		{
			SetFsmVariable(value);
		}

	}

	#endregion

	#region tools
	/*
	void SetFsmVariable(object value)
	{
		if (debug) Debug.Log("SetFsmVariable: "+fsmVariableTarget.NamedVar.Name+" "+fsmVariableTarget.DebugString()+" = "+value);
		
		fsmVariableTarget.SetValue(value);
		
	}
*/
	void SetFsmVariable(Vector2 value)
	{
		if (fsmVector2Target!=null)
		{
			if (debug) Debug.Log("PlayMakerUGuiComponentProxy on "+this.name+": Fsm Vector2 set to "+value);
			fsmVector2Target.Value = value;
		}else{
			Debug.LogError("PlayMakerUGuiComponentProxy on "+this.name+": Fsm Vector2 MISSING !!",this.gameObject);
		}
		
	}
	void SetFsmVariable(bool value)
	{
		if (fsmBoolTarget!=null)
		{
			if (debug) Debug.Log("PlayMakerUGuiComponentProxy on "+this.name+": Fsm Bool set to "+value);
			fsmBoolTarget.Value = value;
		}else{
			Debug.LogError("PlayMakerUGuiComponentProxy on "+this.name+": Fsm Bool MISSING !!",this.gameObject);
		}

	}
	void SetFsmVariable(float value)
	{

		if (fsmFloatTarget!=null)
		{
			if (debug) Debug.Log("PlayMakerUGuiComponentProxy on "+this.name+": Fsm Float set to "+value);

			fsmFloatTarget.Value = value;
		}else{
			Debug.LogError("PlayMakerUGuiComponentProxy on "+this.name+": Fsm Float MISSING !!",this.gameObject);
		}
		
	}

	void SetFsmVariable(string value)
	{
		
		if (fsmStringTarget!=null)
		{
			if (debug) Debug.Log("PlayMakerUGuiComponentProxy on "+this.name+": Fsm String set to "+value);
			
			fsmStringTarget.Value = value;
		}else{
			Debug.LogError("PlayMakerUGuiComponentProxy on "+this.name+": Fsm String MISSING !!",this.gameObject);
		}
		
	}

	void FirePlayMakerEvent(FsmEventData eventData)
	{

		fsmEventTarget.excludeSelf = false; // not available in this context, only when even ti sfired from an Fsm.

		fsmEventTarget.sendToChildren = fsmEventSetup.sendtoChildren;

		if (debug) Debug.Log("Fire event: "+GetEventString());

		PlayMakerUtils.SendEventToTarget(null,fsmEventTarget,GetEventString(),eventData);
	}


	public bool DoesTargetImplementsEvent()
	{
	
		string eventName = GetEventString();

		if (fsmEventSetup.target == PlayMakerProxyEventTarget.BroadCastAll)
		{
			return FsmEvent.IsEventGlobal(eventName);
		}
		
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.FsmComponent)
		{
			return PlayMakerUtils.DoesFsmImplementsEvent(fsmEventSetup.fsmComponent,eventName);
		}

		if (fsmEventSetup.target == PlayMakerProxyEventTarget.GameObject)
		{
			return PlayMakerUtils.DoesGameObjectImplementsEvent(fsmEventSetup.gameObject,eventName,fsmEventSetup.sendtoChildren);
		}
		
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.Owner)
		{
			return PlayMakerUtils.DoesGameObjectImplementsEvent(this.gameObject,eventName,fsmEventSetup.sendtoChildren);
		}

		return false;
	}

	string GetEventString()
	{
		return string.IsNullOrEmpty(fsmEventSetup.customEventName)?fsmEventSetup.builtInEventName:fsmEventSetup.customEventName;
	}


	#endregion


}
