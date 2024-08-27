using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThreeAxisEvent))]
public class ThreeAxisEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ThreeAxisEvent jointEvent = (ThreeAxisEvent)target;

        if (GUILayout.Button("Reset"))
        {
            jointEvent.RESET_VALUE();
        }
    }
}
