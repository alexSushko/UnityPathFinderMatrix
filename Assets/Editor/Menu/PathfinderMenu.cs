using UnityEditor;
using UnityEngine;
namespace GraphPathFinder {
    public class MenuTest : MonoBehaviour {

        [MenuItem ("GameObject/GPF/Manager", false, 10)]
        static void CreatePathFinderManager (MenuCommand menuCommand) {
            // Create a custom game object
            GameObject go = new GameObject ("PathFinder");
            go.AddComponent<GraphPathFinder.PathFinderManager> ();
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign (go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo (go, "Create " + go.name);
            Selection.activeObject = go;
        }

    }
}