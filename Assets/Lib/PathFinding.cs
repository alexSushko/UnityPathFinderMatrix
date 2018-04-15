using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GraphPathFinder.Lib {
    public static class PathFindingLib {
        public static int[, ] CalculatePathsMatrix (float[, ] matrix) {
            var parentsMatrix = new int[matrix.GetLength (0), matrix.GetLength (1)];
            for (int i = 0; i < parentsMatrix.GetLength (0); i++) {
                for (int j = 0; j < parentsMatrix.GetLength (1); j++) {
                    parentsMatrix[i, j] = i;
                }
            }
            for (int k = 0; k < matrix.GetLength (0); k++) {
                for (int x = 0; x < matrix.GetLength (0); x++) {
                    for (int y = 0; y < matrix.GetLength (0); y++) {
                        if (matrix[x, y] > matrix[x, k] + matrix[k, y]) {
                            matrix[x, y] = matrix[x, k] + matrix[k, y];
                            parentsMatrix[x, y] = parentsMatrix[k, y];
                        }

                    }
                }
            }
            return parentsMatrix;
        }

    }

}