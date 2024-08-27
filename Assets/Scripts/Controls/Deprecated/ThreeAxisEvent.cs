using UnityEngine;

[CreateAssetMenu(fileName = "New3AxisEvent", menuName = "ScriptableObjects/RealiPlus/3AxisEvent")]
public class ThreeAxisEvent : PhysicsEvent<Vector3>
{
    public void RESET_VALUE()
    {
        SetValue(Vector3.zero);
    }
}
