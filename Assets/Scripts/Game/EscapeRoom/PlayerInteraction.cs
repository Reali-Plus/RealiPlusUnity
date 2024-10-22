using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDist = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        Interactable interactable = collision.collider.GetComponent<Interactable>();
        if (interactable != null)
        {
            HandleInteraction(interactable, collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Interactable interactable = collision.collider.GetComponent<Interactable>();
        if (interactable != null)
        {
            // Optionnel : gérer la logique de sortie
        }
    }

    public abstract void HandleInteraction(Interactable interactable, Collision collision);
}
