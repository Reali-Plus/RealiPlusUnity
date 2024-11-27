using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : Interactable
{
    public override void Interact(Vector3 position)
    {
        Debug.Log("Interacted with TestButton");
    }
}
