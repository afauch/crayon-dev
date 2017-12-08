// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using UnityEngine;

namespace Crayon
{

	/// <summary>
	/// Standard states for Crayon state transitions
	/// </summary>
	public enum CrayonStateType
	{
		Default,
		Hover,
		Selected,
		Visited,
		Custom
	}

	/// <summary>
	/// Used for toggling between Material, Color-Only, and Opacity-Only tweens
	/// </summary>
	public enum CrayonTweenAppearanceMode
	{
		Material = 0,
		Color = 1,
		Opacity = 2
	}

	/// <summary>
	/// Component that can be added to a GameObject for toggling states with Crayon.
	/// </summary>
	[RequireComponent(typeof(CrayonStateManager))]
	public class CrayonState : MonoBehaviour
	{
		
		public CrayonStateType _crayonStateType;
		public string _customStateType = "";	// If the state is a custom state, this is the user-assigned ID that refers to the state
		public string _crayonMatchKey;	// A match key for finding custom states

		public Easing _easing = Easing.CubicInOut;
		public string _customEasing;
		public float _duration = Crayon.Defaults._duration;

		public bool _tweenAppearance = false;	// Catch-all category for appearance
		public CrayonTweenAppearanceMode _tweenAppearanceMode = CrayonTweenAppearanceMode.Material;
		public Material _material;
		public Color _color = Color.black;
		public float _opacity = 1.0f;

		public bool _tweenPosition = true;
		public Vector3 _relativePosition;

		public bool _tweenRotation = true;
		public Vector3 _relativeRotation;

		public bool _tweenScale = true;
		public Vector3 _relativeScale = new Vector3(1.0f, 1.0f, 1.0f);

		private bool _defaultHasSet;	// If this is a Default state, have we assigned the correct values?

		/// <summary>
		/// On a Unity Editor event.
		/// </summary>
		void OnValidate()
		{
			UpdateMatchKey ();
			if (_crayonStateType == CrayonStateType.Default)
			{
				SetDefaultValues ();
			}
			else
			{
				_defaultHasSet = false;
			}
		}

		/// <summary>
		/// Update the unique match key for this state.
		/// </summary>
		/// <returns>The match key as a string.</returns>
		public string UpdateMatchKey()
		{
			_crayonMatchKey = _crayonStateType.ToString ().ToLower ();
			if(_crayonStateType == CrayonStateType.Custom)
				_crayonMatchKey += _customStateType;
			return _crayonMatchKey;
		}

		/// <summary>
		/// If this state is a Default state, lock in certain state values to prevent errors.
		/// </summary>
		public void SetDefaultValues()
		{

			if (!_defaultHasSet)
			{

				Renderer r = this.gameObject.GetComponent<Renderer> ();
				if (r != null)
				{	
					_material = r.sharedMaterial;

					// Error handling for unconventional materials.
					if(_material.HasProperty("_Color"))
					{
						_color = r.sharedMaterial.GetColor ("_Color");
					}
					else
					{
						Debug.LogWarningFormat ("{0} is using a special shader, unfortunately Crayon doesn't know how to tween it.", this.gameObject.name);
					}
				}
				else
				{
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

			}
			else
			{
				return;
			}

		}

	}

}
