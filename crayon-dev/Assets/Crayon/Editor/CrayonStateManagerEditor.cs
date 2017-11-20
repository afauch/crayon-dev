using System;
using UnityEngine;
using UnityEditor;
using Crayon;

[CustomEditor(typeof(CrayonStateManager))]
public class CrayonStateManagerEditor : Editor {

	int _choiceIndex = 0;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CrayonStateManager myScript = (CrayonStateManager)target;

		try
		{
			_choiceIndex = EditorGUILayout.Popup(_choiceIndex, CrayonStateGlobals.Instance._presetChoices);
			// Update the selected choice in the underlying object
			myScript._presetToLoad = CrayonStateGlobals.Instance._presetChoices[_choiceIndex];
		} catch(Exception e) {
			Debug.LogWarning ("No presets to load.");
		}

		if (GUILayout.Button("Save Preset"))
		{
			myScript.SavePreset();
		}
		if (GUILayout.Button("Load Preset"))
		{
			myScript.LoadPreset();
		}

		// Save the changes back to the object
		EditorUtility.SetDirty(target);

	}

}