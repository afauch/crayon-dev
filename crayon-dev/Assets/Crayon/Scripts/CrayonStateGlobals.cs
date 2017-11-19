using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crayon {

	// This is the high-level class that will store presets
	// and the list of custom states used across the application

	public class CrayonStateGlobals : Singleton<CrayonStateGlobals> {

		public static CrayonStateGlobals Instance;
		public Dictionary<string, CrayonPreset> _presetsById;			// these are presets of CrayonStateManagers (think of them like CSS classes or Sketch style presets)

		public void InitializeInEditor() {

			Instance = this;
			_presetsById = new Dictionary<string, CrayonPreset> ();

		}

		void Awake() {
			Instance = this;
		}

		// TODO: I'm sure there's a better way to structure this
		public void LoadPreset(GameObject gameObject, string id) {


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
