using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestColorTween : MonoBehaviour {

	// public Color _color = Color.white;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Alpha1)) {

			this.gameObject.SetColor ("#FF0000");

		}
		
	}
}
