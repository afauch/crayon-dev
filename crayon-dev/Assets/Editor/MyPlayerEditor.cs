using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (MyPlayer))]
[CanEditMultipleObjects]
public class MyPlayerEditor : Editor {

	SerializedProperty damageProp;
	SerializedProperty armorProp;
	SerializedProperty gunProp;

	void OnEnable() {
		// Set up the Serialized Properties
		damageProp = serializedObject.FindProperty("damage");
		armorProp = serializedObject.FindProperty ("armor");
		gunProp = serializedObject.FindProperty ("gun");

	}

	public override void OnInspectorGUI() {
		// Update the serialized property; always do this in beginning of OnInspectorGUI
		serializedObject.Update();

	}

}
