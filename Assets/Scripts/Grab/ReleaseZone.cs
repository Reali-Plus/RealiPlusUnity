using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Grabbable grabbable))
        {
            grabbable.Release();
        }
    }
}
