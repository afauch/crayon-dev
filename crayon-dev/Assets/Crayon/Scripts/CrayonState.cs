﻿using System.Collections;
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

	public enum CrayonTweenAppearanceMode {
		Material = 0,
		Color = 1,
		Opacity = 2
	}

	[RequireComponent(typeof(CrayonStateManager))]
	public class CrayonState : MonoBehaviour {
		
		public CrayonStateType _crayonStateType;
		public string _customStateType = "";
		public string _crayonMatchKey; // A match key for finding custom states

		public Easing _easing = Easing.CubicInOut;
		public string _customEasing;
		public float _duration;

		// Catch-all category for appearance
		public bool _tweenAppearance = false;
		public CrayonTweenAppearanceMode _tweenAppearanceMode = CrayonTweenAppearanceMode.Material;
		// public bool _tweenMaterial = true;
		public Material _material;
		// public bool _tweenColor = false;
		public Color _color = Color.black;

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

			// if (_material != null && this._crayonStateType == CrayonStateType.Default)
				// Debug.Log (_material.GetColor ("_EmissionColor"));

			// if (this._crayonStateType == CrayonStateType.Default)
				// Debug.Log ("TweenColor is" + _tweenColor + " on GameObject " + this.gameObject.name);
			

		}


		public void SetDefaultValues() {

			if (!_defaultHasSet) {

				Renderer r = this.gameObject.GetComponent<Renderer> ();
				if (r != null) {
					// _tweenMaterial = true;
					// _tweenColor = false; // just a default that can be overridden by the user		
					_material = r.sharedMaterial;
					_color = r.sharedMaterial.GetColor ("_Color");
				} else {
					_tweenAppearance = false;
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
