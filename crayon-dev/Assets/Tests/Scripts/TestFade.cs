﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestFade : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {

			gameObject.FadeIn (1.0f);
		
		}
		
	}
}
