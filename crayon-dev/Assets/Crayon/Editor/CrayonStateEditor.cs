using System;
using UnityEngine;
using UnityEditor;
using Crayon;

[CustomEditor(typeof(CrayonState))]
[CanEditMultipleObjects]
public class CrayonStateEditor : Editor {

	// State Id

	SerializedProperty crayonStateType;
	SerializedProperty customStateType;

	// State Transition

	SerializedProperty easing;
	SerializedProperty customEasing;
	SerializedProperty duration;

	// State Properties

	SerializedProperty tweenColor;
	SerializedProperty color;

	SerializedProperty tweenPosition;
	SerializedProperty relativePosition;

	SerializedProperty tweenRotation;
	SerializedProperty relativeRotation;

	SerializedProperty tweenScale;
	SerializedProperty relativeScale;

	void OnEnable() {

		// State Id

		crayonStateType = serializedObject.FindProperty ("_crayonStateType");
		customStateType = serializedObject.FindProperty ("_customStateType");


		// State Transition

		duration = serializedObject.FindProperty ("_duration");
		easing = serializedObject.FindProperty ("_easing");
		customEasing = serializedObject.FindProperty ("_customEasing");


		// State Properties

		tweenColor = serializedObject.FindProperty ("_tweenColor");
		color = serializedObject.FindProperty ("_color");

		tweenPosition = serializedObject.FindProperty ("_tweenPosition");
		relativePosition = serializedObject.FindProperty ("_relativePosition");

		tweenRotation = serializedObject.FindProperty ("_tweenRotation");
		relativeRotation = serializedObject.FindProperty ("_relativeRotation");

		tweenScale = serializedObject.FindProperty ("_tweenScale");
		relativeScale = serializedObject.FindProperty ("_relativeScale");
	}

	public override void OnInspectorGUI()
	{


		CrayonState myScript = (CrayonState)target;

		// DrawDefaultInspector();


		// State Id

		EditorGUILayout.LabelField("State", EditorStyles.boldLabel);

		EditorGUILayout.PropertyField (crayonStateType);
		EditorGUILayout.PropertyField (customStateType);


		// State Easing

		EditorGUILayout.LabelField("Easing", EditorStyles.boldLabel);

		EditorGUILayout.PropertyField (duration);
		EditorGUILayout.PropertyField (easing);
		EditorGUILayout.PropertyField (customEasing);

		// State Properties

		EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (tweenColor, GUIContent.none, GUILayout.Width(30.0f));
		EditorGUILayout.PropertyField (color);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (tweenPosition, GUIContent.none, GUILayout.Width(30.0f));
		EditorGUILayout.PropertyField (relativePosition);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (tweenRotation, GUIContent.none, GUILayout.Width(30.0f));
		EditorGUILayout.PropertyField (relativeRotation);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.PropertyField (tweenScale, GUIContent.none, GUILayout.Width(30.0f));
		EditorGUILayout.PropertyField (relativeScale);
		EditorGUILayout.EndHorizontal ();

		// Save the changes back to the object
		EditorUtility.SetDirty(target);

		serializedObject.ApplyModifiedProperties ();

	}

}