using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class PlayerInteraction : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Interactable interactable = collision.collider.GetComponent<Interactable>();
        if (interactable != null)
        {
            HandleInteraction(interactable, collision);
        }
    }

    public abstract void HandleInteraction(Interactable interactable, Collision collision);
}
