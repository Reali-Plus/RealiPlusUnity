using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : PlayerInteraction
{
    public override void HandleInteraction(Interactable interactable, Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            interactable.Interact(collision.transform.position);
        }
    }
}
