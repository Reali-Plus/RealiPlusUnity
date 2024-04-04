using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private DetectCollisions parentScript;
    private SocketCommunication socketCommunication;

    private void Start()
    {
        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

   public void Initialize(DetectCollisions parent)
   {
        parentScript = parent;
   }

    private void OnCollisionEnter(Collision collision)
    {
        parentScript.HandleCollision(gameObject);
        /*HapticsData haptics = new HapticsData();
        haptics.AddFeedback(new FingerFeedback(0, true, true));
        socketCommunication.SendData(haptics);*/
        //if the collider is touch activer la restriction

    }

    private void OnCollisionExit(Collision collision)
    {
        parentScript.ExitCollision();
        /*HapticsData haptics = new HapticsData();
        haptics.AddFeedback(new FingerFeedback(0, false, false));
        socketCommunication.SendData(haptics);*/
        //if the collider is touch d�sactiver la restriction

    }

    private void OnTriggerEnter(Collider other)
    {
        //if the collider is touch activer le piezo
        parentScript.HandleTrigger();
    }

    private void OnTriggerExit(Collider other)
    {
        //if not in the collider desactiver le piezo
        parentScript.ExitTrigger();
    }
}
