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
        for (int i = 0; i < targets.Count; i++)
        {
            if (!IsChildTarget(targets[i]))
            {
                continue;
            }

            int d = i * 6;
            Vector3 targetRelPos = targets[i].transform.position - transform.position;

            Vector3 movementX = Vector3.Cross(transform.right, targetRelPos);
            jacobian[d,     jointIndex] = Vector3.Dot(movementX, Vector3.right);
            jacobian[d + 1, jointIndex] = Vector3.Dot(movementX, Vector3.up);
            jacobian[d + 2, jointIndex] = Vector3.Dot(movementX, Vector3.forward);

            jacobian[d + 3, jointIndex] = transform.right.x;
            jacobian[d + 4, jointIndex] = transform.right.y;
            jacobian[d + 5, jointIndex] = transform.right.z;

            Vector3 movementY = Vector3.Cross(transform.up, targetRelPos);
            jacobian[d,     jointIndex + 1] = Vector3.Dot(movementY, Vector3.right);
            jacobian[d + 1, jointIndex + 1] = Vector3.Dot(movementY, Vector3.up);
            jacobian[d + 2, jointIndex + 1] = Vector3.Dot(movementY, Vector3.forward);

            jacobian[d + 3, jointIndex + 1] = transform.up.x;
            jacobian[d + 4, jointIndex + 1] = transform.up.y;
            jacobian[d + 5, jointIndex + 1] = transform.up.z;

            Vector3 movementZ = Vector3.Cross(transform.forward, targetRelPos);
            jacobian[d,     jointIndex + 2] = Vector3.Dot(movementZ, Vector3.right);
            jacobian[d + 1, jointIndex + 2] = Vector3.Dot(movementZ, Vector3.up);
            jacobian[d + 2, jointIndex + 2] = Vector3.Dot(movementZ, Vector3.forward);

            jacobian[d + 3, jointIndex + 2] = transform.forward.x;
            jacobian[d + 4, jointIndex + 2] = transform.forward.y;
            jacobian[d + 5, jointIndex + 2] = transform.forward.z;
        }
    }
}
