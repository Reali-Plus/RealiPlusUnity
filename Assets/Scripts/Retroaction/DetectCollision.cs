using System;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField]
    private SensorID sensorID;

    private HapticsData hapticsData;
    private SleeveCommunication sleeveCommunication;

    public static event Action<SensorID, GameObject, bool> OnFingerTouch;

    [SerializeField]
    private bool sendCollision = true;

    private void Start()
    {
        hapticsData = new HapticsData(sensorID);
        sleeveCommunication = FindObjectOfType<SleeveCommunication>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateHaptics(true, true);

        if (collision.gameObject.TryGetComponent(out Interactable interactable))
        {
            interactable.Interact(transform.position);
        }
        Debug.Log(sensorID + " Collision detected");

        OnFingerTouch?.Invoke(sensorID, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        UpdateHaptics(true, false);
        Debug.Log(sensorID + " Collision ended");

        OnFingerTouch?.Invoke(sensorID, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateHaptics(true, false);
        Debug.Log(sensorID + " Trigger detected");

        OnFingerTouch?.Invoke(sensorID, other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        UpdateHaptics(false, false);
        Debug.Log(sensorID + " Trigger ended");
     
        OnFingerTouch?.Invoke(sensorID, other.gameObject, false);
    }

    private void UpdateHaptics(bool retroaction, bool restriction)
    {
        hapticsData.UpdateFeedback(retroaction, restriction);
        sleeveCommunication.SendData(hapticsData);
    }
}