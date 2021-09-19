using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UIElements;

[CustomEditor(typeof(WorldData))]
[CanEditMultipleObjects]
public class WorldDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WorldData data = (WorldData)target;
        serializedObject.Update();

        SerializedProperty pointData = serializedObject.FindProperty("pointData");
        SerializedProperty walkable = serializedObject.FindProperty("walkable");
        SerializedProperty worldSize = serializedObject.FindProperty("worldSize");

        EditorGUILayout.PropertyField(pointData);
        EditorGUILayout.PropertyField(walkable);
        EditorGUILayout.PropertyField(worldSize);

        if (GUILayout.Button("Generate Data"))
        {
            data.Generate();
        }

        if (GUILayout.Button("Clear Data"))
        {
            data.Clear();
        }
        EditorUtility.SetDirty(target);

        serializedObject.ApplyModifiedProperties();
    }
}
