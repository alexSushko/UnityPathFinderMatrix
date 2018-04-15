using GraphPathFinder;
using NUnit.Framework;
using UnityEngine;

namespace GraphPathFinder.Tests {
    public class ObstacleTest {
        [Test]
        public void GetObstaclePoints () {
            var obstacle = new GameObject ("Obstacle");
            var component = obstacle.AddComponent<ObstacleColliderUnit> ();
            component.ResetCollider ();

            Assert.AreEqual (4, component.GetPoints ().Length);
        }
    }

}