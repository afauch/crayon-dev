﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestTransformTween : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// gameObject.SetRelativePosition (new Vector3 (4.0f, 0.0f, 0.0f), 1.0f, Easing.QuarticInOut);
		gameObject.SetRelativeRotation (new Vector3 (180.0f, 30.0f, 60.0f), 2.0f, Easing.CubicOut);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
