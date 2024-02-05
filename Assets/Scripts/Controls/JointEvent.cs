using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJointEvent", menuName = "ScriptableObjects/Joints/JointEvent")]
public class JointEvent : ScriptableObject
{
    public float CurrentValue { get; private set; }

    [SerializeField, Range(0f, 1f)] private float debugValue;

    public void SetValue(float value)
    {
        CurrentValue = Mathf.Clamp01(value);
    }

    public void DEBUG_VALUE()
    {
        SetValue(debugValue);
    }
}
