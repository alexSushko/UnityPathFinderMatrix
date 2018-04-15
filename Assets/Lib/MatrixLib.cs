using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GraphPathFinder.Lib {
    public static class MatrixLib {
        public static float[, ] CalculateDistanceMatrix (Vector2[] points, int layer) {
            var matrix = new float[points.Length, points.Length];

            for (int i = 0; i < points.Length; i++) {
                for (int k = 0; k < points.Length; k++) {

                    if (i != k) {
                        matrix[i, k] = CalculatDistanceBetweenPoints (
                            points[i],
                            points[k],
                            layer
                        );
                    }
                }
            }
            return matrix;
        }
        private static float CalculatDistanceBetweenPoints (Vector2 point1, Vector2 point2, int layer) {
            if (CheckCollisionBetweenPoints (point1, point2, layer)) {
                return float.MaxValue;
            } else {
                return (point1 - point2).magnitude;
            }
        }

        private static bool CheckCollisionBetweenPoints (Vector2 point1, Vector2 point2, int layer) {
            var direction = point2 - point1;
            RaycastHit2D[] hits = Physics2D.RaycastAll (point1, direction, direction.magnitude, layer);
            foreach (var hit in hits) {
                if (!hit.collider.isTrigger) {
                    return true;
                }
            }
            return false;
        }
    }
}