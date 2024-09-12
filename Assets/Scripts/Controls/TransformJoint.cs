using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using UnityEngine;

public class TransformJoint : TransformController
{
    private enum Axis
    {
        X, Y, Z
    }

    [SerializeField] private Axis axis;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;

    public override int DOFs => 1;

    private Vector3 startForward;
    private Vector3 rotAxis => transform.GetAxis((byte)axis);

    private void Start()
    {
        startForward = transform.forward;
    }

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        float currentAngle = Vector3.SignedAngle(startForward, transform.forward, rotAxis);
        float deltaDeg = Mathf.Clamp(Mathf.Rad2Deg * delta[jointIndex] + currentAngle, minRange, maxRange) - currentAngle;

        transform.Rotate(rotAxis, deltaDeg);
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

            AffectJacobian(ref jacobian, in targetRelPos, targetIndex, jointIndex);
        }
    }

    private void AffectJacobian(ref Matrix<float> jacobian, in Vector3 targetPos, int targetIndex, int jointIndex)
    {
        Vector3 movement = Vector3.Cross(rotAxis, targetPos);

        jacobian[targetIndex,     jointIndex] = Vector3.Dot(movement, Vector3.right);
        jacobian[targetIndex + 1, jointIndex] = Vector3.Dot(movement, Vector3.up);
        jacobian[targetIndex + 2, jointIndex] = Vector3.Dot(movement, Vector3.forward);

        jacobian[targetIndex + 3, jointIndex] = rotAxis.x;
        jacobian[targetIndex + 4, jointIndex] = rotAxis.y;
        jacobian[targetIndex + 5, jointIndex] = rotAxis.z;
    }
}
