using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField]
    private SensorID sensorID;

    private HapticsData hapticsData;
    private SleeveCommunication sleeveCommunication;

    private void Start()
    {
        hapticsData = new HapticsData(sensorID);
        sleeveCommunication = FindObjectOfType<SleeveCommunication>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        hapticsData.UpdateFeedback(true, true);
        sleeveCommunication.SendData(hapticsData);
    }

    private void OnCollisionExit(Collision collision)
    {
        hapticsData.UpdateFeedback(true, false);
        sleeveCommunication.SendData(hapticsData);
    }

    private void OnTriggerEnter(Collider other)
    {
        hapticsData.UpdateFeedback(true, false);
        sleeveCommunication.SendData(hapticsData);
    }

    private void OnTriggerExit(Collider other)
    {
        hapticsData.UpdateFeedback(false, false);
        sleeveCommunication.SendData(hapticsData);
    }
}