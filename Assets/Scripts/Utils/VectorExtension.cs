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
}
