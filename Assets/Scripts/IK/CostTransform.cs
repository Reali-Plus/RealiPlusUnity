using System;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

public class CostTransform : MonoBehaviour
{
    public event Action OnValuesUpdated;
    public float PositionWeight => positionWeight;
    public float OrientationWeight => orientationWeight;

    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float positionWeight = 10f;
    [SerializeField, Min(0f)] private float orientationWeight = 0.1f;

    private void OnValidate()
    {
        OnValuesUpdated?.Invoke();
    }

    public Vector<float> GetErrorVector()
    {
        Vector<float> error = Vector<float>.Build.Dense(6);
        error.SetSubVector(0, 3, (target.position - transform.position).ToMNVector());

        Quaternion rotError = target.rotation * Quaternion.Inverse(transform.rotation);

        error.SetSubVector(3, 3, Mathf.Deg2Rad * rotError.eulerAngles.DeltaEuler().ToMNVector());

        return error;
    }

    private void OnDrawGizmos()
    {
        if (target == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
