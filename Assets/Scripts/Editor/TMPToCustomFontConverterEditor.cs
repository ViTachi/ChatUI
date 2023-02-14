using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TMPToCustomFontConverter))]
public class TMPToCustomFontConverterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TMPToCustomFontConverter converter = (TMPToCustomFontConverter)target;
        if (GUILayout.Button("Convert"))
        {
            converter.Convert();
        }
    }
}
