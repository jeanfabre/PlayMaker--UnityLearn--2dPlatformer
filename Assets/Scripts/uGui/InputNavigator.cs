// http://forum.unity3d.com/threads/tab-between-input-fields.263779/#post-2003757
// http://forum.unity3d.com/threads/tab-between-input-fields.263779/#post-2212450

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Input navigator. 
/// Drop this on the Canvas GameObject and pressing tab will move selected Input to the next one based on navigation setup.
/// </summary>
public class InputNavigator : MonoBehaviour
{
	EventSystem system;

	public KeyCode[] NavigationKeys = new KeyCode[] {KeyCode.Tab};

	bool _keyDown;
	void Start()
	{
		system = EventSystem.current;// EventSystemManager.currentSystem;
		
	}
	// Update is called once per frame
	void Update()
	{
		_keyDown = false;
		if (Input.anyKeyDown)
		{
			foreach(KeyCode _code in NavigationKeys)
			{
				if (Input.GetKeyDown(_code))
				{
					_keyDown = true;
					break;
				}

			}
		}

		if (_keyDown)
		{
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			
			if (next != null)
			{
				
				InputField inputfield = next.GetComponent<InputField>();
				if (inputfield != null)
					inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret
				
				system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
			}else
			{

			
				system.SetSelectedGameObject(system.firstSelectedGameObject, new BaseEventData(system));
			}
			//else Debug.Log("next nagivation element not found");
			
		}
	}
}