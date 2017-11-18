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

			gameObject.GetComponent<CrayonStateManager> ().ChangeState (CrayonStateType.Hover);

		}


	}
}
