using Assets.Scripts.Communication;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    [SerializeField]
    private bool supportRotation = true;
    [SerializeField]
    private bool supportTranslation = false;

    [SerializeField]
    private FingerUtils.Finger fingerID;

    // Check if it really needs to be a queue
    private Queue<SleeveData> dataQueue;

    private void Awake()
    {
        dataQueue = new Queue<SleeveData>();
    }

    private void FixedUpdate()
    {
        if (dataQueue.Count == 0)
        {
            return;
        }

        var sleeveData = dataQueue.Dequeue();

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
        dataQueue.Enqueue(newData);
    }

    public FingerUtils.Finger GetFingerID()
    {
        return fingerID;
    }
}