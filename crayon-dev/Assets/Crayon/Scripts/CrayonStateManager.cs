// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Crayon;

namespace Crayon
{

	/// <summary>
	/// This class manages Crayon states on a single GameObject. It can be used to load and save presets as well.
	/// </summary>
	public class CrayonStateManager : MonoBehaviour
	{

		public bool _listenToParent;	// Should this StateManager listen to the StateManager above it for events?
		public string _newPresetId = "<Enter A Preset Name>";
		[HideInInspector] public string _presetToLoad;

		public Dictionary<string,CrayonState> _allStatesByMatchKey;	// Dictionary of all the CrayonStates managed by this instance

		public delegate void ChangeStateDelegate (CrayonStateType stateType, string customState = "");
		public ChangeStateDelegate OnChangeState;
		public delegate void FreezeTransformDelegate ();
		public FreezeTransformDelegate OnFreezeTransform;

		private string _currentMatchKey = null;
		private CrayonState _currentState;
		private Vector3 _originalPosition;
		private Quaternion _originalRotation;
		private Vector3 _originalScale;

		void Awake()
		{

			InitDictionary ();
			if (_listenToParent)
			{
				SubscribeToParent (true);
			}

			CrayonStateGlobals.Instance._isTweening[this] = false;
			FreezeTransform ();
		}

		void Update() {

			// Keep tabs on our 'base' transform as long as we're not tweening
			if (!CrayonStateGlobals.Instance._isTweening [this])
				FreezeTransform ();

		}

		/// <summary>
		/// This is necessary for any states that use Relative Position, Rotation, or Scale tweens.
		/// If the GameObject changes position for any reason except a tween, we should make the new position the base of our
		/// Relative Position, Rotation, or Scale tweens.
		/// </summary>
		public void FreezeTransform ()
		{

			if (gameObject.transform.localPosition != _originalPosition ||
			   gameObject.transform.localRotation != _originalRotation ||
			   gameObject.transform.localScale != _originalScale)
			{
				_originalPosition = gameObject.transform.localPosition - _currentState._relativePosition;
				_originalRotation = Quaternion.Euler(gameObject.transform.localRotation.eulerAngles - _currentState._relativeRotation);
				_originalScale = new Vector3((gameObject.transform.localScale.x/_currentState._relativeScale.x), (gameObject.transform.localScale.y/_currentState._relativeScale.y), (gameObject.transform.localScale.z/_currentState._relativeScale.z));
			}

			if (OnFreezeTransform != null)
			{
				OnFreezeTransform ();
			}

		}

		/// <summary>
		/// This initializes the dictionary used by the State Manager to recall and access specific states.
		/// </summary>
		public void InitDictionary () {
			
			_allStatesByMatchKey = new Dictionary<string, CrayonState> ();
			CrayonState[] states = GetComponents<CrayonState> ();
			foreach(CrayonState state in states)
			{
				CrayonState check;
				if (!_allStatesByMatchKey.TryGetValue (state._crayonMatchKey, out check))
				{
					_allStatesByMatchKey.Add (state._crayonMatchKey, state);
				}
				else
				{
					Debug.LogWarningFormat ("There are multiple states of the same type on {0}. Please check your states.", this.gameObject.name);
				}
			}
			_currentState = _allStatesByMatchKey["default"];
		}

		/// <summary>.
		/// Look to parent GameObject's CrayonStateManager instance, and subscribe to their ChangeState event
		/// </summary>
		public void SubscribeToParent (bool subscribe) {
			CrayonStateManager[] parent = gameObject.GetComponentsInParent<CrayonStateManager> ();
			try {
				if (subscribe) // If true, subscribe
				{ 
					parent [1].OnChangeState += ChangeState;
					parent [1].OnFreezeTransform += FreezeTransform;
				}
				else // Otherwise, unsubscribe
				{ 
					parent [1].OnChangeState -= ChangeState;
					parent [1].OnFreezeTransform -= FreezeTransform;
				}
			}
			catch
			{
				Debug.LogWarning ("No parent to subscribe to.");
			}
		}

		/// <summary>
		/// Used to load a preset collection of CrayonStates from Crayon > UserPresets.
		/// This can be useful if you're trying to apply similar behaviors to several GameObjects.
		/// </summary>
		public void LoadPreset()
		{
 			CrayonStateGlobals.Instance.LoadPreset (this.gameObject, _presetToLoad);
		}

		/// <summary>
		/// Used to save a collection of CrayonStates as a preset.
		/// </summary>
		public void SavePreset()
		{
			CrayonStateGlobals.Instance.SavePreset (this.gameObject, _newPresetId, this);
		}

		/// <summary>
		/// Function to add a CrayonState to the GameObject.
		/// </summary>
		public void AddState()
		{
			this.gameObject.AddComponent<CrayonState> ();
		}

		/// <summary>
		/// Begins a new transition between states.
		/// </summary>
		public void ChangeState(CrayonStateType stateType, string customState = "")
		{
			// Call the delegate so any downstream items know to change as well.
			if (OnChangeState != null) {
				OnChangeState (stateType, customState);
			}

			// Find the state by match key
			string matchKey = stateType.ToString().ToLower() + customState;

			// Don't do anything if we're already on the right state
			if (matchKey == _currentMatchKey)
			{
				return;
			}

			CrayonState state;
			_allStatesByMatchKey.TryGetValue (matchKey, out state);

			if (state != null)
			{
				if (state._tweenAppearance)
				{
					// Determine the tween method based on the enum selected
					switch (state._tweenAppearanceMode) {
					case CrayonTweenAppearanceMode.Material:
						gameObject.SetMaterial (state._material, state._duration, state._easing, state._customEasing);
						break;
					case CrayonTweenAppearanceMode.Color:
						gameObject.SetColor (state._color, state._duration, state._easing, state._customEasing);
						break;
					}
				}

				if (state._tweenPosition)
				{
					Vector3 relativePosition = new Vector3 (_originalPosition.x + state._relativePosition.x, _originalPosition.y + state._relativePosition.y, _originalPosition.z + state._relativePosition.z);
					gameObject.SetPosition (relativePosition, state._duration, state._easing, state._customEasing);
				}

				if (state._tweenRotation)
				{
					Vector3 relativeRotation = new Vector3 (_originalRotation.eulerAngles.x + state._relativeRotation.x, _originalRotation.eulerAngles.y + state._relativeRotation.y, _originalRotation.eulerAngles.z + state._relativeRotation.z);
					gameObject.SetRotation (relativeRotation, state._duration, state._easing, state._customEasing);
				}

				if (state._tweenScale)
				{
					Vector3 relativeScale = new Vector3 (_originalScale.x * state._relativeScale.x, _originalScale.y * state._relativeScale.y, _originalScale.z * state._relativeScale.z);
					gameObject.SetScale (relativeScale, state._duration, state._easing, state._customEasing);
				}

				// Update the StateManager to reflect the new CrayonState.
				_currentMatchKey = matchKey;
				_currentState = state;
			}
			else
			{
				Debug.LogWarning ("State '" + stateType + "' has not been assigned for " + gameObject.name);
			}

		}

		void Destroy ()
		{
			// Unsubscribe to prevent delegate errors.
			SubscribeToParent (false);
		}


	}

}
