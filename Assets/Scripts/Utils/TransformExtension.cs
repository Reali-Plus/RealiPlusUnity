using System;
using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// Returns the current world-space axis corresponding to the provided index. (0:Right, 1:Up, 2:Forward)
    /// </summary>
    public static Vector3 GetAxis(this Transform t, byte axisIndex)
    {
        switch (axisIndex)
        {
            case 0:
                return t.right;

            case 1:
                return t.up;

            case 2:
                return t.forward;

            default:
                throw new Exception($"{nameof(GetAxis)} only supports index 0 through 2. (0:Right, 1:Up, 2:Forward)");
        }
    }
}
