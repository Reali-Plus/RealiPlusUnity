using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private bool isGrabbed = false;
    private Vector3 grabPosition = Vector3.zero;
    private Vector3 grabDirection = Vector3.zero;
    private Transform grabParent = null;

    void Update()
    {
        if (isGrabbed)
        {
            UpdateTransform();
        }
    }

    public void UpdateTransform()
    {
        // Follow the grabParent with the same relative position
        transform.position = grabParent.position + (transform.position - grabPosition);
        // Rotate the object to keep the same relative direction
        transform.rotation = Quaternion.FromToRotation(grabDirection, transform.position - grabParent.position) * transform.rotation;
    }

    public void Grab(Transform grabParent)
    {
        isGrabbed = true;
        this.grabParent = grabParent;
        grabPosition = transform.position;
        grabDirection = transform.position - grabParent.position;
    }

    public void Release()
    {
        isGrabbed = false;
    }
}
