using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JointEvent))]
public class JointEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        JointEvent jointEvent = (JointEvent)target;

        if (GUILayout.Button("Debug"))
        {
            jointEvent.DEBUG_VALUE();
        }
    }
}
