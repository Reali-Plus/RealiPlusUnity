using UnityEngine;

[CreateAssetMenu(fileName = "NewNormalizedEvent", menuName = "ScriptableObjects/RealiPlus/NormalizedEvent")]
public class NormalizedEvent : PhysicsEvent<float>
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
