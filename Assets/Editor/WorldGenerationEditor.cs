using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UIElements;

[CustomEditor(typeof(WorldGeneration))]
[CanEditMultipleObjects]
public class WorldGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WorldGeneration data = (WorldGeneration)target;
        serializedObject.Update();


        SerializedProperty tile = serializedObject.FindProperty("tile");
        EditorGUILayout.PropertyField(tile);

        SerializedProperty tileList = serializedObject.FindProperty("generatedTiles");
        EditorGUILayout.PropertyField(tileList);

        if (GUILayout.Button("Generate Data"))
        {
            data.GenerateWorld();
        }

        if (GUILayout.Button("Clear Data"))
        {
            data.Clear();
        }
        EditorUtility.SetDirty(target);

        serializedObject.ApplyModifiedProperties();
    }
}
