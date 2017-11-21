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


		void Awake() {

			InitDictionary ();
			if (_listenToParent) {
				Debug.Log ("Calling SubscribeToParent");
				SubscribeToParent ();
			}

		}

		void Update() {

		}

		public void InitDictionary () {
			
			_allStatesByMatchKey = new Dictionary<string, CrayonState> ();

			CrayonState[] states = GetComponents<CrayonState> ();

			Debug.Log ("# of CrayonStates found on " + gameObject.name + " is " + states.Length);

			foreach(CrayonState state in states) {
				_allStatesByMatchKey.Add (state._crayonMatchKey, state);
			}

		}

		// Look to parent and subscribe to their Change State event
		public void SubscribeToParent () {
			Transform[] t = gameObject.GetComponentsInParent<Transform> ();
			CrayonStateManager[] parent = gameObject.GetComponentsInParent<CrayonStateManager> ();
			parent[1].OnChangeState += ChangeState;
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

			// Debug.Log ("Looking for Match key " + matchKey);

			CrayonState state;
			_allStatesByMatchKey.TryGetValue (matchKey, out state);

			// TODO: Add conditionals to make this more efficient
			if (state != null) {
				// Actually do the tween
				gameObject.SetColor (state._color, state._duration, state._easing);
				gameObject.SetRelativePosition (state._relativePosition, state._duration, state._easing);
				gameObject.SetRelativeRotation (state._relativeRotation, state._duration, state._easing);
				gameObject.SetRelativeScale (state._relativeScale, state._duration, state._easing);
			} else {
				Debug.LogWarning (stateType + " has not been assigned for " + gameObject.name);
			}

		}

		void OnValidate() {

			// Debug.Log ("_listenToParent is " + _listenToParent);

		}


	}

}
