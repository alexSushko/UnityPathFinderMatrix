using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace GraphPathFinder {
    [CustomEditor (typeof (ObstacleColliderUnit))]
    [CanEditMultipleObjects]
    public class ObstacleColliderUnitEditor : Editor {
        Vector2[] points = new Vector2[0];
        public override void OnInspectorGUI () {
            ObstacleColliderUnit script = (ObstacleColliderUnit) target;
            DrawDefaultInspector ();

            EditorGUILayout.BeginVertical ();
            if (GUILayout.Button ("Get points")) {
                points = script.GetPoints ();
            }
            EditorGUILayout.LabelField (points.Length.ToString ());
            EditorGUILayout.EndVertical ();
        }
    }
}