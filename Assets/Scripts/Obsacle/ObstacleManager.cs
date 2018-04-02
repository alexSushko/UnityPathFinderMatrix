using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace Scripts.Obstacle {
    public class ObstacleManager : MonoBehaviour {
        public bool UpdatePointsOnStart;
        public bool DrawPoints;
        public GameObject ObstaclesParent;

        public List<Lib.Obstacle> obstacles;

        public float[, ] matrix;

        void Awake () {
            obstacles = new List<Lib.Obstacle> ();
        }
        void Start () {
            UpdateObstacles ();
            UpdateCollisionMatrix ();
        }

        public void UpdateObstacles () {
            var obstaclesUnits = ObstaclesParent.GetComponentsInChildren (typeof (ObstacleUnit));
            foreach (ObstacleUnit obstacleUnit in obstaclesUnits) {
                if (UpdatePointsOnStart) {
                    obstacleUnit.UpdatePoints ();
                }
                obstacles.Add (new Lib.Obstacle (obstacleUnit.transform.position, obstacleUnit.GetPoints ()));
            }
        }

        public void UpdateCollisionMatrix () {
            matrix = Lib.Matrix.CalculateDistanceMatrix (obstacles);
        }

        void OnDrawGizmos () {
            if (DrawPoints) {
                var currentIndexX = 0;
                var currentIndexY = 0;

                for (int i = 0; i < obstacles.Count; i++) {
                    for (int j = 0; j < obstacles[i].bypassPoints.Length; j++) {
                        currentIndexY = 0;
                        for (int k = 0; k < obstacles.Count; k++) {
                            for (int p = 0; p < obstacles[k].bypassPoints.Length; p++) {
                                if (matrix[currentIndexX, currentIndexY] != 0) {
                                    Gizmos.DrawLine (obstacles[i].bypassPoints[j], obstacles[k].bypassPoints[p]);
                                }
                                currentIndexY++;
                            }
                        }
                        currentIndexX++;
                    }
                }
            }

        }
    }
}