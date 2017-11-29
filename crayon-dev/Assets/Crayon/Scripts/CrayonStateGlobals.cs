// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Crayon
{
	/// <summary>
	/// This is the high-level class that stores user-defined presets
	/// of Crayon States.
	/// </summary>
	[ExecuteInEditMode]
	[System.Serializable]
	public class CrayonStateGlobals : MonoBehaviour
	{
		// Singleton model
		private static CrayonStateGlobals instance;
		public static CrayonStateGlobals Instance
		{
			get { 
				if (instance == null)
				{
					CrayonStateGlobals i = GameObject.FindObjectOfType<CrayonStateGlobals> ();
					if (i == null)
					{
						GameObject g = GameObject.Instantiate (new GameObject ("[CrayonStateGlobals]"));
						CrayonStateGlobals c = g.AddComponent<CrayonStateGlobals> ();
						instance = c;
						instance.InitializeInEditor ();
						return c;
					}
					else
					{
						instance = i;
						instance.InitializeInEditor ();
						return i;
					}
				}
				else
				{
					return instance;
				}
			}
			set { }
		}

		public Dictionary<string, CrayonPreset> _presetsById; // These are presets of CrayonStateManagers (think of them like CSS classes or Sketch style presets).
		[HideInInspector] public string[] _presetChoices;
		public Dictionary<CrayonStateManager, bool> _isTweening = new Dictionary<CrayonStateManager, bool> ();	// This is used to check whether a particular StateManager is currently in the process of tweening.

		/// <summary>
		/// Initializes CrayonState in the Unity Editor.
		/// </summary>
		public void InitializeInEditor()
		{
			// Debug.Log ("Initializing CrayonStateGlobals.");
			ClearPresetsCache ();
			CheckUserPresetFolder ();
			LoadPresetFiles ();
		}

		/// <summary>
		/// Double-check to make sure we're initialized.
		/// </summary>
		void OnSceneGUI() {
			if (!instance)
				instance = this;
		}

		/// <summary>
		/// Loads the preset from Crayon > UserPresets.
		/// </summary>
		public void LoadPreset(GameObject g, string id) {

			// Delete all existing states on the gameObject
			CrayonState[] existingStates = g.GetComponents<CrayonState>();

			if (id == null || id == "<None>")
			{
				Debug.LogWarning ("No presets to load.");
				return;
			}
			else if (id == "<Choose Preset>")
			{
				Debug.LogWarning ("Choose a preset to load it.");
				return;
			}
			else
			{
				// Add a state for every state in the preset
				CrayonPreset preset;
				_presetsById.TryGetValue (id, out preset);
				if (preset == null)
				{
					Debug.LogWarning ("Preset not found.");
					return;
				}
				else
				{
					foreach (CrayonState state in existingStates)
					{
						DestroyImmediate (state);
					}
					foreach (CrayonStateData stateData in preset._crayonStatesData)
					{
						stateData.LoadData (g);
					}
				}
			}
		}

		/// <summary>
		/// Checks whether the Unity user has an available folder for saving presets.
		/// </summary>
		private void CheckUserPresetFolder()
		{
			#if UNITY_EDITOR			
			if(!AssetDatabase.IsValidFolder("Assets/Crayon/UserPresets"))
			{
				Debug.Log("Creating Crayon/UserPresets directory");
				AssetDatabase.CreateFolder ("Assets/Crayon", "UserPresets");
			}
			#endif
		}

		/// <summary>
		/// Saves the preset to the internal CrayonStateGlobals list.
		/// </summary>
		public void SavePreset(GameObject g, string id, CrayonStateManager sentBy)
		{
			CrayonState[] states = g.GetComponents<CrayonState> ();
			// Create a new preset.
			CrayonPreset preset = new CrayonPreset(id);
			// Add data to the preset
			foreach (CrayonState state in states)
			{
				// Save data to a new data class
				CrayonStateData stateData = new CrayonStateData(state);
				// Add to the preset's list
				preset.AddStateData(stateData);
			}
			// Is this already in the dictionary? If so, override
			CrayonPreset oldPreset;
			if (_presetsById.TryGetValue (preset._id, out oldPreset))
				_presetsById.Remove (preset._id);
			_presetsById.Add(preset._id, preset);
			Debug.LogWarningFormat ("Crayon preset {0} was saved.", preset._id);
			sentBy._newPresetId = null;
			SavePresetFiles ();
		}

		/// <summary>
		/// Serializes and saves the Preset in JSON format to a file in Crayon > UserPresets.
		/// </summary>
		private void SavePresetFiles()
		{
			foreach (CrayonPreset preset in _presetsById.Values)
			{
				string json = JsonUtility.ToJson(preset, true);
				string path = Application.dataPath + "/Crayon/UserPresets/" + preset._id + ".txt";
				File.WriteAllText (path, json);
			}
			// Re-initialize
			InitializeInEditor();
		}

		/// <summary>
		/// Reads in the serialized Preset as JSON and triggers creation of the appropriate CrayonStates.
		/// </summary>
		private void LoadPresetFiles()
		{
			string presetsPath = Application.dataPath + "/Crayon/UserPresets/";
			// Go through all the files in the UserPresets folder and add to Dictionary
			DirectoryInfo info = new DirectoryInfo(presetsPath);
			FileInfo[] files = info.GetFiles ();
			foreach (FileInfo f in files)
			{
				// is this a text file (i.e. not a meta file)
				if (f.Extension == ".txt")
				{
					string json = File.ReadAllText (f.FullName);
					CrayonPreset preset;
					preset = JsonUtility.FromJson<CrayonPreset> (json);
					_presetsById.Add (preset._id, preset);
				}
			}
			// These presets should be visible in the Editor
			PopulatePresetsDropdown ();
		}

		/// <summary>
		/// Makes it so Presets are visible in the Editor under the CrayonStateManager component.
		/// </summary>
		public void PopulatePresetsDropdown()
		{
			_presetChoices = new string[_presetsById.Count + 1];
			_presetChoices [0] = "<Choose Preset>";
			int i = 1;
			foreach (string id in _presetsById.Keys)
			{
				_presetChoices [i] = id;
				i++;
			}
			// Populate with 'null'
			if (_presetChoices.Length == 0)
			{
				_presetChoices = new string[1];
				_presetChoices [0] = "<None>";
			}
		}

		/// <summary>
		/// Resets all the references presets used in the Editor.
		/// </summary>
		public void ClearPresetsCache()
		{
			_presetsById = new Dictionary<string, CrayonPreset> ();
			_presetChoices = new string[0];
		}

		/// <summary>
		/// Removes saved user presets from Crayon > UserPresets.
		/// </summary>
		public void DeleteAllPresets()
		{
			Debug.LogWarning ("Deleting all user presets from Crayon > UserPresets.");
			string presetsPath = Application.dataPath + "/Crayon/UserPresets/";
			// Go through all the files in the UserPresets folder and add to Dictionary
			DirectoryInfo info = new DirectoryInfo(presetsPath);
			FileInfo[] files = info.GetFiles ();
			foreach (FileInfo f in files)
			{
				File.Delete (f.FullName);
			}
			InitializeInEditor ();
		}
	}
}
