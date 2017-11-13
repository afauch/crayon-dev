using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;

public class TestFadeInNew : MonoBehaviour {

	public GameObject _gameObject;
	public Transform _target;

	// Use this for initialization
	void Start () {

		_gameObject.FadeInNew (_target, 8.0f, Easing.Linear);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
