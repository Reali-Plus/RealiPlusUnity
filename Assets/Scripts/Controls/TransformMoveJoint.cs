using MathNet.Numerics.LinearAlgebra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMoveJoint : TransformController
{
    //[SerializeField] private DOFData xMove;
    //[SerializeField] private DOFData yMove;
    //[SerializeField] private DOFData zMove;

    public override int DOFs => 3;

    public override void ApplyStepDisplacement(in Vector<float> delta, int jointIndex)
    {
        transform.Translate(delta[jointIndex], delta[jointIndex + 1], delta[jointIndex + 2]);
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

            jacobian[d, jointIndex] = transform.right.x;
            jacobian[d + 1, jointIndex] = transform.right.y;
            jacobian[d + 2, jointIndex] = transform.right.z;
            jacobian[d + 3, jointIndex] = 0f;
            jacobian[d + 4, jointIndex] = 0f;
            jacobian[d + 5, jointIndex] = 0f;

            jacobian[d, jointIndex + 1] = transform.up.x;
            jacobian[d + 1, jointIndex + 1] = transform.up.y;
            jacobian[d + 2, jointIndex + 1] = transform.up.z;
            jacobian[d + 3, jointIndex + 1] = 0f;
            jacobian[d + 4, jointIndex + 1] = 0f;
            jacobian[d + 5, jointIndex + 1] = 0f;

            jacobian[d, jointIndex + 2] = transform.forward.x;
            jacobian[d + 1, jointIndex + 2] = transform.forward.y;
            jacobian[d + 2, jointIndex + 2] = transform.forward.z;
            jacobian[d + 3, jointIndex + 2] = 0f;
            jacobian[d + 4, jointIndex + 2] = 0f;
            jacobian[d + 5, jointIndex + 2] = 0f;
        }
    }
}
