using UnityEngine;

public class PhysicsBallJoint : PhysicsController
{
    [SerializeField] private ThreeAxisEvent eulerEvent;

    protected override void ApplySelfTransform(ref Matrix4x4 globalTRS, ref Quaternion globalRot)
    {
        //Quaternion transformRot = Quaternion.AngleAxis(targetAngle, globalRot * Vector3.right);
        //Quaternion localRot = Quaternion.AngleAxis(targetAngle, Vector3.right);

        Quaternion localRot = Quaternion.Euler(eulerEvent.CurrentValue);

        float angle;
        Vector3 axis;
        localRot.ToAngleAxis(out angle, out axis);
        Quaternion transformRot = Quaternion.AngleAxis(angle, globalRot * axis);

        globalTRS *= Matrix4x4.Rotate(localRot);
        globalRot = transformRot * globalRot;
    }
}
