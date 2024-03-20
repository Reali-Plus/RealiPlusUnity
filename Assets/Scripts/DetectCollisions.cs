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
        //public Dictionary<int, >                    //dictionnaire de l'id et de la position (coordonnees)
        public int intensity;                       // intensité/force 
        public int textureCoeff;                    // chiffre/coefficient texture (largeur contact)
        
        // pour la restriction
        public bool isOpen;                         // ouvert/fermé pour la restriction
        public int jointsPosition;                  // position des joints // pas sur de son format vecteur?
    }

    private void Start()
    {
        fingerIds.Add(0, GameObject.FindGameObjectWithTag("Thumb"));
        fingerIds.Add(1, GameObject.FindGameObjectWithTag("Index"));
        fingerIds.Add(2, GameObject.FindGameObjectWithTag("Middle"));
        fingerIds.Add(3, GameObject.FindGameObjectWithTag("Ring"));
        fingerIds.Add(4, GameObject.FindGameObjectWithTag("Pinky"));
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
        //print((feedback.fingersOnCollisionIds).Count);
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
