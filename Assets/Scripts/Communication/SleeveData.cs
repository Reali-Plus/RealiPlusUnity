using UnityEngine;

public enum SensorID
{
    Invalid = -1,
    Logic = 0,
    Hand = 1,
    Shoulder = 2,
    Index = 3,
    Major = 4,
    Annular = 5,
    Auricular = 6,
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
        string[] data = message.Split(" ");

        if(data.Length >= 0) // Ignore empty messages
        {
            if (data[0] == "|") // Ignore messages that start with "|"
            {
                Debug.Log("Message received: " + message);

                return false;
            }
        }

        if (data.Length >= 4) // Accelerometer data (SensorID, AccX, AccY, AccZ)
        {
            // Sensor ID
            SensorID = int.TryParse(data[0], out int input) ? (SensorID) input : SensorID.Invalid;

            // Accelerometer
            float[] accelerations = new float[3];
            for (int i = 1; i < 4; ++i)
            {
                accelerations[i - 1] = float.TryParse(data[i], out float accel) ? accel : accelerations[i - 1];
            }

            Accelerometer.UpdateData(accelerations);

            // Gyroscope
            if (data.Length >= 7) // Gyroscope data (GyroX, GyroY, GyroZ)
            {
                float[] rotations = new float[3];
                for (int i = 4; i < 7; ++i)
                {
                    rotations[i - 4] = float.TryParse(data[i], out float rot) ? rot : rotations[i - 4];
                }

                Gyroscope.UpdateData(rotations);
            }

            return true;
        }

        // Debug.Log("Invalid data: " + message);
        return false;
    }

    public override string ToString()
    {
        return (int) SensorID + " [Acc] " + Accelerometer.ToString() + " [Gyro] " + Gyroscope.ToString();
    }
}