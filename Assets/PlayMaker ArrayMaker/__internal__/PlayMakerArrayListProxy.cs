//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop this script onto a GameObject.
//
// -- PLayMaker Events:
// PLayMaker Events can be dispatched when items are added, set or removed from this ArrayList. 
// you can enable/Disable this feature
// Fill each events with the relevent Events name. Make sure they are actually defined in PlayMaker AND used in Fsm as GlobalTransitions.
//
// -- PreFill: 
// You can define during authoring the content of this arrayList. Simply reveal the Prefill tab in the inspector. 
// select a type from the list
// Select the number of items
// fill each items with your Data.
// When the scene starts, the array will be filled with this data
//
// --ArrayList Content viewing;
// During PlayBack, you can view and edit the content of the ArrayList. 
// you can edit data on the fly
// You can preview only a portion of the content using a start index and the number of element to show ( maximum 30 elements at a time).
//
//
// -- Referencing Within PlayMaker
// Reference this PlayMakerArrayListProxy Component in "ArrayListObject" [and optionaly provide a unique name reference] in one of the related playmaker action:
//   -- OR -- 
// Reference this PlayMakerArrayListProxy Component in an FsmObject Variable with type set to "PLayMakerArrayListProxy". Then use this variable for reference within the related Actions:
// The second technics works well when PlayMakerArrayListProxy is defined only once on a given gameObject. 
// Name reference becomes uncessary and within Actions, leave the reference name field blank too.
// Else, you can maintain a FsmString that contains the reference of that PlayMakerArrayListProxy and use the pair ( FsmObject AND FsmString ) within Actions.
//
// Note: You can directly reference the GameObject or store it in an Fsm variable or global Fsm variable for better referencing

// Common actions for ArrayList
// ArrayListAdd, 
// ArrayListAddRange, 
// ArrayListClear, 
// ArrayListContains, 
// ArrayListCopyTo,    !!!!!!!! only copy to System.array. do we want that? should a PlayMakerArrayProxy be created for it?
// ArrayListCount,
// ArrayListEmpty,
// ArrayListGet,
// ArrayListGetRange,
// ArrayListIndexOf,  To test start index and count. Not sure I fully understand the expected results.
// ArrayListInsert, 
// ArrayListInsertRange,
// ArrayListLastIndexOf,  To test start index and count. Not sure I fully understand the expected results.
// ArrayListRemove, 
// ArrayListRemoveAt,
// ArrayListRemoveRange, 
// ArrayListReverse,
// ArrayListSet,
// ArrayListSetRange,
// ArrayListSort,
//
// Javascript array methods ports:
// 
// listed below the extra actions to provide js array functionnalities
// ArrayListConcat
// ArrayListJoin
// ArrayListPush // duplicated of ArrayListAdd
// ArrayListPop // can be achieved using ArrayListRemoveAt and set the index to the array count -1
// ArrayListShift // can be achieved using ArrayListRemoveAt and set the index to 0
// ArrayListUnShift // can be achieved using ArrayListAddAt and set the index to 0
// 
// Clear: The js array clear method is in conflict with the arrayList clear method. 
//        So for clarity, I will use ArrayListReset which will remove all elements of the array. The array count will be zero.
//
//
// To manage this component on the fly, use one of the following actions:
//
// FindArrayList 
// CreateArrayList 
// DestroyArrayList 
//
// Special Actions:
//
// SplitTexttoArrayList, Powerful action that split a text asset or string into an array. 
//
// If you need more actions, do not hesitate to contact the author



using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;



public class PlayMakerArrayListProxy : PlayMakerCollectionProxy {
		
	// the actual arrayList
	public ArrayList _arrayList;
	
	public ArrayList arrayList
    {
        get { return _arrayList; }
    }
	
	// the copy of the initial array or when ArrayListTakeSnapShot action is used.
	private ArrayList _snapShot;
	
	
	
	public void Awake()
	{	
		_arrayList = new ArrayList();
		PreFillArrayList();
		
		TakeSnapShot();
	}
	
	
	public bool isCollectionDefined()
	{
		return arrayList != null;
	}
	
	public void TakeSnapShot()
	{
		_snapShot = new ArrayList();
		_snapShot.AddRange(_arrayList);
	}
	
	public void RevertToSnapShot()
	{
		_arrayList = new ArrayList();
		_arrayList.AddRange(_snapShot);
	}
	
	// compose Add so that we can broadcast events
	public void Add(object value,string type,bool silent = false)
	{
		arrayList.Add(value);
		if (!silent)
		{
			dispatchEvent(addEvent,value,type);
		}
	}

	// compose AddRange so that we can broadcast events
	public int AddRange(ICollection collection,string type)
	{
		arrayList.AddRange(collection);
		// TOFIX: what should I do here...? specific event?
		//dispatchEvent(addEvent,value,type);
		
		return arrayList.Count;
	}	
	
	public void InspectorEdit(int index){
		dispatchEvent(setEvent,index,"int");
	}
	
	// compose Set so that we can broadcast events
	public void Set(int index,object value,string type)
	{
	//	Debug.Log("Set"+value);
		arrayList[index] = value;
		
		dispatchEvent(setEvent,index,"int");

	}
	
	// compose Remove so that we can broadcast events
	public bool Remove(object value,string type,bool silent = false)
	{
		if (arrayList.Contains(value)){
			arrayList.Remove(value);
			if (!silent)
			{
				dispatchEvent(removeEvent,value,type);
			}
			return true;
		}
		
		return false;
	}

	
	// TODO: compose and dispatch events for more ( insert, addRange, etc etc, maybe?)
	

	private void PreFillArrayList()
	{
		
		/*
		if (fromTextFile){
		
			string[] lines = fromTextFile.text.Split('\n');
			
			arrayList.InsertRange(0,lines);
			return;
		}
		*/
		
		switch (preFillType) {
			case (VariableEnum.Bool):
				arrayList.InsertRange(0,preFillBoolList);
				break;
			case (VariableEnum.Color):
				arrayList.InsertRange(0,preFillColorList);	
				break;
			case (VariableEnum.Float):
				arrayList.InsertRange(0,preFillFloatList);	
				break;
			case (VariableEnum.GameObject):
				arrayList.InsertRange(0,preFillGameObjectList);	
				break;
			case (VariableEnum.Int):
				arrayList.InsertRange(0,preFillIntList);	
				break;
			case (VariableEnum.Material):
				arrayList.InsertRange(0,preFillMaterialList);
				break;
			case (VariableEnum.Quaternion):
				arrayList.InsertRange(0,preFillQuaternionList);	
				break;
			case (VariableEnum.Rect):
				arrayList.InsertRange(0,preFillRectList);	
				break;
			case (VariableEnum.String):
				arrayList.InsertRange(0,preFillStringList);	
				break;
			case (VariableEnum.Texture):
				arrayList.InsertRange(0,preFillTextureList);	
				break;
			case (VariableEnum.Vector2):
				arrayList.InsertRange(0,preFillVector2List);		
				break;
			case (VariableEnum.Vector3):
				arrayList.InsertRange(0,preFillVector3List);		
				break;
			case (VariableEnum.AudioClip):
				arrayList.InsertRange(0,preFillAudioClipList);		
				break;
			default:
				break;
		}
	}
	
}
