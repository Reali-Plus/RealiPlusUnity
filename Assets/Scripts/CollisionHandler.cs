using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private DetectCollisions parentScript;
    private SocketCommunication socketCommunication;

    private void Start()
    {
        //socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

   public void Initialize(DetectCollisions parent)
   {
        parentScript = parent;
   }

    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionENTER");
        parentScript.UpdateFeedback(gameObject, true, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        print("OnCollisionEXIT");
        parentScript.UpdateFeedback(gameObject, true, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        parentScript.UpdateFeedback(gameObject, true, false);
    }

    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit");
        parentScript.UpdateFeedback(gameObject, false, false);
    }
}
