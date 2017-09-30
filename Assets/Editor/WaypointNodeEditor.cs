using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointNodeScript))]
public class WaypointNodeEditor : Editor
{
	SerializedProperty data;

	// Use this for initialization
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		data = serializedObject.FindProperty ("data");

		EditorGUILayout.PropertyField(data.FindPropertyRelative("isJunction"));

		if(data.FindPropertyRelative("isJunction").boolValue)
		{
			EditorGUILayout.PropertyField(data.FindPropertyRelative("node1"), new GUIContent("Left Node"));
			EditorGUILayout.PropertyField(data.FindPropertyRelative("node2"), new GUIContent("Right Node"));
		}
		else
		{
			EditorGUILayout.PropertyField(data.FindPropertyRelative("node1"), new GUIContent("Forward Node"));
		}

		serializedObject.ApplyModifiedProperties();
	}
}
