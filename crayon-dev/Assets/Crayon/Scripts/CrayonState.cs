using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Crayon {

	public enum CrayonStateType {
		Default,
		Hover,
		Selected,
		Visited,
		Custom
	}

	[RequireComponent(typeof(CrayonStateManager))]
	public class CrayonState : MonoBehaviour {
		
		public CrayonStateType _crayonStateType;
		public string _customStateType = "";
		public string _crayonMatchKey; // A match key for finding custom states
		public Material _material;
		public Color _color;
		public Easing _easing = Easing.CubicInOut;
		public float _duration;
		public Vector3 _relativePosition;
		public Vector3 _relativeRotation;
		public Vector3 _relativeScale = new Vector3(1.0f, 1.0f, 1.0f);

		// Constructors

		void OnValidate() {

			UpdateMatchKey ();

		}

		public string UpdateMatchKey() {
			_crayonMatchKey = _crayonStateType.ToString ().ToLower ();
			if(_crayonStateType == CrayonStateType.Custom)
				_crayonMatchKey += _customStateType;
			return _crayonMatchKey;
		}

	}

}
