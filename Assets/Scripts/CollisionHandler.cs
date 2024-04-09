using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private DetectCollisions parentScript;

    public void Initialize(DetectCollisions parent)
    {
        parentScript = parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        parentScript.HandleCollision(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        parentScript.ExitCollision(gameObject);
    }
}
