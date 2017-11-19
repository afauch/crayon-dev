using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

/// <summary>
/// Class used to populate GameObjects with presets.
/// </summary>
public class CrayonStateData {

	public CrayonStateType _crayonStateType;
	public string _customStateType = "";
	public string _crayonMatchKey; // A match key for finding custom states
	public Material _material;
	public Color _color;
	public Easing _easing = Easing.CubicInOut;
	public float _duration;
	public Vector3 _relativePosition;
	public Vector3 _relativeRotation;
	public Vector3 _relativeScale = new Vector3(1.0f, 1.0f, 1.0f);

}
