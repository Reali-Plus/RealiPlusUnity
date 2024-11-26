using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabListener : MonoBehaviour
{
    /*[SerializeField]
    private int nbFingersRequired = 3;

    [SerializeField]
    private BasketZoneManager basketZoneManager;*/

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
                isGrabbing = false;
                Debug.Log("Releasing grab");
            }
        }
        else
        {
            if (ShouldGrab())
            {
                grabbedObject = grabZone.GetClosestObjectTouching();
                grabbedObject.Grab(transform);
                isGrabbing = true;
                Debug.Log("Grab parent " + grabbedObject.name);
            }
        }
    }

    /*public void OnEnable()
    {
        DetectCollision.OnFingerTouch += CollisionDetected;
    }*/

    private void Start()
    {
        //fingersTransforms = new List<Transform>();

 
        //objectsTouching = new Dictionary<Grabbable, int>();
        //objectsTouching = new List<GameObject>();
    }





    /*private void CollisionDetected(SensorID sensorID, GameObject hitObject, bool fingerState)
    {
        if (!hitObject.TryGetComponent<Grabbable>(out var grabbable))
            return;

        UpdateFigerState(sensorID, grabbable, fingerState);
        UpdateObjects(grabbable, fingerState);

        if (!fingerState && isGrabbing && ShouldRelease())
        {
            grabbable.Release();
            isGrabbing = false;
            Debug.Log("Releasing grab");
        } 
        else if (fingerState && !isGrabbing && ShouldGrab())
        {
            grabbedObject = grabbable;
            grabbable.Grab(transform);
            Debug.Log("Grab parent " + grabbedObject.name);
            isGrabbing = true;
        }

    }*/

/*    private IEnumerator WaitToRelease()
    {
        yield return new WaitForSeconds(2);
        canRelease = true;
    }*/

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

        Debug.Log("Distance : " + distance);

        return distance;


 /*       Debug.Log("Distance : " + distance);
        if (distance > 0.14f) // around 0.16 when in the release position
        {
            Debug.Log("Release");
        } else // around 0.09 when in the grab position
        {
            Debug.Log("Grab");
        }*/
    }

    public void Release()
    {
        isGrabbing = false;
        grabbedObject = null; // TODO remove this vrariable
        Debug.Log("Releasing grab");
    }

    public void ReleaseCurrentObject()
    {
        if (isGrabbing)
        {
            grabbedObject.Release();
        }
    }

}
