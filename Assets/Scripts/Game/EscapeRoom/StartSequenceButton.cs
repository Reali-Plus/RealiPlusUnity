using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceButton : MonoBehaviour
{
    [SerializeField] SimonManager simonManager;

    private void OnMouseDown()
    {
        simonManager.PlaySequence();
    }
}
