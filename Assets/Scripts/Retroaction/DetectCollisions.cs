using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private List<GameObject> fingerObjects = new List<GameObject>();

    private void Start()
    {
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));

        foreach (GameObject fingerObject in fingerObjects)
        {
            fingerObject.AddComponent<CollisionHandler>().Initialize(this);
        }
    }

    public void UpdateFeedback(GameObject fingerObject, bool retroaction, bool restriction)
    {
        int fingerId = GetFingerIdFromGameObject(fingerObject);

        if (fingerId != -1)
        {
        }
    }

    private int GetFingerIdFromGameObject(GameObject fingerObject)
    {
        for (int i = 0; i < fingerObjects.Count; ++i)
        {
            if (fingerObjects[i] == fingerObject)
            {
                return i;
            }
        }
        return -1;
    }
}