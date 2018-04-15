using System;
using System.Linq;
using UnityEngine;

namespace GraphPathFinder {
    [AddComponentMenu ("GPF/Collider Obstacle")]
    public class ObstacleColliderUnit : ObstacleUnit {
        public PolygonCollider2D _polygonCollider;

        private void Start () {
            Debug.Log ("Start :" + _polygonCollider.ToString ());
        }
        private void Reset () {
            ResetCollider ();
        }

        public override Vector2[] GetPoints () {
            if (_polygonCollider == null) {
                Debug.LogWarning ("GameObject " + name + " obstacle unit miss polygon collider!");
                return null;
            } else {
                var localPoints = _polygonCollider.GetPath (0);
                var pos = (Vector2) this.transform.position;
                var points = localPoints.Select (v => v + pos).ToArray ();
                Debug.Log (TestToString (points));
                return points;
            }
        }

        public void ResetCollider () {
            Debug.Log ("Reset");
            if (_polygonCollider == null) {
                _polygonCollider = (PolygonCollider2D) this.gameObject.AddComponent<PolygonCollider2D> ();
                _polygonCollider.isTrigger = true;
            }
            _polygonCollider.CreatePrimitive (4);
        }
        public static string TestToString (Vector2[] vector) {
            var s = "";
            foreach (var vec in vector) {
                s += vec.ToString ();
            }
            return s;
        }
    }
}