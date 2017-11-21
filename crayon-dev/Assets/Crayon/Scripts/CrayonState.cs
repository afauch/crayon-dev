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

		public Easing _easing = Easing.CubicInOut;
		public string _customEasing;
		public float _duration;

		public bool _tweenColor = true;
		public Material _material;
		public Color _color;

		public bool _tweenPosition = true;
		public Vector3 _relativePosition;

		public bool _tweenRotation = true;
		public Vector3 _relativeRotation;

		public bool _tweenScale = true;
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
