#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
namespace Map
{
    [CustomEditor(typeof(MapManager))]
    public class MapManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MapManager myScript = (MapManager)target;
            if (GUILayout.Button("Generate Map"))
            {
                myScript.GenerateMap();
            }
        }
    }
}

#endif