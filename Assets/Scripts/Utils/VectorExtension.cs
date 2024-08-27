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

    /// <summary>
    /// Converts a Vector3 (Unity) into a MathNet Vector object
    /// </summary>
    public static Vector<float> ToMNVector(this Vector3 v)
    {
        return Vector<float>.Build.Dense(new float[] { v.x, v.y, v.z });
    }

    /// <summary>
    /// Converts a Euler angle Vector3 in absolute value to a delta representation (Shortest distance) 
    /// </summary>
    public static Vector3 DeltaEuler(this Vector3 v)
    {
        return new Vector3(Mathf.DeltaAngle(0, v.x), Mathf.DeltaAngle(0, v.y), Mathf.DeltaAngle(0, v.z));
    }

    /// <summary>
    /// Returns a Vector3 clamped element-wise between min and max 
    /// </summary>
    public static Vector3 Clamp(this Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = Vector3.zero;
        for (byte b = 0; b < 3; b++)
        {
            result[b] = Mathf.Clamp(v[b], min[b], max[b]);
        }
        return result;
    }
}
