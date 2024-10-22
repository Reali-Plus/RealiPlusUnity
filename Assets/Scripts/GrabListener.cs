using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabListener : MonoBehaviour
{
    private Dictionary<SensorID, bool> fingersState = new Dictionary<SensorID, bool>();

    private int fingersTouching = 0;

    public void OnEnable()
    {
        CollisionHandler.OnFingerTouch += UpdateFingerState;
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
        if (ShouldGrab())
            Debug.Log("Grabbing");
        else
            Debug.Log("Not grabbing");
    }

    public bool ShouldGrab()
    {
        return fingersTouching == 2;
    }
}
