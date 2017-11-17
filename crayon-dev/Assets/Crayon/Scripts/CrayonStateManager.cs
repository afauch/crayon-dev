using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Crayon;

namespace Crayon {

	public class CrayonStateManager : MonoBehaviour {
		
		public Dictionary<string,CrayonState> _allStatesByMatchKey;
		public List<CrayonState> _statesList;							// TODO: Create custom Editor GUI for these states

		void Start() {

			InitDictionary ();

		}

		public void InitDictionary () {

			_allStatesByMatchKey = new Dictionary<string, CrayonState> ();
			foreach(CrayonState state in _statesList) {
				_allStatesByMatchKey.Add (state._crayonMatchKey, state);
			}


		}

		// TODO: Troubleshoot issues when creating/managing new states
		public CrayonState AddState() {

			// This creates a state and adds it to the Dictionary
			CrayonState newState = new CrayonState ();

			// Generate random ID
			// Add to dictionary
			_allStatesByMatchKey.Add (newState._crayonMatchKey, newState);
			// Add to list so it can visible in the editor
			_statesList.Add(newState);
			return newState;

		}

		public void RemoveState(string matchKey) {
			// Remove the state from the dictionary
			_allStatesByMatchKey.Remove(matchKey);

			// Remove the state from the list
			foreach (CrayonState state in _statesList) {
				if (state._crayonMatchKey == matchKey) {
					_statesList.Remove (state);
				}
			}

		}

		public void ChangeState(CrayonStateType stateType, string customState = "") {

			Debug.Log ("Current size of dictionary: " + _allStatesByMatchKey.Count);

			// What's our start state (does it matter)?
			// Keep in mind - you might want a transition to start halfway through an interpolation
			// So you should pass interpolated values

			// What's our end state

			// Find the state by match key

			// TODO: Add error handling for nonexistent states

			string matchKey = stateType.ToString().ToLower() + customState;
			Debug.Log ("Trying match key " + matchKey);

			CrayonState state;
			_allStatesByMatchKey.TryGetValue (matchKey, out state);

			Debug.Log (gameObject.name);

			// TODO: Add conditionals to make this more efficient

			// Actually do the tween
			gameObject.SetColor(state._color);
			gameObject.SetRelativePosition (state._relativePosition);
			gameObject.SetRelativeRotation (state._relativeRotation);
			gameObject.SetRelativeScale (state._relativeScale);



		}

		// This helps us maintain match keys correctly based on editor data changes
		// OnValidate gets called upon data changes in editor
		void OnValidate() {

			Debug.Log ("On Validate Called at " + Time.fixedTime);

			foreach (CrayonState state in _statesList) {
				string oldMatchKey = state._crayonMatchKey;
				string newMatchKey = state.UpdateMatchKey ();

				// Reconfigure the dictionary
				if (oldMatchKey != newMatchKey) {
					_allStatesByMatchKey.Remove(oldMatchKey);
					_allStatesByMatchKey.Add(newMatchKey, state);
				}
			}
		}

	}

}
