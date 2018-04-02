using System.Collections;
using UnityEditor;
using UnityEngine;
namespace Scripts.Obstacle {
    [CanEditMultipleObjects]
    [CustomEditor (typeof (ObstacleUnit))]
    public class ObstacleUnitEditor : Editor {
        public override void OnInspectorGUI () {
            ObstacleUnit myTarget = (ObstacleUnit) target;

            EditorGUILayout.BeginVertical ();
            if (GUILayout.Button ("Update points")) {
                myTarget.UpdatePoints ();
            }

            if (myTarget.GetPoints () != null) {
                EditorGUILayout.LabelField ("Points count:", myTarget.GetPoints ().Length.ToString ());
            }
            EditorGUILayout.EndVertical ();
        }
    }
}