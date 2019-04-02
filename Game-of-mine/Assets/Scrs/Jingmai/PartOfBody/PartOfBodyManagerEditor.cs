#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
namespace JingMai
{
    [CustomEditor(typeof(PartOfBodyManager))]
    public class PartOfBodyManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PartOfBodyManager myScript = (PartOfBodyManager)target;
            if (GUILayout.Button("GenerateBodyPart"))
            {
                myScript.GenerateBodyPart();
            }
        }
    }
}

#endif