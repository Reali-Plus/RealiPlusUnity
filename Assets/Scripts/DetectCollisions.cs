using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private List<GameObject> fingerObjects = new List<GameObject>();
    public List<GameObject> list = new();

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
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Thumb"));
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Index"));
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Middle"));
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Ring"));
        fingerObjects.Add(GameObject.FindGameObjectWithTag("Pinky"));
    }

    void OnCollisionEnter(Collision collision)
    {
        Feedback feedback = new Feedback();
        feedback.fingersOnCollisionIds = new List<int>();

        foreach (ContactPoint contact in collision.contacts)
        {
            //Debug.Log("COLLISION WITH: " + gameObject.name + "\n At this point:" + contact.point);
            Debug.Log("Impulse:" + collision.impulse);
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

        feedback.fingersOnCollisionIds = new List<int>();
        feedback.intensity = 0;
        feedback.textureCoeff = 0;

        feedback.isOpen = true;
        feedback.jointsPosition = 0;
    }

    // Fonction pour obtenir l'ID du doigt à partir du GameObject
    int GetFingerIdFromGameObject(GameObject fingerObject)
    {
        for (int i = 0; i < fingerObjects.Count; i++)
        {
            if (fingerObjects[i] == fingerObject)
            {
                //list.Add(fingerObject);
                return i;
            }
        }
        return -1;
    }
}
