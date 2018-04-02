using UnityEngine;

namespace Scripts.Obstacle
{
    public class ObstacleUnit : MonoBehaviour
    {
        private Vector2[] _bypassAreaPoints;

        void Start()
        {

        }

        public void UpdatePoints()
        {
            var obstacle = transform.Find("bypass");
            _bypassAreaPoints = new Vector2[obstacle.childCount];

            for (int i = 0; i < obstacle.childCount; i++)
            {
                _bypassAreaPoints[i] = obstacle.GetChild(i).position;
            }
        }

        public Vector2[] GetPoints()
        {
            return this._bypassAreaPoints;
        }
        void OnDrawGizmosSelected()
        {
            if (_bypassAreaPoints != null)
            {
                foreach (var point in _bypassAreaPoints)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(point, new Vector2(.1f, .1f));
                }
            }
        }
    }
}