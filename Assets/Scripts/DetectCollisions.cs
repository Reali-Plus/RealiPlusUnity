using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    [SerializeField] private List<GameObject> fingerObjects = new List<GameObject>();
    [SerializeField] private SocketCommunication socketCommunication;
    private bool restriction;
    private bool retroaction;
    HapticsData haptics = new HapticsData();

    private void Start()
    {
        print("START");
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));

        foreach (GameObject fingerObject in fingerObjects)
        {
            fingerObject.AddComponent<CollisionHandler>().Initialize(this);
        }

        //socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    //ici faudrait update selon les collisions et les triggers qui arrivent et l'envoyer au update...
    public void HandleFeedBack(int fingerId, bool retro, bool restrict)
    {
        //haptics.AddFeedback(new FingerFeedback(fingerId, retro, restrict));
        //socketCommunication.SendData(haptics);
    }

    public void HandleCollision(GameObject fingerObject)
    {
        Debug.Log("Collision detected");
        int fingerId = GetFingerIdFromGameObject(fingerObject);
        if (fingerId != -1)
            Debug.Log("Finger " + fingerId + " is in collision");
        {
            HapticsData haptics = new HapticsData();
            haptics.AddFeedback(new FingerFeedback(fingerId, true, true));
            socketCommunication.SendData(haptics);

            //haptics.UpdateData(fingerId, true, true);
            //TODO AJOUTER LA LOGIQUE POUR ACTIVER LA R�TROACTION ET LA RESTRICTION
            //currentFeedback.fingersOnCollisionIds.Add(fingerId);
            restriction = true;
        }
    }

    public void ExitCollision()
    {
        print("EXIT COLLISION");
        restriction = false;
        //haptics.UpdateData(-1, false, false);
    }

    public void HandleTrigger()
    {
        print("ON TRIGGER");
        retroaction = true;
    }

    public void ExitTrigger()
    {
        print("EXIT TRIGGER");
        retroaction = false;
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
