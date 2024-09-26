using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceButton : MonoBehaviour
{
    SimonManager simonManager; // Référence au SimonManager
    private void Start()
    {
        simonManager = FindObjectOfType<SimonManager>();
    }

    private void OnMouseDown()
    {
        simonManager.PlaySequence();
    }
}
