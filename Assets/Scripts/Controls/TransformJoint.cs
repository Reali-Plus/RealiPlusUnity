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

    private Quaternion startRot;
    private Vector3 rotAxis;
    private Vector3 refAxis;

    private void Start()
    {
        rotAxis = Vector3.zero;
        rotAxis[(byte)axis] = 1f;

        refAxis = Vector3.zero;
        refAxis[((byte)axis + 1) % 3] = 1f;

        startRot = transform.localRotation;
    }

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        float currentAngle = Vector3.SignedAngle(startRot * refAxis, transform.localRotation * refAxis, transform.localRotation * rotAxis);
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
        Vector3 worldRot = transform.GetAxis((byte)axis);
        Vector3 movement = Vector3.Cross(worldRot, targetPos);

        jacobian[targetIndex,     jointIndex] = Vector3.Dot(movement, Vector3.right);
        jacobian[targetIndex + 1, jointIndex] = Vector3.Dot(movement, Vector3.up);
        jacobian[targetIndex + 2, jointIndex] = Vector3.Dot(movement, Vector3.forward);

        jacobian[targetIndex + 3, jointIndex] = worldRot.x;
        jacobian[targetIndex + 4, jointIndex] = worldRot.y;
        jacobian[targetIndex + 5, jointIndex] = worldRot.z;
    }
}
