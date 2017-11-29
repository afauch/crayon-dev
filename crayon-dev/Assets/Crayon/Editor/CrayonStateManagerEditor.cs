using System;
using UnityEngine;
using UnityEditor;
using Crayon;

[CustomEditor(typeof(CrayonStateManager))]
[CanEditMultipleObjects]
public class CrayonStateManagerEditor : Editor {

	int _choiceIndex = 0;

	SerializedProperty _listenToParent;
	SerializedProperty _newPresetId;

	bool _showPresets;

	void OnEnable() {

		_listenToParent = serializedObject.FindProperty ("_listenToParent");
		_newPresetId = serializedObject.FindProperty ("_newPresetId");

	}

	public override void OnInspectorGUI()
	{


		CrayonStateManager myScript = (CrayonStateManager)target;
			
		// Listen to Parent field
		EditorGUILayout.LabelField ("Parenting", EditorStyles.boldLabel);
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (_listenToParent, GUIContent.none, GUILayout.Width (24.0f));
		EditorGUILayout.LabelField ("Listen to Parent", GUILayout.Width (112.0f));
		EditorGUILayout.EndHorizontal ();

		_showPresets = EditorGUILayout.Foldout (_showPresets, "Presets");

		if (_showPresets) {

			// Load Fields
			EditorGUILayout.LabelField ("Load a Preset", EditorStyles.boldLabel);
			EditorGUILayout.BeginHorizontal ();
			try {
				_choiceIndex = EditorGUILayout.Popup (_choiceIndex, CrayonStateGlobals.Instance._presetChoices, GUILayout.Width (100.0f));
				// Update the selected choice in the underlying object
				myScript._presetToLoad = CrayonStateGlobals.Instance._presetChoices [_choiceIndex];
			} catch {
				Debug.LogWarning ("No presets to load – reinitializing.");
				CrayonStateGlobals.Instance.InitializeInEditor ();
			}
			if (GUILayout.Button ("Load", GUILayout.Width (100.0f))) {
				myScript.LoadPreset ();
			}
			EditorGUILayout.EndHorizontal ();

			// Save Fields
			EditorGUILayout.LabelField ("Save a Preset", EditorStyles.boldLabel);
			EditorGUILayout.BeginHorizontal ();
			// New Preset Id field
			EditorGUILayout.PropertyField (_newPresetId, GUIContent.none, GUILayout.Width (100.0f));

			if (GUILayout.Button ("Save", GUILayout.Width (100.0f))) {
				myScript.SavePreset ();
			}

			EditorGUILayout.EndHorizontal ();

		}

		if (GUILayout.Button ("Add State")) {
			myScript.AddState ();
		}

		// Save the changes back to the object
		EditorUtility.SetDirty(target);

		serializedObject.ApplyModifiedProperties ();

	}

}