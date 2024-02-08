using UnityEngine;

public class PhysicsMove : PhysicsController
{
    [SerializeField] private PositionEvent positionEvent;

    protected override void ApplySelfTransform(ref Matrix4x4 globalTRS, ref Quaternion globalRot)
    {
        globalTRS = globalTRS * Matrix4x4.Translate(positionEvent.CurrentValue);
    }
}
