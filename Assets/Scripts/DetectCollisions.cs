using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private List<GameObject> fingerObjects = new List<GameObject>();
    private SocketCommunication socketCommunication;

    private void Start()
    {
        print("START");
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));

        foreach (GameObject fingerObject in fingerObjects)
        {
            fingerObject.AddComponent<CollisionHandler>().Initialize(this);
        }

        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    public void UpdateFeedback(GameObject fingerObject, bool retroaction, bool restriction)
    {
        int fingerId = GetFingerIdFromGameObject(fingerObject);

        if (fingerId != -1)
        {
            socketCommunication.SendData(new HapticsData(fingerId, retroaction, restriction));
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