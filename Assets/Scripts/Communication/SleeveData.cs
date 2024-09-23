using Assets.Scripts.Communication;
using UnityEngine;


public class SleeveData
{
    public FingerUtils.Finger FingerID { get; set; }
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

        if (data.Length >= 4)
        {
            // Finger ID
            FingerID = (FingerUtils.Finger)int.Parse(data[0]);

            // Accelerometer
            float[] accelerations = new float[3];
            for (int i = 0; i < 3; i++)
            {
                accelerations[i] = ParseInput(data[i]) * 9.8f;
            }

            Accelerometer.UpdateData(accelerations);

            // Gyroscope
            if (data.Length >= 7)
            {
                float[] rotations = new float[3];
                for (int i = 3; i < 6; i++)
                {
                    rotations[i - 3] = ParseInput(data[i]);
                }

                Gyroscope.UpdateData(rotations);
            }
            return true;
        }

        Debug.Log("Invalid data: " + message);
        return false;
    }

    private float ParseInput(string strInput)
    {
        return float.TryParse(strInput, out float input) ? input : 0f;
    }

    public override string ToString()
    {
        return "[Acc] " + Accelerometer.ToString() + " [Gyro] " + Gyroscope.ToString();
    }
}