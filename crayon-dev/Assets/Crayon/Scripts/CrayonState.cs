using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Crayon {

	[System.Serializable]
	public enum CrayonStateType {
		Default,
		Hover,
		Selected,
		Visited,
		Custom
	}

	[System.Serializable]
	public class CrayonState {
		
		public CrayonStateType _crayonStateType;
		public string _customStateType = "";
		public string _crayonMatchKey; // A match key for finding custom states
		public Material _material;
		public Color _color;
		public Easing _easing;
		public float _duration;
		public Vector3 _relativePosition;
		public Vector3 _relativeRotation;
		public Vector3 _relativeScale = new Vector3(1.0f, 1.0f, 1.0f);

		// Constructors
		public CrayonState() {
			// Create the match key on create
			UpdateMatchKey ();
		}

		public CrayonState(CrayonStateType stateType, string customStateType = "") {
			_crayonStateType = stateType;
			_customStateType = customStateType;
			UpdateMatchKey ();
		}

		public CrayonStateType SetType(CrayonStateType stateType, string customStateType = "") {

			_crayonStateType = stateType;
			_customStateType = customStateType;

			UpdateMatchKey ();

			return stateType;

		}

		public string UpdateMatchKey() {
			_crayonMatchKey = _crayonStateType.ToString ().ToLower() + _customStateType;
			return _crayonMatchKey;
		}

	}

}
