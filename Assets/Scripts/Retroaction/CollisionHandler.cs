/*using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private DetectCollision parentScript;

    public void Initialize(DetectCollision parent)
    {
        parentScript = parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        parentScript.UpdateFeedback(gameObject, true, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        parentScript.UpdateFeedback(gameObject, true, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        parentScript.UpdateFeedback(gameObject, true, false);
    }

    private void OnTriggerExit(Collider other)
    {
        parentScript.UpdateFeedback(gameObject, false, false);
    }
}*/