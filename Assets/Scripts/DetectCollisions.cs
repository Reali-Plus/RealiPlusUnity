using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private List<GameObject> fingerObjects = new List<GameObject>();
    private SocketCommunication socketCommunication;

    private void Start()
    {
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Index"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Middle"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Ring"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Pinky"));

        foreach (GameObject fingerObject in fingerObjects)
        {
            fingerObject.AddComponent<CollisionHandler>().Initialize(this);
        }

        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    public void HandleCollision(GameObject fingerObject)
    {
        int fingerId = GetFingerIdFromGameObject(fingerObject);
        if (fingerId != -1)
        {
            socketCommunication.SendData(new HapticsData(fingerId, true, true));
        }
    }

    public void ExitCollision(GameObject fingerObject)
    {
        int fingerId = GetFingerIdFromGameObject(fingerObject);
        if (fingerId != -1)
        {
            socketCommunication.SendData(new HapticsData(fingerId, false, false));
        }
    }

    private int GetFingerIdFromGameObject(GameObject fingerObject)
    {
        for (int i = 0; i < fingerObjects.Count; i++)
        {
            if (fingerObjects[i] == fingerObject)
            {
                return i;
            }
        }
        return -1;
    }
}
