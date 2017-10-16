// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.

using UnityEngine;
using System.Collections;

using UnityEngine.UI;

#if UNITY_EDITOR

using UnityEditor;

#endif

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class BoxCollider2dMatchRectTransform : MonoBehaviour {

	[SerializeField]
	bool _includeChildren;

	public bool IncludeChildren
	{
		get{
			return _includeChildren;
		}

		set{
			if (value != _includeChildren)
			{
				_includeChildren = value;
				MatchSize();
			}
		}
	}

	[SerializeField]
	Vector2 _margin;

	public Vector2 Margin
	{
		get{
			return _margin;
		}
		
		set{
			if (value != _margin)
			{
				_margin = value;
				MatchSize();
			}
		}
	}

	BoxCollider2D _bc2d;
	RectTransform _rt;

	Bounds bounds;

	Vector2 _size;
	Vector2 _lastMargin;



	bool _isDirty;

	// Use this for initialization
	void Start () {
		MatchSize();
	}

	void OnRectTransformDimensionsChange()
	{
		MatchSize();
	}

	void Update()
	{
		if (IncludeChildren)
		{

			MatchSize();

		}

	}



	public void MatchSize()
	{

		if (_bc2d==null)
		{
			_bc2d = this.GetComponent<BoxCollider2D>();
		}
		if (_rt==null)
		{
			_rt = this.GetComponent<RectTransform>();
		}

		if (_rt==null) return;

		if(_bc2d==null) return;
		

		if (_includeChildren)
		{
			bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(this.transform);
		
		}else{
			bounds = CalculateRelativeRectTransformBounds(this.transform,this.transform);
		}

		_size = bounds.size;

		_bc2d.size =  _size + _margin;
		_bc2d.offset = bounds.center;


	}

	private Vector3[] s_Corners = new Vector3[4];
	Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
	{
		RectTransform[] componentsInChildren = new RectTransform[] { child.GetComponent<RectTransform>()};


		Bounds result;
		if (componentsInChildren.Length > 0)
		{
			Vector3 vector = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
			Vector3 vector2 = new Vector3(-3.40282347E+38f, -3.40282347E+38f, -3.40282347E+38f);
			Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].GetWorldCorners(s_Corners);
				for (int j = 0; j < 4; j++)
				{
					Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(s_Corners[j]);
					vector = Vector3.Min(lhs, vector);
					vector2 = Vector3.Max(lhs, vector2);
				}
				i++;
			}
			Bounds bounds = new Bounds(vector, Vector3.zero);
			bounds.Encapsulate(vector2);
			result = bounds;
		}
		else
		{
			result = new Bounds(Vector3.zero, Vector3.zero);
		}
		return result;
	}



}

#if UNITY_EDITOR

[CustomEditor(typeof(BoxCollider2dMatchRectTransform))]
public class BoxCollider2dMatchRectTransformInspector : Editor 
{
	BoxCollider2dMatchRectTransform myTarget;
	
	public override void OnInspectorGUI()
	{
		myTarget = (BoxCollider2dMatchRectTransform)target;
		
		myTarget.IncludeChildren = EditorGUILayout.Toggle("Include Children",myTarget.IncludeChildren);
		if (myTarget.IncludeChildren )
		{
			EditorGUILayout.LabelField("Warning","Affects performances"); 
		}
		myTarget.Margin = EditorGUILayout.Vector2Field("Margin",myTarget.Margin);
	}
}
#endif

