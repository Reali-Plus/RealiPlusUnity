using System.Collections.Generic;
using UnityEngine;

public class GrabListener : MonoBehaviour
{
    [SerializeField]
    private GrabZone grabZone;

    [SerializeField]
    private Transform palmTransform;

    [SerializeField]
    private List<Transform> fingersTransforms;

    [SerializeField]
    private float grabDistance = 0.14f;

    private SleeveCommunication sleeveCommunication;

    private Grabbable grabbedObject = null;
    private bool isGrabbing = false;


    private void Start()
    {
        sleeveCommunication = SleeveCommunication.Instance;
    }

    private void Update()
    {
        if (isGrabbing)
        {
            if (ShouldRelease())
            {
                Release();
            }
        }
        else
        {
            if (ShouldGrab())
            {
                Grab();
            }
        }
    }

    public bool ShouldGrab() => CalculateGrabPosition() < grabDistance && grabZone.IsTouchingObject();

    public bool ShouldRelease() => CalculateGrabPosition() > grabDistance;

    public void Release()
    {
        grabbedObject.Release();
        grabbedObject = null;
        isGrabbing = false;

        sleeveCommunication.SendToAllFingers(false);
    }

    public void Grab()
    {
        grabbedObject = grabZone.GetClosestObjectTouching();
        grabbedObject.Grab(transform);
        isGrabbing = true;

        sleeveCommunication.SendToAllFingers(true);
    }

    public float CalculateGrabPosition()
    {
        // Calculate the average position of the fingers
        Vector3 averagePosition = Vector3.zero;
        foreach (var finger in fingersTransforms)
        {
            averagePosition += finger.position;
        }

        averagePosition /= fingersTransforms.Count;

        // Calculate the distance from the palm to the average position
        float distance = Vector3.Distance(averagePosition, palmTransform.position);

        return distance;
    }
}
