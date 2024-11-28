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
        sleeveCommunication = SleeveCommunication.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateHaptics(true, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        UpdateHaptics(true, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateHaptics(true, false);
        if (other.gameObject.TryGetComponent(out Interactable interactable))
        {
            interactable.Interact(transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UpdateHaptics(false, false);
    }

    private void UpdateHaptics(bool retroaction, bool restriction)
    {
        hapticsData.UpdateFeedback(retroaction, restriction);
        sleeveCommunication.SendData(hapticsData);
    }
}