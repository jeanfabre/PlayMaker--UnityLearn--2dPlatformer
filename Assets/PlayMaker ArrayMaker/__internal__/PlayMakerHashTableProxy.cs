//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop this script onto a GameObject.
//
// -- PLayMaker Events:
// PLayMaker Events can be dispatched when items are added, set or removed from this HashTable. 
// you can enable/Disable this feature
// Fill each events with the relevent Events name. Make sure they are actually defined in PlayMaker AND used in Fsm as GlobalTransitions.
//
// -- PreFill: 
// You can define during authoring the content of this HashTable. Simply reveal the Prefill tab in the inspector. 
// select a type from the list
// Select the number of items
// fill each items with your key and Data.
// When the scene starts, the array will be filled with this data
//
// LIMITATIONS: Keys are for now only strings. I might add a more flexible way to defines other types for keys 
//
// --HashTable Content viewing;
// During PlayBack, you can view and edit the content of the HashTable. 
// you can edit data on the fly
// You can preview only a portion of the content using a start index and the number of element to show ( maximum 30 elements at a time).
//
//
// -- Referencing Within PlayMaker
// Reference this PlayMakerHashTableProxy Component in "hashTableObject" [and optionaly provide a unique name reference] in one of the related playmaker action:
//   -- OR -- 
// Reference this PlayMakerHashTableProxy Component in an FsmObject Variable with type set to "PLayMakerHashTableProxy". Then use this variable for reference within the related Actions:
// The second technics works well when PlayMakerHashTableProxy is defined only once on a given gameObject. 
// Name reference becomes uncessary and within Actions, leave the reference name field blank too.
// Else, you can maintain a FsmString that contains the reference of that PlayMakerHashTableProxy and use the pair ( FsmObject AND FsmString ) within Actions.
//
// Common actions for HashTable
// HashTableAdd,
// HashTableClear,
// HashTableContains,
// HashTableContainsKey,
// HashTableContainsValue,
// HashTableCopyTo,
// HashTableCount,
// HashTableGet,
// HashTableKeys,
// HashTableSet,
// HashTableValues,
// If you need more actions, do not hesitate to contact the author
// note: You can directly reference the GameObject or store it in an Fsm variable or global Fsm variable for better referencing
//
//
// To manage this component on the fly, use one of the following actions:
//
// FindHashTable
// CreateHashTable
// DestroyHashTable
//
// What is the reference Name for?
//   Several PlayMakerHashTable can coexists on a given GameObject:
//   If only one PlayMakerHashTable is added to a given GameObject, the reference name is not necessary ( tho recommanded for easier evolution of your project).
//   If several PlayMakerHashTable coexists on a given GameObject, then it becomes necessary to properly provide unique references for stable and predictable behavior.



using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;


public class PlayMakerHashTableProxy : PlayMakerCollectionProxy {
	
	// the actual hashTable
	public Hashtable _hashTable;
	
	public Hashtable hashTable
    {
        get { return _hashTable; }
    }
	
	private Hashtable _snapShot;
	
	public void Awake()
	{	
		_hashTable = new Hashtable();
		
		PreFillHashTable();
		
		TakeSnapShot();
	}
	
	public bool isCollectionDefined()
	{
		return hashTable != null;
	}
	
	public void TakeSnapShot()
	{
		_snapShot = new Hashtable();
		foreach(object key in _hashTable.Keys){		
			_snapShot[key] = _hashTable[key];
		}
	}
	
	public void RevertToSnapShot()
	{
		_hashTable = new Hashtable();
		foreach(object key in _snapShot.Keys){		
			_hashTable[key] = _snapShot[key];
		}
	}
	
	
	public void InspectorEdit(int index){
		dispatchEvent(setEvent,index,"int");
	}
	
	
	private void PreFillHashTable()
	{
		for(int i=0;i<preFillKeyList.Count;i++)
		{
			switch (preFillType) {
				case (VariableEnum.Bool):
					hashTable[preFillKeyList[i]] = preFillBoolList[i];
					break;
				case (VariableEnum.Color):
					hashTable[preFillKeyList[i]] = preFillColorList[i];	
					break;
				case (VariableEnum.Float):
					hashTable[preFillKeyList[i]] = preFillFloatList[i];
					break;
				case (VariableEnum.GameObject):
					hashTable[preFillKeyList[i]] = preFillGameObjectList[i];
					break;
				case (VariableEnum.Int):
					hashTable[preFillKeyList[i]] = preFillIntList[i];	
					break;
				case (VariableEnum.Material):
					hashTable[preFillKeyList[i]] = preFillMaterialList[i];;
					break;
				case (VariableEnum.Quaternion):
					hashTable[preFillKeyList[i]] = preFillQuaternionList[i];
					break;
				case (VariableEnum.Rect):
					hashTable[preFillKeyList[i]] = preFillRectList[i];	
					break;
				case (VariableEnum.String):
					hashTable[preFillKeyList[i]] = preFillStringList[i];	
					break;
				case (VariableEnum.Texture):
					hashTable[preFillKeyList[i]] = preFillTextureList[i];;	
					break;
				case (VariableEnum.Vector2):
					hashTable[preFillKeyList[i]] = preFillVector2List[i];		
					break;
				case (VariableEnum.Vector3):
					hashTable[preFillKeyList[i]] = preFillVector3List[i];		
					break;
				case (VariableEnum.AudioClip):
					hashTable[preFillKeyList[i]] = preFillAudioClipList[i];		
					break;
				default:
					break;
			}
		}
	}
	
}
