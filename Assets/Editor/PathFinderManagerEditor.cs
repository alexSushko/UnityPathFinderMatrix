using System.Collections;
using UnityEditor;
using UnityEngine;
namespace GraphPathFinder {
    [CustomEditor (typeof (PathFinderManager))]
    [CanEditMultipleObjects]
    public class MyPlayerEditor : Editor {
        public override void OnInspectorGUI () {
            PathFinderManager script = (PathFinderManager) target;
            DrawDefaultInspector ();

            EditorGUILayout.BeginVertical ();
            if (GUILayout.Button ("Update points")) {
                script.UpdatePoints ();
            }

            if (GUILayout.Button ("Update paths")) {
                script.UpdatePaths ();
            }

            EditorGUILayout.EndVertical ();
        }
    }
}