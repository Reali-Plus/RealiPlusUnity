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
            X = data[0];
            Y = data[1];
            Z = data[2];
        }
    }

    public override string ToString()
    {
        return "X " + X + " Y " + Y + " Z " + Z;
    }
}
