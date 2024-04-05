using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    [SerializeField] private List<GameObject> fingerObjects = new List<GameObject>();
    [SerializeField] private SocketCommunication socketCommunication;
    private HapticsData haptics = new HapticsData();

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

    public void HandleFeedBack()
    {
        socketCommunication.SendData(haptics);
    }

    public void UpdateFeedback(GameObject fingerObject, bool retroaction, bool restriction)
    {
        int fingerId = GetFingerIdFromGameObject(fingerObject);

        if (fingerId != -1)
        {
            haptics.AddFeedback(new FingerFeedback(fingerId, retroaction, restriction));
        }
    }

    //public void ExitCollision()
    //{
    //    print("EXIT COLLISION");
    //    restriction = false;
    //    //haptics.UpdateData(-1, false, false);
    //}

    //public void HandleTrigger()
    //{
    //    print("ON TRIGGER");
    //    retroaction = true;
    //}

    //public void ExitTrigger()
    //{
    //    print("EXIT TRIGGER");
    //    retroaction = false;
    //}

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
