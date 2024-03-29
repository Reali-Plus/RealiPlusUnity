using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsJoint : PhysicsController
{
    public override int DOFs => 1;

    [SerializeField] private NormalizedEvent jointEvent;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;

    protected override void ApplySelfTransform(ref Matrix4x4 globalTRS, ref Quaternion globalRot)
    {
        float targetAngle = Mathf.Lerp(minRotation, maxRotation, jointEvent.CurrentValue);

        Quaternion transformRot = Quaternion.AngleAxis(targetAngle, globalRot * Vector3.right);
        Quaternion localRot = Quaternion.AngleAxis(targetAngle, Vector3.right);

        globalTRS *= Matrix4x4.Rotate(localRot);
        globalRot = transformRot * globalRot;
    }

    public override void UpdateJacobian(ref Matrix<float> jacobian, in List<CostTransform> targets, int jointIndex)
    {
        throw new System.NotImplementedException();
    }

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        throw new System.NotImplementedException();
    }
}
