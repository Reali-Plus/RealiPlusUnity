using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    [SerializeField]
    private Transform palmTransform;

    private List<GameObject> objectsTouching = new List<GameObject>();

    public bool IsTouchingObject() => objectsTouching.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            objectsTouching.Add(other.gameObject);
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            objectsTouching.Remove(other.gameObject);
        }
    }

    public Grabbable GetClosestObjectTouching()
    {
        if (objectsTouching.Count == 0)
            return null;

        GameObject closestObject = objectsTouching[0];
        float minDistance = Vector3.Distance(palmTransform.position, closestObject.transform.position);
        float distance;

        for (int i = 0; i < objectsTouching.Count; ++i)
        {
            distance = Vector3.Distance(palmTransform.position, objectsTouching[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObject = objectsTouching[i];
            }
        }

        return closestObject.GetComponent<Grabbable>();
    }
}
