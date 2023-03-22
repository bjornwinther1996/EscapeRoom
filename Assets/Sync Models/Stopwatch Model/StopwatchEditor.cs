using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Stopwatch))]
public class StopwatchEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Stopwatch stopwatch = (Stopwatch)target;

        // Only enable in play mode
        EditorGUI.BeginDisabledGroup(Application.isPlaying == false);

        TimeSpan timeSpan = TimeSpan.FromSeconds(stopwatch.time);
        EditorGUILayout.LabelField($"Time: {timeSpan:mm\\:ss\\.ff}");

        if (GUILayout.Button("Start")) stopwatch.StartStopwatch();

        EditorGUI.EndDisabledGroup();

        if (Application.isPlaying) EditorUtility.SetDirty(target);
    }

}
