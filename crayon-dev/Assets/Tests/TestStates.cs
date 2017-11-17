using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestStates : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.GetComponent<CrayonStateManager> ().ChangeState (CrayonStateType.Hover);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
