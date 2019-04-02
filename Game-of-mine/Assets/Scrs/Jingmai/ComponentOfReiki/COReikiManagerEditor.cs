#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
namespace JingMai
{
    [CustomEditor(typeof(COReikiManager))]
    public class COReikiManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            COReikiManager myScript = (COReikiManager)target;
            if (GUILayout.Button("Test"))
            {
                //myScript.GenerateBodyPart();
            }
        }
    }
}

#endif