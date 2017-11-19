using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {

	// This is the high-level class that will store presets
	// and the list of custom states used across the application

	[ExecuteInEditMode]
	public class CrayonStateGlobals : Singleton<CrayonStateGlobals> {

		public static CrayonStateGlobals Instance;
		public Dictionary<string, CrayonPreset> _presetsById;			// these are presets of CrayonStateManagers (think of them like CSS classes or Sketch style presets)

		public void InitializeInEditor() {
			Debug.Log ("InitializeInEditor called.");
			Instance = this;
			// TODO: This really needs to be serialized and stored as a text file, otherwise it won't persist between Editor states
			if (_presetsById == null)
				ClearPresets ();
			Debug.Log ("Current Dictionary Size is: " + _presetsById.Count);
		}

		void Awake() {
			Debug.Log ("Awake called.");
			Instance = this;
			if (_presetsById == null)
				ClearPresets ();
			Debug.Log ("Current Dictionary Size is: " + _presetsById.Count);
		}

		// TODO: I'm sure there's a better way to structure this
		public void LoadPreset(GameObject g, string id) {

			// Delete all existing states on the gameObject
			CrayonState[] existingStates = g.GetComponents<CrayonState>();
			foreach (CrayonState state in existingStates) {
				DestroyImmediate (state);
			}

			// Add a state for every state in the preset
			CrayonPreset preset;
			_presetsById.TryGetValue (id, out preset);

			foreach(CrayonStateData stateData in preset._crayonStatesData) {

				// TODO: May be better to structure this differently so the methods aren't on the StateData
				stateData.LoadData (g);

			}

		}

		public void SavePreset(GameObject g, string id) {

			CrayonState[] states = g.GetComponents<CrayonState> ();

			// Create a new preset.
			CrayonPreset preset = new CrayonPreset(id);

			// Add data to the preset
			foreach (CrayonState state in states) {

				// Save data to a new data class
				CrayonStateData stateData = new CrayonStateData(state);

				// Add to the preset's list
				preset.AddStateData(stateData);

			}

			// Add Preset to the Dictionary

			// Is this already in the dictionary? If so, override
			CrayonPreset oldPreset;
			if (_presetsById.TryGetValue (preset._id, out oldPreset))
				_presetsById.Remove (preset._id);

			_presetsById.Add(preset._id, preset);
			Debug.Log ("Crayon Preset Saved");

			Debug.Log ("Number of items in preset dict: " + _presetsById.Count);
				
		}

		// Resets all the presets
		public void ClearPresets() {

			_presetsById = new Dictionary<string, CrayonPreset> ();

		}


	}

}
