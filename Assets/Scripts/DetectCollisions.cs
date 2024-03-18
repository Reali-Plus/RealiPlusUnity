using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    struct Feedback
    {
        // pour la rétroaction
        public List<GameObject> fingersOnCollision; // liste des doigt(s) en jeux dans la collision
        public int intensity;                       // intensité/force 
        public int textureCoeff;                    // chiffre/coefficient texture (largeur contact)
        
        // pour la restriction
        public bool isOpen;                         // ouvert/fermé pour la restriction
        public int jointsPosition;                  // position des joints // pas sur de son format vecteur?
    }

    void OnCollisionEnter(Collision collision)
    {
        Feedback feedback = new Feedback();
        feedback.fingersOnCollision = new List<GameObject>();

        foreach (ContactPoint contact in collision.contacts)
        {
            print("COLLISIONS WITH: "+ gameObject.name + "\n At this point:" + contact.point);
            feedback.fingersOnCollision.Add(collision.gameObject);
        }
        Debug.Log("feedback.fingersOnCollision" + feedback.fingersOnCollision);
    }

    // Lorsqu'il n'y a plus de collision on remet 
    private void OnCollisionExit(Collision collision)
    {
        Feedback feedback = new Feedback();

        feedback.fingersOnCollision = null;
        feedback.intensity = 0;
        feedback.textureCoeff = 0;

        feedback.isOpen = true;
        feedback.jointsPosition = 0;
        Debug.Log("feedback.fingersOnCollision" + feedback.fingersOnCollision);

    }
}
