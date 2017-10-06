using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointNodeScript))]
public class WaypointNodeEditor : Editor
{
	SerializedProperty data;
	bool backNodeFoldout = true;

	// Use this for initialization
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		data = serializedObject.FindProperty ("data");

		if(data.FindPropertyRelative("backNodes").arraySize > 0)
		{
			GUI.enabled = false;
			if(data.FindPropertyRelative("backNodes").arraySize == 1)
			{
				EditorGUILayout.PropertyField(data.FindPropertyRelative("backNodes").GetArrayElementAtIndex(0), new GUIContent("Back Node"));
			}
			else
			{
				backNodeFoldout = EditorGUILayout.Foldout(backNodeFoldout, "Back Nodes");
				if(backNodeFoldout)
				{
					EditorGUI.indentLevel++;
					for(int i = 0; i < data.FindPropertyRelative("backNodes").arraySize; i++)
					{
						EditorGUILayout.PropertyField(data.FindPropertyRelative("backNodes").GetArrayElementAtIndex(i), GUIContent.none);
					}
					EditorGUI.indentLevel--;
				}
			}
			GUI.enabled = true;
		}
		else
		{
			EditorGUILayout.HelpBox("This node has no previous node.", MessageType.Warning);
		}

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

		EditorGUILayout.HelpBox("To reset this node, click on \"Reset Node\" instead of \"Reset\".", MessageType.Info);

		serializedObject.ApplyModifiedProperties();
	}
}
