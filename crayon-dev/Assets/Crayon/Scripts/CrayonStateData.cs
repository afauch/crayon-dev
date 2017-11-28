using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {

	[System.Serializable]
	public class CrayonStateData {

		public CrayonStateType _crayonStateType;
		public string _customStateType = "";
		public string _crayonMatchKey; // A match key for finding custom states

		public Easing _easing = Easing.CubicInOut;
		public string _customEasing;
		public float _duration;

		public bool _tweenColor = true;
		public Color _color;

		public bool _tweenMaterial = true;
		public Material _material;

		public bool _tweenPosition = true;
		public Vector3 _relativePosition;

		public bool _tweenRotation = true;
		public Vector3 _relativeRotation;

		public bool _tweenScale = true;
		public Vector3 _relativeScale = new Vector3(1.0f, 1.0f, 1.0f);


		// Constructor from CrayonState
		public CrayonStateData (CrayonState stateToSave) {

			SaveData (stateToSave);

		}

		public void SaveData(CrayonState state) {

			_crayonStateType = state._crayonStateType;
			_customStateType = state._customStateType;
			_crayonMatchKey = state._crayonMatchKey;

			_duration = state._duration;
			_easing = state._easing;
			_customEasing = state._customEasing;

			_tweenMaterial = state._tweenMaterial;
			_material = state._material;

			_tweenColor = state._tweenColor;
			_color = state._color;

			_tweenPosition = state._tweenPosition;
			_relativePosition = state._relativePosition;

			_tweenRotation = state._tweenRotation;
			_relativeRotation = state._relativeRotation;

			_tweenScale = state._tweenScale;
			_relativeScale = state._relativeScale;

		}

		public void LoadData(GameObject g) {

			CrayonState state = g.AddComponent<CrayonState> ();

			state._crayonStateType = _crayonStateType;
			state._customStateType = _customStateType;
			state._crayonMatchKey = _crayonMatchKey;

			state._duration = _duration;
			state._easing = _easing;
			state._customEasing = _customEasing;

			state._tweenMaterial = _tweenMaterial;
			state._material = _material;

			state._tweenColor = _tweenColor;
			state._color = _color;

			state._tweenPosition = _tweenPosition;
			state._relativePosition = _relativePosition;

			state._tweenRotation = _tweenRotation;
			state._relativeRotation = _relativeRotation;

			state._tweenScale = _tweenScale;
			state._relativeScale = _relativeScale;

		}

	}
}