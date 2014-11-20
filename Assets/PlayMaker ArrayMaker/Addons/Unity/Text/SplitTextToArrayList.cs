//	(c) Jean Fabre, 2011-2013 All rights reserved.
//	http://www.fabrejean.net

// INSTRUCTIONS
// Drop a PlayMakerArrayList script onto a GameObject, and define a unique name for reference if several PlayMakerArrayList coexists on that GameObject.
// In this Action interface, link that GameObject in "arrayListObject" and input the reference name if defined. 
// Note: You can directly reference that GameObject or store it in an Fsm variable or global Fsm variable
using System;

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Split a text asset or string into an arrayList")]
	public class SplitTextToArrayList : ArrayListActions
	{
		
		public enum ArrayMakerParseStringAs  {String,Int,Float};
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("From where to start parsing, leave to 0 to start from the beginning")]
		public FsmInt startIndex;
		
		[Tooltip("the range of parsing")]
		public FsmInt parseRange;
		
		[ActionSection("Source")]
		
		[Tooltip("Text asset source")]
		public TextAsset textAsset;
		
		[Tooltip("Text Asset is ignored if this is set.")]
		public FsmString OrThisString;
		
		public enum SplitSpecialChars
		{
			NewLine,
			Tab,
			Space,
		}
		
		[ActionSection("Split")]
		[Tooltip("Split")]
		public SplitSpecialChars split;
		
		[Tooltip("Split is ignored if this value is not empty. Each chars taken in account for split")]
		public FsmString OrThisChar;
		
		[ActionSection("Value")]
		[Tooltip("Parse the line as a specific type")]
		public ArrayMakerParseStringAs parseAsType;
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			startIndex = null;
			parseRange = null;
			textAsset = null;	
			split = SplitSpecialChars.NewLine;
			parseAsType = ArrayMakerParseStringAs.String;
			
		}

		
		public override void OnEnter()
		{
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				splitText();
			
			Finish();
		}

		
		public void splitText()
		{
			if (! isProxyValid()) 
				return;
			
			string _text;
			
			if (OrThisString.Value.Length==0){
				if (textAsset== null) 
				{
					return;
				}else{
					_text = textAsset.text;
				}
			}else{
				_text = OrThisString.Value;
			}
			
			
			
			proxy.arrayList.Clear();
			
			string[] rawlines;
			
			if (OrThisChar.Value.Length==0){
				char _split = '\n';
				
				switch(split){
				case SplitSpecialChars.Tab:
					_split = '\t';
					break;
				case SplitSpecialChars.Space:
					_split = ' ';
					break;
				}
	
	
				rawlines = _text.Split(_split);
				
			}else{
				rawlines = _text.Split(OrThisChar.Value.ToCharArray());
			}
			
			
			
			int start = startIndex.Value;
			
			int count = rawlines.Length;
			
			if (parseRange.Value>0)
			{
				count = Mathf.Min (count-start,parseRange.Value);
			}
			
			string[] lines = new string[count];
			
			int j = 0;
			
			for(int i=start;i<start+count;i++)
			{
				lines[j] = rawlines[i];
				j++;
			}
			
			if (parseAsType == ArrayMakerParseStringAs.String)
			{
					
				proxy.arrayList.InsertRange(0,lines);
				
			}else if (parseAsType == ArrayMakerParseStringAs.Int)
			{
				int[] ints = new int[lines.Length];
				int i = 0;
				
				foreach(String text in lines)
				{
				  int.TryParse(text, out ints[i]);
				  ++i;
				
				}
				proxy.arrayList.InsertRange(0,ints);
				
			}else if (parseAsType == ArrayMakerParseStringAs.Float)
			{
				float[] floats = new float[lines.Length];
				int i = 0;
				
				foreach(String text in lines)
				{
				  float.TryParse(text, out floats[i]);
				  ++i;
				
				}
				proxy.arrayList.InsertRange(0,floats);
			}
			
			
			
		
		}
	}
}