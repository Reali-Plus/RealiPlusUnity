using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NormalizedEvent))]
public class NormalizedEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NormalizedEvent jointEvent = (NormalizedEvent)target;

        if (GUILayout.Button("Debug"))
        {
            jointEvent.DEBUG_VALUE();
        }
    }
}
