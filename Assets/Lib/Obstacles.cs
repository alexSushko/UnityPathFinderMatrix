using System;
using UnityEngine;

namespace Lib
{
    [Serializable]
    public struct Obstacle
    {
        public Vector2 center { get; set; }
        public Vector2[] bypassPoints { get; set; }

        public Obstacle(Vector2 center, Vector2[] points)
        {
            this.center = center;
            this.bypassPoints = points;
        }
    }
}