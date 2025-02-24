using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ClimbTrigger))]
public class ClimbInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Custom Inspector Comming Soon*");
    }
}
