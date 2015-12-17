// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"https://gist.githubusercontent.com/anonymous/93e343a83de42df03baa/raw/de3623271dd5d4a6aa204cae1575e10c1fff1cb5/RadialLayout.cs?assetFilePath=Assets/Scripts/JustAPixel/uGui/RadialLayout.cs"
					]
}
EcoMetaEnd
---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets various values of a UGui Radial Layout component.")]
	public class uGuiRadialLayoutSetProperties : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RadialLayout))]
		[Tooltip("The GameObject with the Radial Layout component.")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Values")]
		
		[Tooltip("The f distance.")]
		public FsmFloat fDistance;

		[Tooltip("The min or start angle in degrees")]
		[HasFloatSlider(0f,360f)]
		public FsmFloat minAngle;
		
		[Tooltip("The max or end angle in degrees")]
		[HasFloatSlider(0f,360f)]
		public FsmFloat maxAngle;
		
		[Tooltip("The start Angle in degrees")]
		[HasFloatSlider(0f,360f)]
		public FsmFloat startAngle;

		[Tooltip("Repeats every frame. Useful for animation")]
		public bool everyFrame;
		
		private RadialLayout _layoutElement;
		
		public override void Reset()
		{
			gameObject = null;
			fDistance = new FsmFloat(){UseVariable=true};
			minAngle = new FsmFloat(){UseVariable=true};
			maxAngle = new FsmFloat(){UseVariable=true};
			startAngle = new FsmFloat(){UseVariable=true};
			
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_layoutElement = _go.GetComponent<RadialLayout>();
			}
			
			
			DoSetValues();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetValues();
		}
		
		void DoSetValues()
		{
			
			if (_layoutElement!=null)
			{
				if (!fDistance.IsNone)
				{
					_layoutElement.fDistance = fDistance.Value;
				}
				
				if (!minAngle.IsNone)
				{
					_layoutElement.MinAngle = minAngle.Value;
				}
				
				if (!maxAngle.IsNone)
				{
					_layoutElement.MaxAngle = maxAngle.Value;
				}
				
				if (!startAngle.IsNone)
				{
					_layoutElement.StartAngle = startAngle.Value;
				}

				_layoutElement.CalculateLayoutInputVertical();
			}
		}
		
	}
}