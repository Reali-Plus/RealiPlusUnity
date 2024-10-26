using UnityEngine;

public class SensorData3D
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }

    public SensorData3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public SensorData3D(SensorData3D data)
    {
        X = data.X;
        Y = data.Y;
        Z = data.Z;
    }

    public void UpdateData(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void UpdateData(SensorData3D data)
    {
        X = data.X;
        Y = data.Y;
        Z = data.Z;
    }

    public void UpdateData(float[] data)
    {
        if (data.Length >= 3)
        {
            UpdateData(data[0], data[1], data[2]);
        }
    }

    public void UpdateData(float[] data, Vector3 offset)
    {
        if (data.Length >= 3)
        {
            UpdateData(data[0] - offset.x, data[1] - offset.y, data[2] - offset.z);
        }
    }

    public override string ToString()
    {
        return "X " + X + " Y " + Y + " Z " + Z;
    }
}