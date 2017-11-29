// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace Crayon
{

	/// <summary>
	/// Helper class that mirrors CrayonState and holds data in format that can be serialized and saved into presets.
	/// </summary>
	[System.Serializable]
	public class CrayonStateData
	{

		public CrayonStateType _crayonStateType;
		public string _customStateType = "";
		public string _crayonMatchKey;

		public Easing _easing = Easing.CubicInOut;
		public string _customEasing;
		public float _duration;

		public bool _tweenAppearance = true;
		public CrayonTweenAppearanceMode _tweenAppearanceMode = CrayonTweenAppearanceMode.Material;
		public Color _color;
		public Material _material;

		public bool _tweenPosition = true;
		public Vector3 _relativePosition;

		public bool _tweenRotation = true;
		public Vector3 _relativeRotation;

		public bool _tweenScale = true;
		public Vector3 _relativeScale = new Vector3(1.0f, 1.0f, 1.0f);

		/// <summary>
		/// Constructor: Create a new CrayonStateData instance from a CrayonState.
		/// </summary>
		public CrayonStateData (CrayonState stateToSave)
		{
			SaveData (stateToSave);
		}

		/// <summary>
		/// Saves data on transitions and properties from a CrayonState.
		/// </summary>
		public void SaveData(CrayonState state)
		{
			
			_crayonStateType = state._crayonStateType;
			_customStateType = state._customStateType;
			_crayonMatchKey = state._crayonMatchKey;

			_duration = state._duration;
			_easing = state._easing;
			_customEasing = state._customEasing;

			_tweenAppearance = state._tweenAppearance;
			_tweenAppearanceMode = state._tweenAppearanceMode;
			_material = state._material;
			_color = state._color;

			_tweenPosition = state._tweenPosition;
			_relativePosition = state._relativePosition;

			_tweenRotation = state._tweenRotation;
			_relativeRotation = state._relativeRotation;

			_tweenScale = state._tweenScale;
			_relativeScale = state._relativeScale;

		}

		/// <summary>
		/// Adds a CrayonState to the specified GameObject, then loads in data on
		/// transitions and properties from the CrayonStateData class.
		/// </summary>
		/// <param name="g">The green component.</param>
		public void LoadData(GameObject gameObject)
		{

			CrayonState state = gameObject.AddComponent<CrayonState> ();

			state._crayonStateType = _crayonStateType;
			state._customStateType = _customStateType;
			state._crayonMatchKey = _crayonMatchKey;

			state._duration = _duration;
			state._easing = _easing;
			state._customEasing = _customEasing;

			state._tweenAppearance = _tweenAppearance;
			state._tweenAppearanceMode = _tweenAppearanceMode;

			state._material = _material;

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