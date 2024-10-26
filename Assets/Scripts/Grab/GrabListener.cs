using System.Collections.Generic;
using UnityEngine;

public class GrabListener : MonoBehaviour
{
    [SerializeField]
    private int nbFingersRequired = 3;

    struct FingerState
    {
        public bool isTouching;
        public Grabbable hitObject;

        public FingerState(bool isTouching, Grabbable hitObject)
        {
            this.isTouching = isTouching;
            this.hitObject = hitObject;
        }
    }

    private Dictionary<SensorID, FingerState> fingersState = new Dictionary<SensorID, FingerState>();
    Dictionary<Grabbable, int> objectsTouching = new Dictionary<Grabbable, int>();

    private Grabbable grabbedObject = null;
    private bool isGrabbing = false;


    public void OnEnable()
    {
        DetectCollision.OnFingerTouch += CollisionDetected;
    }

    private void Start()
    {
        for (int i = 3; i < 8; ++i)
        {
            fingersState.Add((SensorID) i, new FingerState(false, null));
        }
        objectsTouching = new Dictionary<Grabbable, int>();
    }

    private void UpdateObjects(Grabbable hitObject, bool fingerState)
    {
        if (fingerState)
        {
            if (objectsTouching.ContainsKey(hitObject))
                ++objectsTouching[hitObject];
            else
                objectsTouching.Add(hitObject, 1);
        }
        else
        {
            --objectsTouching[hitObject]; // Find a place to remove the object from the dictionary ?
        }
    }

    private void UpdateFigerState(SensorID sensorID, Grabbable hitObject, bool fingerState)
    {
        if (fingersState.ContainsKey(sensorID))
        {
            FingerState state = fingersState[sensorID];
            state.isTouching = fingerState;

            if (fingerState)
                state.hitObject = hitObject;
            else
                state.hitObject = null;

            fingersState[sensorID] = state;
        }
        else
        {
            fingersState.Add(sensorID, new FingerState(fingerState, hitObject));
        }

        
    }

    private void CollisionDetected(SensorID sensorID, GameObject hitObject, bool fingerState)
    {
        Grabbable grabbable = hitObject.GetComponent<Grabbable>();
        if (grabbable == null)
            return;

        UpdateFigerState(sensorID, grabbable, fingerState);
        UpdateObjects(grabbable, fingerState);
        
        if (isGrabbing)
        { 
            if (ShouldRelease())
            {
                grabbable.Release();
                isGrabbing = false;
                Debug.Log("Releasing grab");
            }
        }
        else
        {
            if (ShouldGrab())
            {
                grabbedObject = grabbable;
                grabbable.Grab(transform);
                isGrabbing = true;
                Debug.Log("Grabbing");
            }
        }
    }

    public void OnDisable()
    {
        DetectCollision.OnFingerTouch -= CollisionDetected;
    }

    // TODO: Check for palm
    public bool ShouldGrab()
    {
        foreach (var obj in objectsTouching)
        {
            if (obj.Value == nbFingersRequired)
                return true;
        }

        return false;
    }

    public bool ShouldRelease()
    {
        return objectsTouching[grabbedObject] <= nbFingersRequired;
    }
}
