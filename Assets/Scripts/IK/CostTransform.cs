using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostTransform : MonoBehaviour
{
    private static readonly List<CostTransform> allCostTransform = new List<CostTransform>();

    [SerializeField] private Transform target;
    [SerializeField] private float positionWeight = 1f;
    [SerializeField] private float orientationWeight = 1f;

    private void OnEnable()
    {
        allCostTransform.Add(this);
    }

    private void OnDisable()
    {
        allCostTransform.Remove(this);
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
