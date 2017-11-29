using System;
using UnityEngine;
using UnityEditor;
using Crayon;

[CustomEditor(typeof(CrayonState))]
[CanEditMultipleObjects]
public class CrayonStateEditor : Editor {

	// Layout Variables
	float _checkboxWidth = 24.0f;
	float _labelsWidth = 112.0f;
	bool _showTransition = false;
	bool _showProperties = false;
	bool _isDefault;

	// State Id

	SerializedProperty crayonStateType;
	SerializedProperty customStateType;

	// State Transition

	SerializedProperty easing;
	SerializedProperty customEasing;
	SerializedProperty duration;

	// State Properties

	SerializedProperty tweenAppearance;
	SerializedProperty tweenAppearanceMode;

	SerializedProperty tweenMaterial;
	SerializedProperty material;

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

		tweenAppearance = serializedObject.FindProperty ("_tweenAppearance");
		tweenAppearanceMode = serializedObject.FindProperty ("_tweenAppearanceMode");

		tweenMaterial = serializedObject.FindProperty ("_tweenMaterial");
		material = serializedObject.FindProperty ("_material");

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
		EditorGUILayout.LabelField ("State", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField (crayonStateType, GUIContent.none);
		if (crayonStateType.enumValueIndex == (crayonStateType.enumNames.Length - 1)) {
			EditorGUILayout.PropertyField (customStateType, new GUIContent("Custom State Name"));
		}


		// State Easing

		_showTransition = EditorGUILayout.Foldout (_showTransition, "Transition");

		// EditorGUILayout.LabelField("Easing", EditorStyles.boldLabel);

		if (_showTransition) {

			EditorGUILayout.PropertyField (duration);
			EditorGUILayout.PropertyField (easing);
			if (easing.enumValueIndex == (easing.enumNames.Length - 1)) {
				EditorGUILayout.PropertyField (customEasing);
			}

		}
			

		// Hide values for Default states

		_isDefault = crayonStateType.enumValueIndex == 0 ? true : false;

		_showProperties = EditorGUILayout.Foldout (_showProperties, "Properties");


		if (_showProperties) {


			// State Properties

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PropertyField (tweenAppearance, GUIContent.none, GUILayout.Width (_checkboxWidth));
			EditorGUILayout.PropertyField (tweenAppearanceMode, GUIContent.none, GUILayout.Width (_labelsWidth));
			if (tweenAppearance.boolValue) {

				switch (tweenAppearanceMode.enumValueIndex) {
				case 0:
					EditorGUILayout.PropertyField (material, GUIContent.none);
					break;
				case 1:
					EditorGUILayout.PropertyField (color, GUIContent.none);
					break;
				case 2:
					// Debug.Log ("Opacity feature not yet implemented.");
					break;
				}


			}
			EditorGUILayout.EndHorizontal ();

//			EditorGUILayout.BeginHorizontal ();
//			EditorGUI.BeginDisabledGroup (tweenColor.boolValue == true);
//			EditorGUILayout.PropertyField (tweenMaterial, GUIContent.none, GUILayout.Width (_checkboxWidth));
//			EditorGUILayout.LabelField ("Material", GUILayout.Width (_labelsWidth));
//			if (tweenMaterial.boolValue) {
//				EditorGUI.BeginDisabledGroup (_isDefault);
//				EditorGUILayout.PropertyField (material, GUIContent.none);
//				EditorGUI.EndDisabledGroup ();
//			}
//			EditorGUI.EndDisabledGroup ();
//			EditorGUILayout.EndHorizontal ();
//
//			EditorGUILayout.BeginHorizontal ();
//			EditorGUI.BeginDisabledGroup (tweenMaterial.boolValue == true);
//			EditorGUILayout.PropertyField (tweenColor, GUIContent.none, GUILayout.Width (_checkboxWidth));
//			EditorGUILayout.LabelField ("Color", GUILayout.Width (_labelsWidth));
//			if (tweenColor.boolValue) {
//				EditorGUI.BeginDisabledGroup (_isDefault);
//				EditorGUILayout.PropertyField (color, GUIContent.none);
//				EditorGUI.EndDisabledGroup ();
//			}
//			EditorGUI.EndDisabledGroup ();
//			EditorGUILayout.EndHorizontal ();


			EditorGUI.BeginDisabledGroup (_isDefault);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PropertyField (tweenPosition, GUIContent.none, GUILayout.Width (_checkboxWidth));
			EditorGUILayout.LabelField ("Relative Position", GUILayout.Width (_labelsWidth));
			if (tweenPosition.boolValue) {
				EditorGUILayout.PropertyField (relativePosition, GUIContent.none);
			}
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PropertyField (tweenRotation, GUIContent.none, GUILayout.Width (_checkboxWidth));
			EditorGUILayout.LabelField ("Relative Rotation", GUILayout.Width (_labelsWidth));
			if (tweenRotation.boolValue) {
				EditorGUILayout.PropertyField (relativeRotation, GUIContent.none);
			}
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PropertyField (tweenScale, GUIContent.none, GUILayout.Width (_checkboxWidth));
			EditorGUILayout.LabelField ("Relative Scale", GUILayout.Width (_labelsWidth));
			if (tweenScale.boolValue) {
				EditorGUILayout.PropertyField (relativeScale, GUIContent.none);
			}
			EditorGUILayout.EndHorizontal ();

			EditorGUI.EndDisabledGroup ();

		}
			

		// Save the changes back to the object
		EditorUtility.SetDirty(target);

		serializedObject.ApplyModifiedProperties ();

	}

}