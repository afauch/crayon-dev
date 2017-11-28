using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Crayon {

	// This is the high-level class that will store presets
	// and the list of custom states used across the application

	[ExecuteInEditMode]
	[System.Serializable]
	public class CrayonStateGlobals : MonoBehaviour {

		private static CrayonStateGlobals instance;
		public static CrayonStateGlobals Instance {

			get { 
			
				if (instance == null) {
					CrayonStateGlobals i = GameObject.FindObjectOfType<CrayonStateGlobals> ();
					if (i == null) {
						// Debug.Log ("No CrayonStateGlobals found - creating a new GameObject");
						GameObject g = GameObject.Instantiate (new GameObject ("[CrayonStateGlobals]"));
						CrayonStateGlobals c = g.AddComponent<CrayonStateGlobals> ();
						instance = c;
						instance.InitializeInEditor ();
						return c;
					} else {
						// Debug.Log ("instance was null but found an instance of CrayonStateGlobals.");
						instance = i;
						instance.InitializeInEditor ();
						return i;
					}
				} else {
					// Debug.Log ("instance is not null - returning instance.");
					return instance;
				}
			}

			set { }

		}

		public Dictionary<string, CrayonPreset> _presetsById;			// these are presets of CrayonStateManagers (think of them like CSS classes or Sketch style presets)
		public string[] _presetChoices;

		public Dictionary<CrayonStateManager, bool> _isTweening = new Dictionary<CrayonStateManager, bool> ();		// This is used to check whether a particular StateManager is currently tweening

		public void InitializeInEditor() {
			Debug.Log ("Initializing CrayonStateGlobals.");

			ClearPresetsCache ();
			CheckUserPresetFolder ();
			LoadPresetFiles ();

		}

//		void Awake() {
//			InitializeInEditor ();
//		}

		// Double-check to make sure we're initialized
		void OnSceneGUI() {
			if (!instance)
				instance = this;
		}

		// TODO: I'm sure there's a better way to structure this
		public void LoadPreset(GameObject g, string id) {

			// Delete all existing states on the gameObject
			CrayonState[] existingStates = g.GetComponents<CrayonState>();

			if (id == null || id == "<None>") {
				Debug.LogWarning ("No presets to load.");
				return;
			} else if (id == "<Choose Preset>") {

				Debug.LogWarning ("Choose a preset to load it.");
				return;

			} else {

				// Add a state for every state in the preset
				CrayonPreset preset;
				_presetsById.TryGetValue (id, out preset);

				if (preset == null) {

					Debug.LogWarning ("Preset not found.");
					return;

				} else {

					foreach (CrayonState state in existingStates) {
						DestroyImmediate (state);
					}

					foreach (CrayonStateData stateData in preset._crayonStatesData) {

						// TODO: May be better to structure this differently so the methods aren't on the StateData
						stateData.LoadData (g);

					}

				}

			}

		}

		private void CheckUserPresetFolder() {
			#if UNITY_EDITOR			
			if(!AssetDatabase.IsValidFolder("Assets/Crayon/UserPresets")) {
				Debug.Log("Creating Crayon/UserPresets directory");
				AssetDatabase.CreateFolder ("Assets/Crayon", "UserPresets");
			}
			#endif
		}

		public void SavePreset(GameObject g, string id, CrayonStateManager sentBy) {

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
			Debug.Log ("Crayon Preset " + preset._id + " Saved");
			Debug.Log ("Number of items in preset dict: " + _presetsById.Count);

			sentBy._newPresetId = null;

			SavePresetFiles ();

				
		}

		private void SavePresetFiles() {

			// Can it serialize a single state?

			foreach (CrayonPreset preset in _presetsById.Values) {

				string json = JsonUtility.ToJson(preset, true);
				// Debug.Log (json);

				string path = Application.dataPath + "/Crayon/UserPresets/" + preset._id + ".txt";
				File.WriteAllText (path, json);

			}

			// Re-Initialize with new preset
			InitializeInEditor();

		}

		// Read data from the file
		private void LoadPresetFiles() {

			string presetsPath = Application.dataPath + "/Crayon/UserPresets/";

			// Go through all the files in the UserPresets folder and add to Dictionary
			DirectoryInfo info = new DirectoryInfo(presetsPath);
			FileInfo[] files = info.GetFiles ();
			foreach (FileInfo f in files) {

				// is this a text file (i.e. not a meta file)
				if (f.Extension == ".txt") {
					// Debug.Log (f);
					string json = File.ReadAllText (f.FullName);
					// Debug.Log (json);

					CrayonPreset preset;
					preset = JsonUtility.FromJson<CrayonPreset> (json);

					_presetsById.Add (preset._id, preset);

				}
					
			}
				

			PopulatePresetsDropdown ();

		}

		public void PopulatePresetsDropdown() {

			// Debug.Log ("PopulatePresetsDropdown called.");

			_presetChoices = new string[_presetsById.Count + 1];
			_presetChoices [0] = "<Choose Preset>";
			int i = 1;
			foreach (string id in _presetsById.Keys) {
				_presetChoices [i] = id;
				i++;
			}

			// Populate with 'null'
			if (_presetChoices.Length == 0) {
				_presetChoices = new string[1];
				_presetChoices [0] = "<None>";
			}

		}

		// Resets all the presets
		public void ClearPresetsCache() {

			_presetsById = new Dictionary<string, CrayonPreset> ();
			_presetChoices = new string[0];

		}
//
//		public int GetIntFromString(string choice) {
//
//			for (int i = 0; i < _presetChoices.Length; i++) {
//				if (choice == _presetChoices [i]) {
//					return i;
//				}
//			}
//
//		}

		public void DeleteAllPresets() {

			string presetsPath = Application.dataPath + "/Crayon/UserPresets/";

			// Go through all the files in the UserPresets folder and add to Dictionary
			DirectoryInfo info = new DirectoryInfo(presetsPath);
			FileInfo[] files = info.GetFiles ();
			foreach (FileInfo f in files) {
				File.Delete (f.FullName);
			}

			InitializeInEditor ();

		}


	}

}
