using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    [SerializeField] private List<GameObject> fingerObjects = new List<GameObject>();
    //private HapticsData.FingerFeedback currentFeedback;
    [SerializeField]
    private SocketCommunication socketCommunication;


    private void Start()
    {
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));

        foreach (GameObject fingerObject in fingerObjects)
        {
            fingerObject.AddComponent<CollisionHandler>().Initialize(this);
        }

        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    public void HandleCollision(GameObject fingerObject)
    {
        Debug.Log("Collision detected");
        //currentFeedback.fingersOnCollisionIds = new List<int>();

        int fingerId = GetFingerIdFromGameObject(fingerObject);
        if (fingerId != -1)
            Debug.Log("Finger " + fingerId + " is in collision");
        {
            HapticsData haptics = new HapticsData();
            haptics.AddFeedback(new FingerFeedback(fingerId, true, true));
            socketCommunication.SendData(haptics);

            //haptics.UpdateData(fingerId, true, true);
            //currentFeedback.fingersOnCollisionIds.Add(fingerId);
        }
    }

    public void ExitCollision()
    {
        //TODO: modifier pour faire fonctionner
        //haptics.UpdateData(-1, false, false);
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
