using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMove : PhysicsController
{
    public override int DOFs => 3;

    [SerializeField] private ThreeAxisEvent positionEvent;

    protected override void ApplySelfTransform(ref Matrix4x4 globalTRS, ref Quaternion globalRot)
    {
        globalTRS = globalTRS * Matrix4x4.Translate(positionEvent.CurrentValue);
    }

    public override void UpdateJacobian(ref Matrix<float> jacobian, in List<CostTransform> targets, int jointIndex)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            int d = i * 6;
            jacobian[d,   jointIndex] = Vector3.Dot(transform.right, Vector3.right);
            jacobian[d+1, jointIndex] = Vector3.Dot(transform.right, Vector3.up);
            jacobian[d+2, jointIndex] = Vector3.Dot(transform.right, Vector3.forward);
            jacobian[d+3, jointIndex] = 0f;
            jacobian[d+4, jointIndex] = 0f;
            jacobian[d+5, jointIndex] = 0f;

            jacobian[d,   jointIndex+1] = Vector3.Dot(transform.up, Vector3.right);
            jacobian[d+1, jointIndex+1] = Vector3.Dot(transform.up, Vector3.up);
            jacobian[d+2, jointIndex+1] = Vector3.Dot(transform.up, Vector3.forward);
            jacobian[d+3, jointIndex+1] = 0f;
            jacobian[d+4, jointIndex+1] = 0f;
            jacobian[d+5, jointIndex+1] = 0f;

            jacobian[d,   jointIndex+2] = Vector3.Dot(transform.forward, Vector3.right);
            jacobian[d+1, jointIndex+2] = Vector3.Dot(transform.forward, Vector3.up);
            jacobian[d+2, jointIndex+2] = Vector3.Dot(transform.forward, Vector3.forward);
            jacobian[d+3, jointIndex+2] = 0f;
            jacobian[d+4, jointIndex+2] = 0f;
            jacobian[d+5, jointIndex+2] = 0f;
        }
    }
}
