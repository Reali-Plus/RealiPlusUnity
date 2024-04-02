public class SleeveData
{
    // TODO : Add other data
    public SensorData3D Accelerometer { get; private set; }
    public SensorData3D Gyroscope { get; private set; }

    public SleeveData()
    {
        Accelerometer = new SensorData3D(0, 0, 0);
        Gyroscope = new SensorData3D(0, 0, 0);
    }

    public SleeveData(string strMessage)
    {
        Accelerometer = new SensorData3D(0, 0, 0);
        Gyroscope = new SensorData3D(0, 0, 0);
        FromString(strMessage);
    }

    public SleeveData(SensorData3D gyro, SensorData3D acc)
    {
        Accelerometer = new SensorData3D(acc);
        Gyroscope = new SensorData3D(gyro);
    }

   public void UpdateData(SensorData3D gyro, SensorData3D acc)
    {
        Accelerometer.UpdateData(acc);
        Gyroscope.UpdateData(gyro);
    }

    public bool FromString(string message)
    {
        string[] data = message.Split(" ");

        if (data.Length >= 3)
        {
            // Accelerometer
            float[] accelerations = new float[3];
            for (int i = 0; i < 3; i++)
            {
                accelerations[i] = ParseAcceleration(data[i]);
            }

            Accelerometer.UpdateData(accelerations);

            // Gyroscope
            if (data.Length >= 6)
            {
                float[] rotations = new float[3];
                for (int i = 3; i < 6; i++)
                {
                    rotations[i - 3] = ParseRotation(data[i]);
                }

                Gyroscope.UpdateData(rotations);

                return true;
            }
        }
        return false;
    }

    private float ParseRotation(string strRotation)
    {
        return float.TryParse(strRotation, out float rotation) ? rotation : 0f;
    }

    private float ParseAcceleration(string strAcceleration)
    {
        return float.TryParse(strAcceleration, out float acceleration) ? acceleration * 9.8f : 0f;
    }

    public override string ToString()
    {
        return "[Acc] " + Accelerometer.ToString() + " [Gyro] " + Gyroscope.ToString();
    }
}