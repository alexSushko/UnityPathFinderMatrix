using UnityEngine;

namespace GraphPathFinder {
    public abstract class ObstacleUnit : MonoBehaviour {
        public abstract Vector2[] GetPoints ();
    }
}