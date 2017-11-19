using UnityEngine;
using UnityEditor;
using Crayon;

[CustomEditor(typeof(CrayonStateManager))]
public class CrayonStateManagerEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CrayonStateManager myScript = (CrayonStateManager)target;
		if (GUILayout.Button("Save Preset"))
		{
			myScript.SavePreset();
		}


	}

}