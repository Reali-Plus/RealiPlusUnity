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
    }

    private void OnCollisionExit(Collision collision)
    {
        parentScript.ExitCollision();
        /*HapticsData haptics = new HapticsData();
        haptics.AddFeedback(new FingerFeedback(0, false, false));
        socketCommunication.SendData(haptics);*/
    }
}
