using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransformController : MonoBehaviour
{
    [Serializable]
    protected struct DOFData
    {
        public bool Active;
        public float MinValue;
        public float MaxValue;
    }

    public abstract int DOFs { get; }

    private List<CostTransform> childTargets;

    private void Awake()
    {
        childTargets = new List<CostTransform>(GetComponentsInChildren<CostTransform>());
    }

    public abstract void UpdateJacobian(ref Matrix<float> jacobian, in List<CostTransform> targets, int jointIndex);
    public abstract void ApplyStepDisplacement(in Vector<float> delta, int jointIndex);

    protected bool IsChildTarget(CostTransform target)
    {
        return childTargets.Contains(target);
    }
}