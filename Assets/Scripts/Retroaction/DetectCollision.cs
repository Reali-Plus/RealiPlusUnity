using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField]
    private SensorID sensorID;

    private HapticsData hapticsData;

    private void Start()
    {
        hapticsData = new HapticsData(sensorID);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(sensorID + " Collision detected");
        hapticsData.UpdateFeedback(true, true);
        //sleeveCommunication.SendData(hapticsData);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(sensorID + " Collision ended");
        hapticsData.UpdateFeedback(true, false);
        //sleeveCommunication.SendData(hapticsData);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(sensorID + " Trigger detected");
        hapticsData.UpdateFeedback(true, false);
        //sleeveCommunication.SendData(hapticsData);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(sensorID + " Trigger ended");
        hapticsData.UpdateFeedback(false, false);
        //sleeveCommunication.SendData(hapticsData);
    }
}