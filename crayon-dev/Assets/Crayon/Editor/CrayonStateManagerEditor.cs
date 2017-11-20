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

	void OnEnable() {

		_listenToParent = serializedObject.FindProperty ("_listenToParent");
		_newPresetId = serializedObject.FindProperty ("_newPresetId");

	}

	public override void OnInspectorGUI()
	{

		CrayonStateManager myScript = (CrayonStateManager)target;

		// DrawDefaultInspector();
	
		// Listen to Parent field
		EditorGUILayout.LabelField("Parenting", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField (_listenToParent, new GUIContent ("Listen to Parent"));

		// Load Fields
		EditorGUILayout.LabelField("Load a Preset", EditorStyles.boldLabel);
		Rect load = EditorGUILayout.BeginHorizontal ();
		try
		{
			_choiceIndex = EditorGUILayout.Popup(_choiceIndex, CrayonStateGlobals.Instance._presetChoices, GUILayout.Width(150.0f));
			// Update the selected choice in the underlying object
			myScript._presetToLoad = CrayonStateGlobals.Instance._presetChoices[_choiceIndex];
		} catch(Exception e) {
			Debug.LogWarning ("No presets to load – reinitializing.");
			CrayonStateGlobals.Instance.InitializeInEditor ();
		}
		if (GUILayout.Button("Load",GUILayout.Width(150.0f)))
		{
			myScript.LoadPreset();
		}
		EditorGUILayout.EndHorizontal ();

		// Save Fields
		EditorGUILayout.LabelField("Save a Preset", EditorStyles.boldLabel);
		Rect save = EditorGUILayout.BeginHorizontal ();
		// New Preset Id field
		EditorGUILayout.PropertyField (_newPresetId, GUIContent.none, GUILayout.Width(150.0f));

		if (GUILayout.Button("Save",GUILayout.Width(150.0f)))
		{
			myScript.SavePreset();
		}

		EditorGUILayout.EndHorizontal ();

		// Save the changes back to the object
		EditorUtility.SetDirty(target);

		serializedObject.ApplyModifiedProperties ();

	}

}