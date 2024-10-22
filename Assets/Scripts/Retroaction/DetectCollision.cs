using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Demander si cette liste est vrm nécessaire, je vois que le Thuml c'est le joystick dans l<arborecense, mais pq on met pas le script directement la ?
    // Je vais faire des modifs pour que ca soit plus simple a ce niveau
    // Je comprend une partie du pq, juste avec un fichier on peut ajouter l'objet a tous les deux, en utilisant seulement un tag, mais il faut quand meme lui ajouuter un tag donc rendu la on peut mettre le script
    // private List<GameObject> fingerObjects = new List<GameObject>();

    [SerializeField]
    private SensorID sensorID;

    private SleeveCommunication sleeveCommunication;
    private HapticsData hapticsData;

    /*private void Start()
    {
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));

        foreach (GameObject fingerObject in fingerObjects)
        {
            fingerObject.AddComponent<CollisionHandler>().Initialize(this);
        }

        sleeveCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SleeveCommunication>();
    }*/

    private void Start()
    {
        //sleeveCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SleeveCommunication>();
        hapticsData = new HapticsData(sensorID);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        hapticsData.UpdateFeedback(true, true);
        //sleeveCommunication.SendData(hapticsData);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision ended");
        hapticsData.UpdateFeedback(true, false);
        //sleeveCommunication.SendData(hapticsData);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected");
        hapticsData.UpdateFeedback(true, false);
        //sleeveCommunication.SendData(hapticsData);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger ended");
        hapticsData.UpdateFeedback(false, false);
        //sleeveCommunication.SendData(hapticsData);
    }


    /*    public void UpdateFeedback(GameObject fingerObject, bool retroaction, bool restriction)
        {
            int fingerId = GetFingerIdFromGameObject(fingerObject);

            if (fingerId != -1)
            {
                sleeveCommunication.SendData(new HapticsData(fingerId, retroaction, restriction));
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
        }*/
}