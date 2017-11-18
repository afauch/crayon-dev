using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestStates : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Space)) {

			// TODO: Change CrayonStateType to a shorter, better term like State.Hover
			gameObject.SetState (CrayonStateType.Custom,"A");

		}

	}
}
