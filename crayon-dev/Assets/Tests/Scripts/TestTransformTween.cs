using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestTransformTween : MonoBehaviour {

	// Use this for initialization
	void Start () {

		float time = 3.0f;

		//gameObject.SetRelativePosition (new Vector3 (0.0f, 1.0f, 0.0f), time, Easing.QuarticInOut);
		//gameObject.SetRelativeRotation (new Vector3 (0.0f, 0.0f, 10.0f), time, Easing.CubicOut);
		gameObject.SetRelativeScale(1.2f, time, Easing.CubicOut);
		gameObject.SetColor("#FF0000");

	}
	
	// Update is called once per frame
	void Update () {



	}
}
