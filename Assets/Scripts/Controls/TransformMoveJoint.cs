using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using UnityEngine;

public class TransformMoveJoint : TransformController
{
    public override int DOFs => 3;

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        transform.Translate(delta[jointIndex], delta[jointIndex + 1], delta[jointIndex + 2]);
    }

    public override void UpdateJacobian(ref Matrix<float> jacobian, in List<CostTransform> targets, int jointIndex)
    {
        for (int i = 0; i < targets.Count; ++i)
        {
            if (!IsChildTarget(targets[i]))
            {
                continue;
            }

            int targetIndex = i * 6;

            for (byte b = 0; b < DOFs; ++b)
            {
                AffectJacobian(ref jacobian, targetIndex, jointIndex, b);
            }
        }
    }

    private void AffectJacobian(ref Matrix<float> jacobian, int targetIndex, int jointIndex, byte dofIndex)
    {
        Vector3 refAxis = transform.GetAxis(dofIndex);

        jacobian[targetIndex,     jointIndex + dofIndex] = refAxis.x;
        jacobian[targetIndex + 1, jointIndex + dofIndex] = refAxis.y;
        jacobian[targetIndex + 2, jointIndex + dofIndex] = refAxis.z;
        jacobian[targetIndex + 3, jointIndex + dofIndex] = 0f;
        jacobian[targetIndex + 4, jointIndex + dofIndex] = 0f;
        jacobian[targetIndex + 5, jointIndex + dofIndex] = 0f;
    }
}
