using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private DetectCollisions parentScript;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    public void Initialize(DetectCollisions parent)
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
}