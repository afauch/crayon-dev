using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestFade : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.FadeIn (0.8f, Easing.Linear);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
