using UnityEngine;
using UnityEditor;
using Crayon;

[CustomEditor(typeof(CrayonStateGlobals))]
public class CrayonStateGlobalsEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CrayonStateGlobals myScript = (CrayonStateGlobals)target;
		if (GUILayout.Button("Initialize"))
		{
			myScript.InitializeInEditor();
		}


	}

}