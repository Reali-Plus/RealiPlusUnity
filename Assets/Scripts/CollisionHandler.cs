using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private DetectCollisions parentScript;
    private SocketCommunication socketCommunication;
    public Color collisionColor = Color.red;
    public Color triggerColor = Color.blue;

    private void Start()
    {
        //socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    public void Initialize(DetectCollisions parent)
    {
        parentScript = parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionENTER");
        DisplayCollider(collision.collider, collisionColor);
        parentScript.UpdateFeedback(gameObject, true, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        print("OnCollisionEXIT");
        DisplayCollider(collision.collider, triggerColor);
        parentScript.UpdateFeedback(gameObject, true, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        DisplayCollider(other, triggerColor);
        parentScript.UpdateFeedback(gameObject, true, false);
    }

    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit");
        DisplayCollider(other, Color.white);
        parentScript.UpdateFeedback(gameObject, false, false);
    }

    private void DisplayCollider(Collider collider, Color color)
    {
        MeshRenderer colliderRenderer = collider.GetComponent<MeshRenderer>();
        if (colliderRenderer != null)
        {
            colliderRenderer.material.color = color;
        }
    }
}