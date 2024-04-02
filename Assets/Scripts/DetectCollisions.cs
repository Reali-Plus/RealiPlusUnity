using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    [SerializeField] private List<GameObject> fingerObjects = new List<GameObject>();
    private Feedback currentFeedback; 

    private struct Feedback
    {
        // pour la rétroaction
        public List<int> fingersOnCollisionIds;     // liste des ID en collision
        public int intensity;                       // intensité/force 
        public int textureCoeff;                    // chiffre/coefficient texture (largeur contact)
        
        // pour la restriction
        public bool isOpen;                         // ouvert/fermé pour la restriction
        public int jointsPosition;                  // position des joints

        public Feedback(List<int> fingersOnCollisionIds, int intensity, int textureCoeff, bool isOpen, int jointsPosition)
        {
            this.fingersOnCollisionIds = fingersOnCollisionIds;
            this.intensity = intensity;
            this.textureCoeff = textureCoeff;
            this.isOpen = isOpen;
            this.jointsPosition = jointsPosition;
        }
    }

    private void Start()
    {
        print("START");
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Index"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Middle"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Ring"));
        //fingerObjects.Add(GameObject.FindGameObjectWithTag("Pinky"));

        foreach (GameObject fingerObject in fingerObjects)
        {
            fingerObject.AddComponent<CollisionHandler>().Initialize(this);
        }
    }

    public void HandleCollision(GameObject fingerObject)
    {
        currentFeedback.fingersOnCollisionIds = new List<int>();

        int fingerId = GetFingerIdFromGameObject(fingerObject);
        if (fingerId != -1)
        {
            print("HandleCollision:"+ fingerId);
            currentFeedback.fingersOnCollisionIds.Add(fingerId);
        }
    }

    public void ExitCollision()
    {
        print("EXIT COLLISION");
        currentFeedback = new Feedback(new List<int>(), 0, 0, true, 0);
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
