using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Crayon;

namespace Crayon {

	public class CrayonStateManager : MonoBehaviour {

		public bool _listenToParent;

		public Dictionary<string,CrayonState> _allStatesByMatchKey;

		[SerializeField]
		public List<CrayonState> _statesList;							// TODO: Create custom Editor GUI for these states

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

			Debug.Log (gameObject.name + " OnChangeState is " + OnChangeState);

		}

		public void InitDictionary () {
			_allStatesByMatchKey = new Dictionary<string, CrayonState> ();
			foreach(CrayonState state in _statesList) {
				_allStatesByMatchKey.Add (state._crayonMatchKey, state);
			}
		}

		// Look to parent and subscribe to their Change State event
		public void SubscribeToParent () {
			Debug.Log ("Subscribe to Parent called on " + gameObject.name);
			Transform[] t = gameObject.GetComponentsInParent<Transform> ();
			Debug.Log (gameObject.name + "'s parent transform is " + t[1].gameObject.name);
			CrayonStateManager[] parent = gameObject.GetComponentsInParent<CrayonStateManager> ();
			Debug.Log ("Parent component = " + parent[1]);
			parent[1].OnChangeState += ChangeState;
			Debug.Log ("parent.OnChangeState is" + parent[1].OnChangeState);
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

			Debug.Log ("ChangeState called on " + gameObject.name);
			Debug.Log (OnChangeState);
			if (OnChangeState != null) {
				Debug.Log ("OnChangeState != null for " + gameObject.name);
				OnChangeState (stateType, customState);
			}


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
			gameObject.SetColor(state._color, state._duration, state._easing);
			gameObject.SetRelativePosition (state._relativePosition, state._duration, state._easing);
			gameObject.SetRelativeRotation (state._relativeRotation, state._duration, state._easing);
			gameObject.SetRelativeScale (state._relativeScale, state._duration, state._easing);

		}

		// This helps us maintain match keys correctly based on editor data changes
		// OnValidate gets called upon data changes in editor
		void OnValidate() {

			if (_allStatesByMatchKey == null) {
				_allStatesByMatchKey = new Dictionary<string, CrayonState> ();
			}

			Debug.Log ("On Validate Called at " + Time.fixedTime);

			// TODO: Optimize this for performance
			if(!_listenToParent)
				gameObject.GetComponentInParent<CrayonStateManager> ().OnChangeState -= ChangeState;

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
