using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestCubicBezier : MonoBehaviour {

	public int index;

	// Use this for initialization
	void Start () {

		if (index == 0) {

			gameObject.SetRelativePosition (new Vector3 (0.0f, 5.0f, 0.0f), 3.0f, Easing.CubicInOut);

		} else {

			gameObject.SetRelativePositionCB (new Vector3 (0.0f, 5.0f, 0.0f), 3.0f, Easing.Custom, "0.645,0.045,0.355,1");

		}
		
	}

}
