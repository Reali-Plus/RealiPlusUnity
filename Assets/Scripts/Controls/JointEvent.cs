using UnityEngine;

[CreateAssetMenu(fileName = "NewJointEvent", menuName = "ScriptableObjects/RealiPlus/JointEvent")]
public class JointEvent : PhysicsEvent<float>
{
    [SerializeField, Range(0f, 1f)] private float debugValue;

    public override void SetValue(float value)
    {
        CurrentValue = Mathf.Clamp01(value);
    }

    public void DEBUG_VALUE()
    {
        SetValue(debugValue);
    }
}
