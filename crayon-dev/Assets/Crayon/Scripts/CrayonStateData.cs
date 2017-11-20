using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {

	[System.Serializable]
	public class CrayonStateData {

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


		// Constructor from CrayonState
		public CrayonStateData (CrayonState stateToSave) {

			SaveData (stateToSave);

		}

		public void SaveData(CrayonState state) {

			_crayonStateType = state._crayonStateType;
			_customStateType = state._customStateType;
			_crayonMatchKey = state._crayonMatchKey;
			_material = state._material;
			_color = state._color;
			_easing = state._easing;
			_duration = state._duration;
			_relativePosition = state._relativePosition;
			_relativeRotation = state._relativeRotation;
			_relativeScale = state._relativeScale;

		}

		public void LoadData(GameObject g) {

			CrayonState state = g.AddComponent<CrayonState> ();

			state._crayonStateType = _crayonStateType;
			state._customStateType = _customStateType;
			state._crayonMatchKey = _crayonMatchKey;
			state._material = _material;
			state._color = _color;
			state._easing = _easing;
			state._duration = _duration;
			state._relativePosition = _relativePosition;
			state._relativeRotation = _relativeRotation;
			state._relativeScale = _relativeScale;

		}

	}
}