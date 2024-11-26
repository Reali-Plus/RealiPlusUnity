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

    private Grabbable grabbedObject = null;
    private bool isGrabbing = false;


    private void Update()
    {
        if (isGrabbing)
        {
            if (ShouldRelease())
            {
                grabbedObject.Release();
                grabbedObject = null;
                isGrabbing = false;

            }
        }
        else
        {
            if (ShouldGrab())
            {
                grabbedObject = grabZone.GetClosestObjectTouching();
                grabbedObject.Grab(transform);
                isGrabbing = true;
            }
        }
    }

    public bool ShouldGrab() => CalculateGrabPosition() < grabDistance && grabZone.IsTouchingObject();

    public bool ShouldRelease() => CalculateGrabPosition() > grabDistance;

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
