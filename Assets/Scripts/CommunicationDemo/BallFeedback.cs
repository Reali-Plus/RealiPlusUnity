using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFeedback : MonoBehaviour
{
    private SocketCommunication socketCommunication;

    void Start()
    {
        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ball hit");

            HapticsData hapticsData = new HapticsData("Ball Hit");
            socketCommunication.SendData(hapticsData);
        }
    }
}
