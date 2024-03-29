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
            jacobian[d,   jointIndex] = transform.right.x;
            jacobian[d+1, jointIndex] = transform.right.y;
            jacobian[d+2, jointIndex] = transform.right.z;
            jacobian[d+3, jointIndex] = 0f;
            jacobian[d+4, jointIndex] = 0f;
            jacobian[d+5, jointIndex] = 0f;

            jacobian[d,   jointIndex+1] = transform.up.x;
            jacobian[d+1, jointIndex+1] = transform.up.y;
            jacobian[d+2, jointIndex+1] = transform.up.z;
            jacobian[d+3, jointIndex+1] = 0f;
            jacobian[d+4, jointIndex+1] = 0f;
            jacobian[d+5, jointIndex+1] = 0f;

            jacobian[d,   jointIndex+2] = transform.forward.x;
            jacobian[d+1, jointIndex+2] = transform.forward.y;
            jacobian[d+2, jointIndex+2] = transform.forward.z;
            jacobian[d+3, jointIndex+2] = 0f;
            jacobian[d+4, jointIndex+2] = 0f;
            jacobian[d+5, jointIndex+2] = 0f;
        }
    }

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        float deltaX = delta[jointIndex];
        float deltaY = delta[jointIndex + 1];
        float deltaZ = delta[jointIndex + 2];

        positionEvent.SetValue(positionEvent.CurrentValue + new Vector3(deltaX, deltaY, deltaZ));
    }
}
