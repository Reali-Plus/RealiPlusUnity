using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

public static class VectorExtension
{
    /// <summary>
    /// Inverts a Vector3 element-wise
    /// </summary>
    public static Vector3 Invert(this Vector3 v)
    {
        return new Vector3(1f/v.x, 1f/v.y, 1f/v.z);
    }

    public static Vector<float> ToMNVector(this Vector3 v)
    {
        return Vector<float>.Build.Dense(new float[] { v.x, v.y, v.z });
    }

    public static Vector3 DeltaEuler(this Vector3 v)
    {
        return new Vector3(Mathf.DeltaAngle(0, v.x), Mathf.DeltaAngle(0, v.y), Mathf.DeltaAngle(0, v.z));
    }
}
