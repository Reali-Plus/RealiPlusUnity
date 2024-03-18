using MathNet.Numerics.LinearAlgebra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematicsOptimizer : MonoBehaviour
{
    public int TargetDOFs => 6 * targets.Count;
    public int JointDOFs { get; private set; }

    [SerializeField, Min(0)] private int maxIter = 10;
    [SerializeField] private List<CostTransform> targets;
    [SerializeField] private List<PhysicsController> joints;

    private Matrix<float> jacobian;
    private Vector<float> error;

    private void OnEnable()
    {
        JointDOFs = 0;
        foreach (var joint in joints)
        {
            JointDOFs += joint.DOFs;
        }

        jacobian = Matrix<float>.Build.Dense(TargetDOFs, JointDOFs);
        error = Vector<float>.Build.Dense(TargetDOFs);
    }

    private void Update() // TODO : "Fixed" update
    {
        /*
         * Masanori Sekiguchi & Naoyuki Takesue (2020) 
         * Fast and robust numerical method for inverse kinematics with prioritized multiple targets 
         * for redundant robots, Advanced Robotics, 34:16, 1068-1078, 
         * DOI: 10.1080/01691864.2020.1780151 
         */

        for (int i = 0; i < maxIter; i++)
        {
            UpdateJacobian();
            UpdateErrorVector();
            // Calculate virtual torque and elastic energy
            // Calculate damping matrix
            // Tick angles
        }
    }

    private void UpdateJacobian()
    {
        int jointIndex = 0;
        for (int i = 0; i < joints.Count; i++)
        {
            joints[i].UpdateJacobian(ref jacobian, in targets, jointIndex);
            jointIndex += joints[i].DOFs;
        }

        //Debug.Log(jacobian);
    }

    private void UpdateErrorVector()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            error.SetSubVector(6 * i, 6, targets[i].GetErrorVector());
        }

        //Debug.Log(error);
    }
}
