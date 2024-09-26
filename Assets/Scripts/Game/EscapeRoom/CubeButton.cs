using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] float resetDelay =.25f;
    [SerializeField] SimonManager simonManager;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ResetButton();
    }

    private void OnMouseDown()
    {
        if (!simonManager.isSequencePlaying)
        {
            Highlight();
            simonManager.CheckSequence(this);  // Signale au SimonManager quel cube a été cliqué
        }
    }

    public void Highlight()
    {
        GetComponent<MeshRenderer>().material.color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    void ResetButton()
    {
        GetComponent<MeshRenderer>().material.color = defaultColor;
    }
}
