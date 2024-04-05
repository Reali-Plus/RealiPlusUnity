using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Use TransformBallJoint with NewPhysicsController instead. Optimization fails in physics cycle.")]
public class PhysicsBallJoint : PhysicsController
{
    public override int DOFs => 3;

    [SerializeField] private ThreeAxisEvent eulerEvent;

    protected override void ApplySelfTransform(ref Matrix4x4 globalTRS, ref Quaternion globalRot)
    {
        Quaternion localRot = Quaternion.Euler(eulerEvent.CurrentValue);

        float angle;
        Vector3 axis;
        localRot.ToAngleAxis(out angle, out axis);
        Quaternion transformRot = Quaternion.AngleAxis(angle, globalRot * axis);

        globalTRS *= Matrix4x4.Rotate(localRot);
        globalRot = transformRot * globalRot;
    }

    public override void UpdateJacobian(ref Matrix<float> jacobian, in List<CostTransform> targets, int jointIndex)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (!childTargets.Contains(targets[i]))
            {
                continue;
            }

            int d = i * 6;
            Vector3 targetRelPos = targets[i].transform.position - transform.position;

            Vector3 movementX = Vector3.Cross(transform.right, targetRelPos);
            jacobian[d,   jointIndex] = Vector3.Dot(movementX, Vector3.right);
            jacobian[d+1, jointIndex] = Vector3.Dot(movementX, Vector3.up);
            jacobian[d+2, jointIndex] = Vector3.Dot(movementX, Vector3.forward);
            jacobian[d+3, jointIndex] = transform.right.x;
            jacobian[d+4, jointIndex] = transform.right.y;
            jacobian[d+5, jointIndex] = transform.right.z;

            Vector3 movementY = Vector3.Cross(transform.up, targetRelPos);
            jacobian[d,   jointIndex+1] = Vector3.Dot(movementY, Vector3.right);
            jacobian[d+1, jointIndex+1] = Vector3.Dot(movementY, Vector3.up);
            jacobian[d+2, jointIndex+1] = Vector3.Dot(movementY, Vector3.forward);
            jacobian[d+3, jointIndex+1] = transform.up.x;
            jacobian[d+4, jointIndex+1] = transform.up.y;
            jacobian[d+5, jointIndex+1] = transform.up.z;

            Vector3 movementZ = Vector3.Cross(transform.forward, targetRelPos);
            jacobian[d,   jointIndex+2] = Vector3.Dot(movementZ, Vector3.right);
            jacobian[d+1, jointIndex+2] = Vector3.Dot(movementZ, Vector3.up);
            jacobian[d+2, jointIndex+2] = Vector3.Dot(movementZ, Vector3.forward);
            jacobian[d+3, jointIndex+2] = transform.forward.x;
            jacobian[d+4, jointIndex+2] = transform.forward.y;
            jacobian[d+5, jointIndex+2] = transform.forward.z;
        }
    }

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        float deltaX = delta[jointIndex];
        float deltaY = delta[jointIndex+1];
        float deltaZ = delta[jointIndex+2];

        eulerEvent.SetValue(eulerEvent.CurrentValue + Mathf.Rad2Deg * new Vector3(deltaX, deltaY, deltaZ));
    }
}
