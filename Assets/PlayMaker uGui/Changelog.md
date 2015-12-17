#PlayMaker uGui Change log

###1.1.4
**new**
- Fixed `uGuiToggleSetIsOn` bad class name  
- Fixed api upgrades on 5.X on RectTransform custom actions


###1.1.3
**new**
- new action `RectTransformContainsScreenPoint` 
- new action `RectTransformScreenPointToWorldPointInRectangle`
- new action `RectTransformScreenPointToLocalPointInRectangle`
- Fixed obsolete properties in `GetLastPointerEventData` action

###1.1.2
**New**  
- new action `uGuiInputFieldOnSubmitEvent` 
- new action `uGuiInputFieldGetWasCanceled`
- new action `uGuiInputFieldGetIsFocused`
- improved `uGuiInputFieldOnEndEditEvent` with text access and wasCanceled property access
- improved `PlayMakerUGuiComponentProxy` for InputField EndEdit Event to pass wasCanceled in the EventData.
 
**Fix**  
- removed unecessary RequiredField attribute in action `uGuiInputFieldGetIsFocused` and `uGuiInputFieldGetHideMobileInput`  

 
###1.1.1
**New**  
- new get/set local position and rotation for RectTransform  
- new Drop proxy
- new sample Drag and Drop


**Fix**  
- Fixed `IsPointerOverUiObject` action
- Fixed Ecosystem drag example to use the new actions

**Improvement**  
- publishing alternative package with all ugui actions 
- renamed few actions to keep consistency  
- added sentByGameObject for Proxy events

###1.1.0

**Improvement**  
- Comply with Ecosystem versioning and changelog  
- better Git rep structure with PlayMaker Utils as SubModule  

**New**  
- Added EventSystem support for drag and raw pointer events  


###1.0.0

**New:**  
- Initial release  

