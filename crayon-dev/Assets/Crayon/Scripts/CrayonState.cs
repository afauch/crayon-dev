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
		public Color _color = Color.black;

		public bool _tweenMaterial = true;
		public Material _material;

		public bool _tweenPosition = true;
		public Vector3 _relativePosition;

		public bool _tweenRotation = true;
		public Vector3 _relativeRotation;

		public bool _tweenScale = true;
		public Vector3 _relativeScale = new Vector3(1.0f, 1.0f, 1.0f);

		private bool _defaultHasSet;		// have we assigned the correct default values

		// Constructors

		void OnValidate() {

			UpdateMatchKey ();

			if (_crayonStateType == CrayonStateType.Default) {
				SetDefaultValues ();
			} else {
				_defaultHasSet = false;
			}

		}

		public string UpdateMatchKey() {
			_crayonMatchKey = _crayonStateType.ToString ().ToLower ();
			if(_crayonStateType == CrayonStateType.Custom)
				_crayonMatchKey += _customStateType;
			return _crayonMatchKey;
		}

		void Update() {

			if (_material != null && this._crayonStateType == CrayonStateType.Default)
				Debug.Log (_material.GetColor ("_EmissionColor"));

		}


		public void SetDefaultValues() {

			if (!_defaultHasSet) {

				Renderer r = this.gameObject.GetComponent<Renderer> ();
				if (r != null) {
					_tweenMaterial = true;
					_tweenColor = true;					
					_material = Instantiate(r.sharedMaterial);
					_color = r.sharedMaterial.GetColor ("_Color");
				} else {
					_tweenMaterial = false;
					_tweenColor = false;
				}

				_tweenPosition = true;
				_relativePosition = Vector3.zero;

				_tweenRotation = true;
				_relativeRotation = Vector3.zero;

				_tweenScale = true;
				_relativeScale = new Vector3 (1.0f, 1.0f, 1.0f);

				_defaultHasSet = true;
				return;
			} else {
				return;
			}

		}

	}

}
