using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphPathFinder.Lib;
using UnityEngine;

namespace GraphPathFinder {
    [AddComponentMenu ("GPF/Manager")]
    public class PathFinderManager : MonoBehaviour {
        public GameObject From;
        public GameObject To;
        public int TestCount;
        private List<Vector2> Path;
        #region Settings
        public bool DrawPaths;
        public bool UpdatePathsPerFrame;
        public bool InitializeFromFile;
        public bool WriteToJson;
        public bool TestWriteToFile;
        public string PathToFile;
        public LayerMask layer;
        public float MaxDistance;
        #endregion

        private Vector2[] _points;
        private float[, ] distanceMatrix;
        private int[, ] pathsMatrix;
        #region Unity functions
        private void Start () {
            if (InitializeFromFile) {
                LoadFromFile ();
                UpdatePaths ();
            } else {
                UpdatePoints ();
            }
        }
        private void Update () {
            if (UpdatePathsPerFrame) {
                UpdatePoints ();
            }
            for (int i = 0; i < TestCount; i++)
                this.Path = GetPath (From.transform.position, To.transform.position);

        }
        private void OnDrawGizmos () {
            if (DrawPaths && _points != null && distanceMatrix != null) {
                Gizmos.color = Color.white;
                for (int i = 0; i < _points.Length; i++) {
                    for (int j = 0; j < _points.Length; j++) {
                        if (distanceMatrix[i, j] != 0 && distanceMatrix[i, j] < float.MaxValue) {
                            Gizmos.DrawLine (_points[i], _points[j]);
                        }
                    }
                }
            }
            if (this.Path != null && this.Path.Any ()) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine (From.transform.position, this.Path[0]);
                for (int i = 0; i < this.Path.Count - 1; i++) {
                    Gizmos.DrawLine (this.Path[i], this.Path[i + 1]);
                }
                Gizmos.DrawLine (this.Path[this.Path.Count - 1], To.transform.position);
            }
        }
        #endregion

        #region API
        public void UpdatePoints () {
            GetPoints ();
            CalculateDistanceMatrix ();
            CalculatePathsMatrix ();
        }
        public void UpdatePaths () {
            CalculateDistanceMatrix ();
            CalculatePathsMatrix ();
        }

        public void GenerateToFile () {
            GetPoints ();
            CalculateDistanceMatrix ();
            CalculatePathsMatrix ();
            WriteToFile ();
        }

        public List<Vector2> GetPath (Vector2 from, Vector2 to) {
            var indexFrom = GetNearestPointIndex (from);
            var indexTo = GetNearestPointIndex (to);
            var path = RestorePath (indexTo, indexFrom);
            return path;
        }
        #endregion

        private void GetPoints () {
            var obstacles = FindObjectsOfType<ObstacleColliderUnit> ();
            List<Vector2> pointsList = new List<Vector2> ();

            foreach (var obstacle in obstacles) {
                pointsList.AddRange (obstacle.GetPoints ());
            }

            this._points = pointsList.ToArray ();

            PrintArray (this._points, "points.txt");
        }

        private void CalculateDistanceMatrix () {
            this.distanceMatrix = MatrixLib.CalculateDistanceMatrix (this._points, layer, MaxDistance);
            PrintMatrix (this.distanceMatrix, "dist.txt");
        }

        private void CalculatePathsMatrix () {
            var distanceMatrixCopy = (float[, ]) distanceMatrix.Clone ();
            this.pathsMatrix = PathFindingLib.CalculatePathsMatrix (distanceMatrixCopy);
            PrintMatrix (this.pathsMatrix, "paths.txt");
        }

        private void LoadFromFile () {

        }
        private void WriteToFile () {

        }
        private int GetNearestPointIndex (Vector2 point) {
            var minDist = float.MaxValue;
            var index = -1;
            for (int i = 0; i < _points.Length; i++) {
                var direction = point - _points[i];
                if (direction.magnitude < minDist) {
                    index = i;
                    minDist = direction.magnitude;
                }
            }

            return index;
        }
        private List<Vector2> RestorePath (int from, int to) {
            var result = new List<Vector2> ();
            var s = "Path : " + to + " ";
            result.Add (_points[to]);
            var lastPointIndex = to;
            int iteration = 0;
            do {
                lastPointIndex = pathsMatrix[from, lastPointIndex];
                s += lastPointIndex + " ";
                result.Add (_points[lastPointIndex]);
                iteration++;
                if (iteration > 300) throw new UnityException ("PathRestore iteration count over 300");
            } while (from != lastPointIndex);
            return result;
        }

        private void PrintMatrix<T> (T[, ] matrix, string path) {
            if (TestWriteToFile) {
                using (StreamWriter file =
                    new StreamWriter (path)) {
                    for (int i = 0; i < matrix.GetLength (0); i++) {
                        for (int j = 0; j < matrix.GetLength (1); j++) {
                            file.Write (matrix[i, j] + "\t");
                        }
                        file.WriteLine ();
                    }
                }
            }
        }
        private void PrintArray<T> (T[] arr, string path) {
            if (TestWriteToFile) {
                using (StreamWriter file =
                    new StreamWriter (path)) {
                    for (int i = 0; i < arr.Length; i++) {
                        file.Write (arr[i] + "\t");
                    }
                    file.WriteLine ();
                }
            }
        }
    }
}