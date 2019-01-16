#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AI.Test
{
    [CustomEditor(typeof(_ParserTest), true)]
    public class _ParserTestEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Test Input"))
            {
                ((_ParserTest)target).startTest();
            }
        }

    }
}
#endif
