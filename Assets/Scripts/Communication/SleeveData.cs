using UnityEngine;


public enum SensorID
{
    Logic = 0,
    Hand = 1,
    Shoulder = 2,
    Index = 6,
    Major = 5,
    Annullar = 4,
    Auricular = 3,
    Thumb = 7,
    Calibrate = 8
}

public class SleeveData
{
    public SensorID SensorID { get; set; }
    public SensorData3D Accelerometer { get; private set; }
    public SensorData3D Gyroscope { get; private set; }

    public SleeveData()
    {
        Accelerometer = new SensorData3D(0, 0, 1);
        Gyroscope = new SensorData3D(0, 0, 0);
    }

    public SleeveData(string strMessage)
    {
        Accelerometer = new SensorData3D(0, 0, 1);
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

    // Data format: [SensorID] [AccX] [AccY] [AccZ] [GyroX] [GyroY] [GyroZ]
    public bool FromString(string message)
    {
        Debug.Log("Message : " + message);
        string[] data = message.Split(" ");

        if(data.Length >= 0) // Ignore empty messages
        {
            Debug.Log("data[0] " + data[0]);
            if (data[0] == "|") // Ignore messages that start with "|"
            {
                Debug.Log("Message received: " + message);

                return false;
            }
        }

        if (data.Length >= 4) // Accelerometer data (SensorID, AccX, AccY, AccZ)
        {
            // Sensor ID
            SensorID = (SensorID) ParseIntInput(data[0]);

            // Accelerometer
            float[] accelerations = new float[3];
            for (int i = 1; i < 4; ++i)
            {
                accelerations[i - 1] = ParseFloatInput(data[i]);
            }

            Accelerometer.UpdateData(accelerations);

            // Gyroscope
            if (data.Length >= 7) // Gyroscope data (GyroX, GyroY, GyroZ)
            {
                float[] rotations = new float[3];
                for (int i = 3; i < 6; ++i)
                {
                    rotations[i - 3] = ParseFloatInput(data[i]);
                }

                Gyroscope.UpdateData(rotations);
            }

            return true;
        }

        Debug.Log("Invalid data: " + message);
        return false;
    }

    private float ParseFloatInput(string strInput)
    {
        return float.TryParse(strInput, out float input) ? input : 0f;
    }

    private int ParseIntInput(string strInput)
    {
        return int.TryParse(strInput, out int input) ? input : 0;
    }

    public override string ToString()
    {
        return "[ID] " + SensorID.ToString() + " [Acc] " + Accelerometer.ToString() + " [Gyro] " + Gyroscope.ToString();
    }
}