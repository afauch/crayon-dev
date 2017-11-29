// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEditor;
using Crayon;

/// <summary>
/// Editor UI for CrayonStateGlobals class.
/// </summary>
[CustomEditor(typeof(CrayonStateGlobals))]
public class CrayonStateGlobalsEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CrayonStateGlobals myScript = (CrayonStateGlobals)target;

		// Button for initializing an instance of CrayonStateGlobals
		if (GUILayout.Button("Initialize"))
		{
			myScript.InitializeInEditor();
		}

		// Button for clearing presets from Crayon > UserPresets
		if (GUILayout.Button("Delete All Presets"))
		{
			myScript.DeleteAllPresets();
		}
	}
}