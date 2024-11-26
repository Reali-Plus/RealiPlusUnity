using System;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField]
    private SensorID sensorID;

    private HapticsData hapticsData;
    private SleeveCommunication sleeveCommunication;

    //public static event Action<SensorID, GameObject, bool> OnFingerTouch;

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
    }

    private void OnCollisionExit(Collision collision)
    {
        UpdateHaptics(true, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateHaptics(true, false);

        //OnFingerTouch?.Invoke(sensorID, other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        UpdateHaptics(false, false);
     
        //OnFingerTouch?.Invoke(sensorID, other.gameObject, false);
    }

    private void UpdateHaptics(bool retroaction, bool restriction)
    {
        hapticsData.UpdateFeedback(retroaction, restriction);
        sleeveCommunication.SendData(hapticsData);
    }
}