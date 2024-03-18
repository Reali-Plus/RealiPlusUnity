using MathNet.Numerics.LinearAlgebra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostTransform : MonoBehaviour
{
    private static readonly List<CostTransform> allCostTransform = new List<CostTransform>();

    [SerializeField] private Transform target;
    [SerializeField] private float positionWeight = 10f;
    [SerializeField] private float orientationWeight = 0.1f;

    private void OnEnable()
    {
        allCostTransform.Add(this);
        Debug.Log($"{gameObject} cost: {GetCost()}, Total cost: {GetTotalCost()}");
    }

    private void OnDisable()
    {
        allCostTransform.Remove(this);
    }

    public Vector<float> GetErrorVector()
    {
        Vector<float> error = Vector<float>.Build.Dense(6);
        error.SetSubVector(0, 3, (target.position - transform.position).ToMNVector());

        (Quaternion.Inverse(target.rotation) * transform.rotation).ToAngleAxis(out float angle, out Vector3 axis);
        error.SetSubVector(3, 3, (Mathf.Deg2Rad * angle * axis).ToMNVector());

        return error;
    }

    public float GetCost()
    {
        return positionWeight * (transform.position - target.position).magnitude +
               orientationWeight * Quaternion.Angle(transform.rotation, target.rotation);
    }

    public static float GetTotalCost()
    {
        float cost = 0f;
        foreach (var costTransform in allCostTransform)
        {
            cost += costTransform.GetCost();
        }
        return cost;
    }

    private void OnDrawGizmos()
    {
        if (target == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
