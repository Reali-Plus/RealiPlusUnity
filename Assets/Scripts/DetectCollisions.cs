using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private Dictionary<int, GameObject> fingerIds = new Dictionary<int, GameObject>();

    struct Feedback
    {
        // pour la rétroaction
        public List<int> fingersOnCollisionIds;     // liste des ID en collision
        public int intensity;                       // intensité/force 
        public int textureCoeff;                    // chiffre/coefficient texture (largeur contact)
        
        // pour la restriction
        public bool isOpen;                         // ouvert/fermé pour la restriction
        public int jointsPosition;                  // position des joints // pas sur de son format vecteur?
    }

    private void Start()
    {
        //voir si c'est la meilleure facon de le mapper
        fingerIds.Add(0, GameObject.Find("Thumb_3"));
        fingerIds.Add(1, GameObject.Find("Finger1_4"));
        fingerIds.Add(2, GameObject.Find("Finger2_4"));
        fingerIds.Add(3, GameObject.Find("Finger3_4"));
        fingerIds.Add(4, GameObject.Find("Finger4_4"));
    }

    void OnCollisionEnter(Collision collision)
    {
        Feedback feedback = new Feedback();
        feedback.fingersOnCollisionIds = new List<int>();

        foreach (ContactPoint contact in collision.contacts)
        {
            //Debug.Log("COLLISION WITH: " + gameObject.name + "\n At this point:" + contact.point);
            int fingerId = GetFingerIdFromGameObject(gameObject);

            if (fingerId != -1)
            {
                feedback.fingersOnCollisionIds.Add(fingerId);
            }
        }

        // Affichage du contenu de la liste
        foreach (int id in feedback.fingersOnCollisionIds)
        {
            Debug.Log("ID EN COLLISION: " + id);
        }
    }

    // Lorsqu'il n'y a plus de collision on remet 
    private void OnCollisionExit(Collision collision)
    {
        Feedback feedback = new Feedback();

        feedback.fingersOnCollisionIds = null;
        feedback.intensity = 0;
        feedback.textureCoeff = 0;

        feedback.isOpen = true;
        feedback.jointsPosition = 0;
    }

    // Fonction pour obtenir l'ID du doigt à partir du GameObject
    int GetFingerIdFromGameObject(GameObject fingerObject)
    {
        foreach (KeyValuePair<int, GameObject> pair in fingerIds)
        {
            if (pair.Value == fingerObject)
            {
                return pair.Key;
            }
        }
        return -1;
    }

}
