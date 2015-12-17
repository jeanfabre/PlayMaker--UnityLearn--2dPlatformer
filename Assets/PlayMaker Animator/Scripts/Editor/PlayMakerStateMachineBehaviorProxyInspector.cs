using System;
using System.Collections;

using UnityEditor;
using UnityEditorInternal;

using UnityEngine;

using HutongGames.PlayMaker.Ecosystem.Utils;

#if UNITY_5
[CustomEditor(typeof(PlayMakerStateMachineBehaviorProxy))]
public class PlayMakerStateMachineBehaviorProxyInspector : UnityEditor.Editor {


	
	public override void OnInspectorGUI()
	{
		/*
		PlayMakerStateMachineBehaviorProxy _state = (PlayMakerStateMachineBehaviorProxy)this.target;


		if (_state.stateNames ==null )
		{
			updateLUT();
		}
	*/
		//_state.stateName  _state.name;

		DrawDefaultInspector();
		/*
		PlayMakerStateMachineBehaviorProxy _target = (PlayMakerStateMachineBehaviorProxy)this.target;

		EvenTargetInspectorGUI((Gameobject)target);

		PlayMakerEventTarget _eventTarget = _target.eventTarget;
*/
	}

	/*
	void updateLUT()
	{	
		PlayMakerStateMachineBehaviorProxy _state = (PlayMakerStateMachineBehaviorProxy)this.target;


		// Get a reference to the Animator Controller:
		UnityEditor.Animations.AnimatorController ac = _state..GetComponent<Animator>().runtimeAnimatorController as UnityEditor.Animations.AnimatorController;

		// Number of layers:
		int layerCount = ac.GetLayerCount();

		_state.stateNames = new stateNames[0];
		_state.stateHashes = new stateHashes[0];

		Debug.Log(string.Format("Layer Count: {0}", layerCount));
		
		// Names of each layer:
		for (int layer = 0; layer < layerCount; layer++) {

			string _layerName = ac.GetLayerName(layer);

			UnityEditor.Animations.AnimatorStateMachine sm = ac.GetLayerStateMachine(layer);

			foreach (UnityEditor.Animations.AnimatorState s in sm.statesRecursive) {
				ArrayUtility.Add(ref _state.stateNames, s.name);
				ArrayUtility.Add(ref _state.stateHashes,Animator.StringToHash(s.name));
				Debug.Log(string.Format("State: {0}", s.GetUniqueName()));
			}
		}
	}
  */

}
#endif
