using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewJointEvent", menuName = "ScriptableObjects/Joints/JointEvent")]
public class JointEvent : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] private float debugValue;

    private List<JointController> listeners = new List<JointController>();

    public void RegisterListener(JointController listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public  void UnregisterListener(JointController listener)
    {
        listeners.Remove(listener);
    }

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);

        foreach (JointController listener in listeners)
        {
            listener.UpdateValue(value);
        }
    }

    public void DEBUG_VALUE()
    {
        SetValue(debugValue);
    }
}
