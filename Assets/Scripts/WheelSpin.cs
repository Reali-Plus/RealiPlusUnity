using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    public float rotationSpeed = 100f;
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }
}
