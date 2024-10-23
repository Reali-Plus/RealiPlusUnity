using System.Collections.Generic;
using UnityEngine;

public class GrabListener : MonoBehaviour
{
    [SerializeField]
    private int numberOfFingers = 3;

    private Dictionary<SensorID, bool> fingersState = new Dictionary<SensorID, bool>();
    private int fingersTouching = 0;
    private bool isGrabbing = false;


    public void OnEnable()
    {
        DetectCollision.OnFingerTouch += UpdateFingerState;
    }

    private void Start()
    {
        for (int i = 6; i < 8; i++)
        {
            fingersState.Add((SensorID)i, false);
        }
    }

    private void UpdateFingerState(SensorID sensorID, bool state)
    {
        fingersState[sensorID] = state;

        if (state)
            fingersTouching++;
        else
            fingersTouching--;


        // TODO: Find a way to clip the object to the hand
        Debug.Log("Fingers touching: " + fingersTouching);

        if (isGrabbing)
        { 
            if (ShouldRelease())
            {
                isGrabbing = false;
                Debug.Log("Releasing grab");
            }
        }
        else
        {
            if (ShouldGrab())
            {
                isGrabbing = true;
                Debug.Log("Grabbing");
            }
        }
    }

    public bool ShouldGrab()
    {
        return fingersTouching == numberOfFingers;
    }

    public bool ShouldRelease()
    {
        return fingersTouching - (fingersState[SensorID.Palm] ? 1 : 0) != numberOfFingers;
    }
}
