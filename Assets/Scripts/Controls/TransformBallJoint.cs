using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using UnityEngine;

public class TransformBallJoint : TransformController
{
    [SerializeField] private Vector3 minRange;
    [SerializeField] private Vector3 maxRange;

    public override int DOFs => 3;

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        float deltaX = delta[jointIndex];
        float deltaY = delta[jointIndex + 1];
        float deltaZ = delta[jointIndex + 2];

        Vector3 deltaDeg = Mathf.Rad2Deg * new Vector3(deltaX, deltaY, deltaZ);
        Vector3 signedEuler = transform.localEulerAngles.DeltaEuler();

        Vector3 eulerDelta = (deltaDeg + signedEuler).Clamp(minRange, maxRange) - signedEuler;

        transform.Rotate(eulerDelta, Space.Self);
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
            Vector3 targetRelPos = targets[i].transform.position - transform.position;

            for (byte b = 0; b < DOFs; ++b)
            {
                AffectJacobian(ref jacobian, in targetRelPos, targetIndex, jointIndex, b);
            }
        }
    }

    private void AffectJacobian(ref Matrix<float> jacobian, in Vector3 targetPos, int targetIndex, int jointIndex, byte dofIndex)
    {
        Vector3 refAxis = transform.GetAxis(dofIndex);
        Vector3 movement = Vector3.Cross(refAxis, targetPos);

        jacobian[targetIndex,     jointIndex + dofIndex] = Vector3.Dot(movement, Vector3.right);
        jacobian[targetIndex + 1, jointIndex + dofIndex] = Vector3.Dot(movement, Vector3.up);
        jacobian[targetIndex + 2, jointIndex + dofIndex] = Vector3.Dot(movement, Vector3.forward);

        jacobian[targetIndex + 3, jointIndex + dofIndex] = refAxis.x;
        jacobian[targetIndex + 4, jointIndex + dofIndex] = refAxis.y;
        jacobian[targetIndex + 5, jointIndex + dofIndex] = refAxis.z;
    }
}
