using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Crayon;

namespace Crayon {

	public class CrayonStateManager : MonoBehaviour {

		public bool _listenToParent;
		public string _newPresetId = "<Enter A Preset Name>";
		[HideInInspector] public string _presetToLoad;

		public Dictionary<string,CrayonState> _allStatesByMatchKey;

		public delegate void ChangeStateDelegate (CrayonStateType stateType, string customState = "");
		public ChangeStateDelegate OnChangeState;
		public delegate void FreezeTransformDelegate ();
		public FreezeTransformDelegate OnFreezeTransform;

		private string _currentMatchKey = null;
		private CrayonStateType _currentState = CrayonStateType.Default; 
		private Vector3 _originalPosition;
		private Quaternion _originalRotation;
		private Vector3 _originalScale;

		void Awake() {

			InitDictionary ();
			if (_listenToParent) {
				Debug.Log ("Calling SubscribeToParent");
				SubscribeToParent (true);
			}

			// TODO: This is a hack and should be cleaned up.
			_currentState = CrayonStateType.Default;
			FreezeTransform ();

		}

		void Update() {

			if (_currentState == CrayonStateType.Default) {

				FreezeTransform ();

			}

		}

		public void FreezeTransform () {

			if (gameObject.transform.localPosition != _originalPosition ||
			   gameObject.transform.localRotation != _originalRotation ||
			   gameObject.transform.localScale != _originalScale) {

				_originalPosition = gameObject.transform.localPosition;
				_originalRotation = gameObject.transform.localRotation;
				_originalScale = gameObject.transform.localScale;

			}

			if (OnFreezeTransform != null)
				OnFreezeTransform ();

		}

		public void InitDictionary () {
			
			_allStatesByMatchKey = new Dictionary<string, CrayonState> ();

			CrayonState[] states = GetComponents<CrayonState> ();

			foreach(CrayonState state in states) {
				_allStatesByMatchKey.Add (state._crayonMatchKey, state);
			}

		}

		// Look to parent and subscribe to their Change State event
		public void SubscribeToParent (bool subscribe) {
			// Transform[] t = gameObject.GetComponentsInParent<Transform> ();
			CrayonStateManager[] parent = gameObject.GetComponentsInParent<CrayonStateManager> ();

			try {

			if (subscribe) { // If true, subscribe
				parent [1].OnChangeState += ChangeState;
				parent [1].OnFreezeTransform += FreezeTransform;
			} else { // Otherwise, unsubscribe
				parent [1].OnChangeState -= ChangeState;
				parent [1].OnFreezeTransform -= FreezeTransform;
			}

			} catch {

				Debug.LogWarning ("No parent to subscribe to.");

			}

		}

		public void LoadPreset() {
 			CrayonStateGlobals.Instance.LoadPreset (this.gameObject, _presetToLoad);
		}

		public void SavePreset() {
			CrayonStateGlobals.Instance.SavePreset (this.gameObject, _newPresetId, this);
		}

		public void ChangeState(CrayonStateType stateType, string customState = "") {

			if (OnChangeState != null) {
				OnChangeState (stateType, customState);
			}
				
			// What's our end state

			// Find the state by match key

			// TODO: Add error handling for nonexistent states
			string matchKey = stateType.ToString().ToLower() + customState;

			// Don't do anything if we're already on the right state
			if (matchKey == _currentMatchKey)
				return;

			CrayonState state;
			_allStatesByMatchKey.TryGetValue (matchKey, out state);

			// TODO: Add conditionals to make this more efficient
			if (state != null) {


				// Actually do the tween

				if (state._tweenColor) {
					// Debug.Log ("TweenColor is true");
					gameObject.SetColor (state._color, state._duration, state._easing, state._customEasing);
				}

				if (state._tweenPosition) {
					// Debug.Log ("TweenPosition is true");					
					Vector3 relativePosition = new Vector3 (_originalPosition.x + state._relativePosition.x, _originalPosition.y + state._relativePosition.y, _originalPosition.z + state._relativePosition.z);
					gameObject.SetPosition (relativePosition, state._duration, state._easing, state._customEasing);
				}

				if (state._tweenRotation) {
					// Debug.Log ("TweenRotation is true");					
					Vector3 relativeRotation = new Vector3 (_originalRotation.eulerAngles.x + state._relativeRotation.x, _originalRotation.eulerAngles.y + state._relativeRotation.y, _originalRotation.eulerAngles.z + state._relativeRotation.z);
					gameObject.SetRotation (relativeRotation, state._duration, state._easing, state._customEasing);
				}

				if (state._tweenScale) {
					// Debug.Log ("TweenScale is true");					
					Vector3 relativeScale = new Vector3 (_originalScale.x * state._relativeScale.x, _originalScale.y * state._relativeScale.y, _originalScale.z * state._relativeScale.z);
					gameObject.SetScale (relativeScale, state._duration, state._easing, state._customEasing);
				}

				_currentMatchKey = matchKey;
				_currentState = state._crayonStateType;

			} else {
				Debug.LogWarning (stateType + " has not been assigned for " + gameObject.name);
			}

		}

		void OnValidate() {

			// Debug.Log ("_listenToParent is " + _listenToParent);

		}

		void Destroy () {

			SubscribeToParent (false);

		}


	}

}
