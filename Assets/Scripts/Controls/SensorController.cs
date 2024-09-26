using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    [SerializeField]
    private bool supportRotation = true;
    [SerializeField]
    private bool supportTranslation = false;

    [SerializeField]
    private SensorID sensorID;

    private SleeveData sleeveData;

    private void Awake()
    {
        sleeveData = new SleeveData();
    }

    private void FixedUpdate()
    {
        if (supportRotation)
        {
            transform.rotation *= Quaternion.Euler(new Vector3(sleeveData.Gyroscope.X, sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z) * Time.fixedDeltaTime);
        }

        if (supportTranslation)
        {
            transform.Translate(new Vector3(sleeveData.Accelerometer.X, sleeveData.Accelerometer.Y, sleeveData.Accelerometer.Z) * Time.fixedDeltaTime);
        }
    }

    public void ReceiveData(SleeveData newData)
    {
        sleeveData = newData;
    }

    public SensorID GetSensorID()
    {
        return sensorID;
    }
}