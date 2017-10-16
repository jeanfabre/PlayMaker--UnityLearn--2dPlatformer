// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1)
#else
#define UNITY_5_2_OR_NEWER
#endif

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

using System.Collections;

[CustomEditor(typeof(PlayMakerUGuiComponentProxy))]
public class PlayMakerUGuiComponentProxyInspector : Editor {


	private PlayMakerUGuiComponentProxy _target;

	bool UiTargetIsValid;

	public override void OnInspectorGUI()
	{

		_target = (PlayMakerUGuiComponentProxy)target;

		// let the user select the target for the ui component
		_target.UiTargetOption = (OwnerDefaultOption)EditorGUILayout.EnumPopup("Target",_target.UiTargetOption);

		if (_target.UiTargetOption == OwnerDefaultOption.SpecifyGameObject)
		{
			EditorGUI.indentLevel++;
			_target.UiTarget = (GameObject)EditorGUILayout.ObjectField("Ui Target",_target.UiTarget,typeof(GameObject),true);
			EditorGUI.indentLevel--;
		}else{
			_target.UiTarget = _target.gameObject;
		}

		ContextGUI();
	
		if (UiTargetIsValid)
		{
			EditorGUI.indentLevel++;
			if (_target.action== PlayMakerUGuiComponentProxy.ActionType.SendFsmEvent)
			{
				SendFsmEventGUI();
			}else{
				SetFsmVariableGUI();
			}
			EditorGUI.indentLevel--;
			_target.debug = EditorGUILayout.Toggle("Debug",_target.debug);
		}

	}

	string[] _variableChoices;
	
	string[] _fsmChoices;


	void SetFsmVariableGUI()
	{
		bool _reset = false;
		
		// let the user select the target for variable
		PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget _newTarget = (PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget)EditorGUILayout.EnumPopup("Target",_target.fsmVariableSetup.target);
		
		// check if we changed target
		if (_newTarget != _target.fsmVariableSetup.target)
		{
			_reset = true;
			
			// reset fsm variable setup data
			_target.fsmVariableSetup.target = _newTarget;
			_target.fsmVariableSetup.gameObject = null;
			_target.fsmVariableSetup.variableName = null;
			_target.fsmVariableSetup.fsmComponent = null;
			_target.fsmVariableSetup.fsmIndex = 0;
			_target.fsmVariableSetup.variableIndex = 0;
			
		}
		
		// dispatch UI based on target type
		
		// GLOBAL VARIABLE
		if (_target.fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.GlobalVariable)
		{
			SelectFsmVariableGUI( FsmVariables.GlobalVariables);
		}
		
		// FSMCOMPONENT
		else if (_target.fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.FsmComponent)
		{
			
			_target.fsmVariableSetup.fsmComponent =  (PlayMakerFSM)EditorGUILayout.ObjectField(_target.fsmVariableSetup.fsmComponent,typeof(PlayMakerFSM),true);
			if (_target.fsmVariableSetup.fsmComponent!=null)
			{
				SelectFsmVariableGUI(_target.fsmVariableSetup.fsmComponent.FsmVariables);
			}else{
				ErrorFeedbackGui("Select a Target");
			}
		}
		
		// GAMEOBJECT
		else if (_target.fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.GameObject)
		{
			if (_reset)
			{
				_target.fsmVariableSetup.gameObject = null;;
				_target.fsmVariableSetup.fsmComponent = null;
			}
			
			// Let the user choose a GameObject
			GameObject _newGameObject = (GameObject)EditorGUILayout.ObjectField(" ",_target.fsmVariableSetup.gameObject,typeof(GameObject),true);
			
			if (_newGameObject!=_target.fsmVariableSetup.gameObject)
			{
				_target.fsmVariableSetup.gameObject = _newGameObject;
				_target.fsmVariableSetup.fsmComponent = null;
			}
			
			// if the user chose a GameObject then proceed with fsm and variable ui
			if (_target.fsmVariableSetup.gameObject!=null)
			{
				SelectFsmFromGameObject(_target.fsmVariableSetup.gameObject);
				
				if (_target.fsmVariableSetup.fsmComponent!=null)
				{
					//GUILayout.Label(_target.fsmVariableSetup.gameObject.name+" <-> "+_target.fsmVariableSetup.fsmComponent.gameObject.name);
					SelectFsmVariableGUI(_target.fsmVariableSetup.fsmComponent.FsmVariables);
				}
			}else{
				ErrorFeedbackGui("Select a Target");
			}
			
		}
		
		// OWNER
		else if (_target.fsmVariableSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyVariableTarget.Owner)
		{
			if (_reset)
			{
				_target.fsmVariableSetup.fsmComponent = null;
			}
			_target.fsmVariableSetup.gameObject = _target.gameObject;
			
			SelectFsmFromGameObject(_target.gameObject);
			if (_target.fsmVariableSetup.fsmComponent!=null)
			{
				SelectFsmVariableGUI(_target.fsmVariableSetup.fsmComponent.FsmVariables);
			}else{
				ErrorFeedbackGui("No fsm on Owner");
			}
		}
	}


	void SendFsmEventGUI()
	{
		
		bool isTargetSetup = EventTargetSetupGUI();

		if (!isTargetSetup)
		{
			ErrorFeedbackGui("No target defined");
			return;
		}
		bool isCustomEvent = ! string.IsNullOrEmpty(_target.fsmEventSetup.customEventName);

	
		bool isimplemented = _target.DoesTargetImplementsEvent();



		GUILayout.BeginHorizontal();
			
			if (!isCustomEvent)
			{
				//EditorGUILayout.LabelField("Event",GUILayout.ExpandWidth(false));
			EditorGUILayout.LabelField("Event (built in)",_target.fsmEventSetup.builtInEventName,"WordWrapLabel");

				if (GUILayout.Button("Edit",GUILayout.Width(40),GUILayout.Height(15)))
				{
					_target.fsmEventSetup.customEventName = _target.fsmEventSetup.builtInEventName;
				}
			}else{
			_target.fsmEventSetup.customEventName = EditorGUILayout.TextField("Event (Custom)",_target.fsmEventSetup.customEventName,GUILayout.ExpandWidth(true));
				if (GUILayout.Button("X",GUILayout.Width(21),GUILayout.Height(15)))
				{
					_target.fsmEventSetup.customEventName = "";
				}
			}
		GUILayout.EndHorizontal();

		bool _showSendToChildren = _target.fsmEventSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget.GameObject ||
			_target.fsmEventSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget.Owner
			;

		if (_showSendToChildren)
		{
			_target.fsmEventSetup.sendtoChildren = EditorGUILayout.Toggle("Send to Children",_target.fsmEventSetup.sendtoChildren);
		}

		if (!isimplemented)
		{
			if (_target.fsmEventSetup.target == PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget.BroadCastAll)
			{
				ErrorFeedbackGui("There is no such global Event");
			}else{
				ErrorFeedbackGui("This Event is not implemented on target");
			}

		}


		
	}
	
	bool EventTargetSetupGUI()
	{
		_target.fsmEventSetup.target = (PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget)EditorGUILayout.EnumPopup("Target",_target.fsmEventSetup.target);
		
		if (_target.fsmEventSetup.target  == PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget.FsmComponent)
		{
			_target.fsmEventSetup.fsmComponent =  (PlayMakerFSM)EditorGUILayout.ObjectField(_target.fsmEventSetup.fsmComponent,typeof(PlayMakerFSM),true);

			return _target.fsmEventSetup.fsmComponent != null;
		}else if (_target.fsmEventSetup.target  == PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget.GameObject)
		{
			_target.fsmEventSetup.gameObject = (GameObject)EditorGUILayout.ObjectField(_target.fsmEventSetup.gameObject,typeof(GameObject),true);
		
			return _target.fsmEventSetup.gameObject != null;
		}else if (_target.fsmEventSetup.target  == PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget.Owner)
		{
			return true;
		}else if (_target.fsmEventSetup.target  == PlayMakerUGuiComponentProxy.PlayMakerProxyEventTarget.BroadCastAll)
		{
			return true;
		}

		return false;
	}
	
	void ContextGUI()
	{
		EditorGUI.indentLevel++;
		bool canSetValue = false;
		UiTargetIsValid = false;
		string uiComponentType = "";
		string uiCallBack = "";

		if (_target.UiTarget == null)
		{
			return;
		}

		if (_target.UiTarget.GetComponent<Button>()!=null)
		{
			_target.fsmEventSetup.builtInEventName = "UGUI / ON CLICK";
			uiComponentType = "Button";
			uiCallBack = "On Click";
			UiTargetIsValid = true;
		}
		if (_target.UiTarget.GetComponent<Toggle>()!=null)
		{
			canSetValue = true;
			_target.fsmVariableSetup.variableType = VariableType.Bool;
			
			_target.fsmEventSetup.builtInEventName = "UGUI / ON BOOL VALUE CHANGED";

			uiComponentType ="Toggle";
			uiCallBack = "On Value Changed (Bool)";
			UiTargetIsValid = true;
			
		}
		if (_target.UiTarget.GetComponent<Slider>()!=null)
		{
			canSetValue = true;
			_target.fsmVariableSetup.variableType = VariableType.Float;
			_target.fsmEventSetup.builtInEventName = "UGUI / ON FLOAT VALUE CHANGED";
	
			uiComponentType ="Slider";
			uiCallBack = "On Value Changed (float)";
			UiTargetIsValid = true;
			
		}
		if (_target.UiTarget.GetComponent<Scrollbar>()!=null)
		{
			canSetValue = true;
			_target.fsmVariableSetup.variableType = VariableType.Float;
			_target.fsmEventSetup.builtInEventName = "UGUI / ON FLOAT VALUE CHANGED";

			uiComponentType ="Scrollbar";
			uiCallBack = "On Value Changed (float)";
			UiTargetIsValid = true;
		}

		if (_target.UiTarget.GetComponent<ScrollRect>()!=null)
		{
			canSetValue = true;
			_target.fsmVariableSetup.variableType = VariableType.Vector2;
			_target.fsmEventSetup.builtInEventName = "UGUI / ON VECTOR2 VALUE CHANGED";
			
			uiComponentType ="ScrollRect";
			uiCallBack = "On Value Changed (vector2)";
			UiTargetIsValid = true;
		}

		if (_target.UiTarget.GetComponent<InputField>()!=null)
		{
			canSetValue = true;
			_target.fsmVariableSetup.variableType = VariableType.String;

			_target.fsmEventSetup.builtInEventName = "UGUI / ON END EDIT";
			EditorGUILayout.LabelField("OnEndEdit for InputField");
			uiComponentType ="InputField";
			uiCallBack = "On End Edit";
			UiTargetIsValid = true;
		}

		#if UNITY_5_2_OR_NEWER
		
			if (_target.UiTarget.GetComponent<Dropdown>()!=null)
			{
				canSetValue = true;
				_target.fsmVariableSetup.variableType = VariableType.Int;
				_target.fsmEventSetup.builtInEventName = "UGUI / ON INT VALUE CHANGED";
				
				uiComponentType ="Dropdown";
				uiCallBack = "On Value Changed (int)";
				UiTargetIsValid = true;
			}
		
		#endif

		if (UiTargetIsValid)
		{
			EditorGUILayout.LabelField("UI Component", uiComponentType);
			EditorGUILayout.LabelField("UI Callback", uiCallBack);
		}else{
			ErrorFeedbackGui("UI Target is not a valid UI Component");
		}

		EditorGUI.indentLevel--;
		if (canSetValue)
		{
			_target.action = (PlayMakerUGuiComponentProxy.ActionType)EditorGUILayout.EnumPopup("Action: ",_target.action);
			
		}else if (_target.action == PlayMakerUGuiComponentProxy.ActionType.SetFsmVariable){
			_target.action = PlayMakerUGuiComponentProxy.ActionType.SendFsmEvent;
		}
	}

	#region Sub ui elements
	
	void ErrorFeedbackGui(string error)
	{
		GUILayout.Space(5);
		GUILayout.Label(error,"flow node 5",GUILayout.ExpandWidth(true), GUILayout.Height(24));	
		GUILayout.Space(5);
	}

	void SelectFsmFromGameObject(GameObject go)
	{

		PlayMakerFSM[] _list = go.GetComponents<PlayMakerFSM>();
		_fsmChoices = new string[_list.Length];
		for(int i=0;i<_list.Length;i++)
		{
			_fsmChoices[i] = _list[i].FsmName;
		} 


		if (_fsmChoices.Length==0)
		{
			ErrorFeedbackGui("No Fsm on target");
		}else{

			int _choiceIndex =  EditorGUILayout.Popup("Fsm",_target.fsmVariableSetup.fsmIndex,_fsmChoices);
			if ( _choiceIndex!=_target.fsmVariableSetup.fsmIndex || _target.fsmVariableSetup.fsmComponent==null )
			{
				//GUILayout.Label("finding "+go.name+"/"+_fsmList[__fsmListChoiceIndex]+":" + PlayMakerUtils.FindFsmOnGameObject(go,_fsmList[__fsmListChoiceIndex]));
				_target.fsmVariableSetup.fsmIndex = _choiceIndex;

				PlayMakerFSM _fsm = PlayMakerUtils.FindFsmOnGameObject(go,_fsmChoices[_choiceIndex]);

				if (_fsm==null)
				{
					Debug.LogError("Could not find fsm "+_fsmChoices[_choiceIndex]+" on go:"+go.name);
					_target.fsmVariableSetup.fsmComponent = null;
				}else{
				//	Debug.LogError("found fsm "+_fsmList[_choiceIndex]+" on go:"+go.name+" "+_fsm.FsmName);
					_target.fsmVariableSetup.fsmComponent = _fsm;
				}
			}
		}
		
	}

	void SelectFsmVariableGUI(FsmVariables fsmVariables)
	{
		NamedVariable[] _list = new NamedVariable[0];
		
		if (_target.fsmVariableSetup.variableType == VariableType.Bool)
		{
			_list = fsmVariables.GetNames(typeof(FsmBool));
		}else if (_target.fsmVariableSetup.variableType == VariableType.Float)
		{
			_list = fsmVariables.GetNames(typeof(FsmFloat));
		}else if (_target.fsmVariableSetup.variableType == VariableType.Vector2)
		{
			_list = fsmVariables.GetNames(typeof(FsmVector2));
		}else if (_target.fsmVariableSetup.variableType == VariableType.String)
		{
			_list = fsmVariables.GetNames(typeof(FsmString));
		}


		_variableChoices = new string[_list.Length];
		for(int i=0;i<_list.Length;i++)
		{
			_variableChoices[i] = _list[i].Name;
		}

		if (_variableChoices.Length==0)
		{
			ErrorFeedbackGui("No "+_target.fsmVariableSetup.variableType+" variable on Fsm");
		}else{
			int _choiceIndex =  EditorGUILayout.Popup(_target.fsmVariableSetup.variableType.ToString()+" Variable",_target.fsmVariableSetup.variableIndex,_variableChoices);
			if (_choiceIndex!=_target.fsmVariableSetup.variableIndex || string.IsNullOrEmpty(_target.fsmVariableSetup.variableName))
			{
				_target.fsmVariableSetup.variableIndex =_choiceIndex;
				_target.fsmVariableSetup.variableName = _variableChoices[_choiceIndex];
			}
		}

	}

	#endregion


}
