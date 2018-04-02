using System.Collections.Generic;
using UnityEngine;
namespace Lib {
    public static class Matrix {
        public static float[, ] CalculateDistanceMatrix (List<Obstacle> obstacles) {
            var dimension = CalculateMatrixDimension (obstacles);
            var matrix = new float[dimension, dimension];
            var currentIndexX = 0;
            var currentIndexY = 0;

            for (int i = 0; i < obstacles.Count; i++) {
                for (int j = 0; j < obstacles[i].bypassPoints.Length; j++) {
                    currentIndexY = 0;
                    for (int k = 0; k <= i; k++) {
                        for (int p = 0; p < obstacles[k].bypassPoints.Length; p++) {

                            if (currentIndexX != currentIndexY) {
                                matrix[currentIndexX, currentIndexY] = CalculatDistanceBetweenPoints (
                                    obstacles[i].bypassPoints[j],
                                    obstacles[k].bypassPoints[p]
                                );
                            }
                            currentIndexY++;
                        }
                    }
                    currentIndexX++;
                }
            }
            return matrix;
        }
        private static int CalculateMatrixDimension (List<Obstacle> obstacles) {
            int result = 0;
            foreach (var obstacle in obstacles) {
                result += obstacle.bypassPoints.Length;
            }
            return result;
        }

        private static float CalculatDistanceBetweenPoints (Vector2 point1, Vector2 point2) {
            if (CheckCollisionBetweenPoints (point1, point2)) {
                return 0;
            } else {
                return (point1 - point2).magnitude;
            }
        }

        private static bool CheckCollisionBetweenPoints (Vector2 point1, Vector2 point2) {
            var direction = point2 - point1;
            //TODO add layerMask
            RaycastHit2D hit = Physics2D.Raycast (point1, direction, direction.magnitude);
            if (hit.collider == null) {
                return false;
            } else {
                return true;
            }
        }
    }
}