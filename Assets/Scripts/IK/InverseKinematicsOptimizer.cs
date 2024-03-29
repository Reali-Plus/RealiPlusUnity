using MathNet.Numerics.LinearAlgebra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematicsOptimizer : MonoBehaviour
{
    public int TargetDOFs => 6 * targets.Count;
    public int JointDOFs { get; private set; }

    private const float EPSILON = 1e-6f;

    [SerializeField, Min(0)] private int maxIter = 10;
    [SerializeField] private List<CostTransform> targets;
    [SerializeField] private List<PhysicsController> joints;

    private Matrix<float> jacobianMat;
    private Vector<float> errorVec;
    private Matrix<float> springMat;
    private Matrix<float> dampingMat;
    private Vector<float> torqueVec;
    private float elasticEnergy;

    private void OnEnable()
    {
        JointDOFs = 0;
        foreach (var joint in joints)
        {
            JointDOFs += joint.DOFs;
        }

        jacobianMat = Matrix<float>.Build.Dense(TargetDOFs, JointDOFs);
        errorVec = Vector<float>.Build.Dense(TargetDOFs);
        springMat = Matrix<float>.Build.Diagonal(TargetDOFs, TargetDOFs, InitSpringMatrix);
    }

    private float InitSpringMatrix(int index)
    {
        int targetIndex = index / 6;

        if (index % 6 < 3)
        {
            return targets[targetIndex].PositionWeight;
        }
        else
        {
            return targets[targetIndex].OrientationWeight;
        }
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
            UpdateEnergy();
            UpdateDampingMatrix();
            TickJoints();
        }
    }

    private void UpdateJacobian()
    {
        int jointIndex = 0;
        for (int i = 0; i < joints.Count; i++)
        {
            joints[i].UpdateJacobian(ref jacobianMat, in targets, jointIndex);
            jointIndex += joints[i].DOFs;
        }

        //Debug.Log(jacobianMat);
    }

    private void UpdateErrorVector()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            errorVec.SetSubVector(6 * i, 6, targets[i].GetErrorVector());
        }

        Debug.Log(errorVec);
    }

    private void UpdateEnergy()
    {
        torqueVec = jacobianMat.Transpose() * springMat * errorVec;
        elasticEnergy = 0.5f * errorVec * springMat * errorVec;

        //Debug.Log($"Torque: {torqueVec} \nEnergy: {elasticEnergy}");
    }

    private void UpdateDampingMatrix()
    {
        dampingMat = jacobianMat.Transpose() * springMat * jacobianMat + 
                     (0.5f * elasticEnergy + EPSILON) * Matrix<float>.Build.DiagonalIdentity(JointDOFs);

        //Debug.Log(dampingMat);
    }

    private void TickJoints()
    {
        Vector<float> delta = dampingMat.Inverse() * torqueVec;

        //Debug.Log(delta);

        int jointIndex = 0;
        for (int i = 0; i < joints.Count; i++)
        {
            joints[i].ApplyStepDisplacement(in delta, jointIndex);
            jointIndex += joints[i].DOFs;
        }
    }
}
