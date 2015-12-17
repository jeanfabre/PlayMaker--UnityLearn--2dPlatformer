// http://forum.unity3d.com/threads/tab-between-input-fields.263779/#post-2003757
// http://forum.unity3d.com/threads/tab-between-input-fields.263779/#post-2212450

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputNavigator : MonoBehaviour
{
	EventSystem system;
	
	void Start()
	{
		system = EventSystem.current;// EventSystemManager.currentSystem;
		
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
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